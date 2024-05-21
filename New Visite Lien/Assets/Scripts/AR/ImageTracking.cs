using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedEvenement{
    public bool isCompleted;
    public float progress;

    public bool refreshed;

    public TrackedEvenement(bool _isCompleted,float _progress, bool _refreshed){
        isCompleted = _isCompleted;
        progress = _progress;
        refreshed = _refreshed;
    }
}
public class ImageTracking : MonoBehaviour
{
    [SerializeField] EvenementScriptableObject[] placeablePrefabs;

    Dictionary<string, (GameObject, EvenementScriptableObject)> spawnedPrefabs
    = new Dictionary<string, (GameObject, EvenementScriptableObject)>();
    public ARTrackedImageManager trackedImageManager;
    [SerializeField] XRReferenceImageLibrary runtimeImageLibrary;
    [SerializeField] ARSession aRSession;
    public UnityEvent<float> onScanProgress { get; private set; } = new UnityEvent<float>();
    Dictionary<ARTrackedImage, TrackedEvenement> imagesProgress = new Dictionary<ARTrackedImage, TrackedEvenement>();

    [SerializeField] Image backgroundForFlat;

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        foreach (var item in placeablePrefabs)
        {
            GameObject newPrefab;

            if (item.useFlatEvent)
            {
                newPrefab = Instantiate(item.flatEvent.gameObject, Vector3.zero, Quaternion.identity);
                newPrefab.transform.parent = Camera.main.transform;
                spawnedPrefabs.Add(item.name, (newPrefab, item));
                newPrefab.SetActive(false);
            }
            else
            {
                GameObject anchor = new GameObject(item.name, typeof(ARAnchor));
                newPrefab = Instantiate(item.arEvent.gameObject, Vector3.zero, Quaternion.identity);
                newPrefab.transform.parent = anchor.transform;
                spawnedPrefabs.Add(item.name, (anchor, item));
                anchor.SetActive(false);
            }
            newPrefab.name = item.name;

        }
    }

    private void Start()
    {
        UpdateImageLibrary(runtimeImageLibrary);
    }


    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    void UpdateImageLibrary(XRReferenceImageLibrary library)
    {
        trackedImageManager.enabled = false;
        trackedImageManager.referenceLibrary = trackedImageManager.CreateRuntimeLibrary(library);
        trackedImageManager.enabled = true;
    }

    void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        //Debug.Log("The image changed");
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            //Debug.Log("Image added");
            imagesProgress.TryAdd(trackedImage,new TrackedEvenement(false,0f,true));
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            //Debug.Log("Image updated");
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            //Debug.Log("Image Removde");
            if (spawnedPrefabs.TryGetValue(trackedImage.referenceImage.name,
            out (GameObject, EvenementScriptableObject) value))
            {
                value.Item1.SetActive(false);
            }
        }
    }

    void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        trackedImage.transform.GetPositionAndRotation(out Vector3 position, out Quaternion rotation);
        GameObject prefab = spawnedPrefabs[name].Item1;

        if (prefab.activeInHierarchy){
            imagesProgress[trackedImage].progress = 0f;
            return;
        }

        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            Debug.Log("We are tracking"+name);
            imagesProgress[trackedImage].progress += Time.deltaTime;
            onScanProgress.Invoke(imagesProgress[trackedImage].progress);
        }
        else
        {
            imagesProgress[trackedImage].progress = 0f;
        }

        if (imagesProgress[trackedImage].progress > 0.99f)
        {
            imagesProgress[trackedImage].progress = 0f;
            onScanProgress.Invoke(imagesProgress[trackedImage].progress);
            if (spawnedPrefabs[name].Item2.useFlatEvent)
            {
                prefab.SetActive(true);
                backgroundForFlat.gameObject.SetActive(true);
                backgroundForFlat.sprite = spawnedPrefabs[name].Item2.backgroundImageForFlatEvent;
                aRSession.GetComponent<ARSession>().enabled = false;
            }

            prefab.SetActive(true);
        }
        else if (!spawnedPrefabs[name].Item2.useFlatEvent)
        {
            prefab.transform.SetPositionAndRotation(position,rotation);
        }
        /*
                string name = trackedImage.referenceImage.name;
                Vector3 position = trackedImage.transform.position;
                Quaternion rotation = trackedImage.transform.rotation;

                GameObject prefab = spawnedPrefabs[name].Item1;

                if (spawnedPrefabs[name].Item2.useFlatEvent)
                {
                    prefab.SetActive(true);
                    aRSession.GetComponent<ARSession>().enabled = false;
                }
                else
                {
                    prefab.transform.position = position;
                    prefab.transform.rotation = rotation;
                }

                prefab.SetActive(true);
        */
        foreach (var go in spawnedPrefabs.Values)
        {
            if (go.Item1.name != name)
            {
                go.Item1.SetActive(false);
            }
        }
    }
}
