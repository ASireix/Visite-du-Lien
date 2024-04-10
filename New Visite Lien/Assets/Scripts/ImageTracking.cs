using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracking : MonoBehaviour
{
    [SerializeField] GameObject[] placeablePrefabs;

    Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    public ARTrackedImageManager trackedImageManager;
    [SerializeField] XRReferenceImageLibrary runtimeImageLibrary;

    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        

        foreach (var item in placeablePrefabs)
        {
            GameObject newPrefab = Instantiate(item, Vector3.zero, Quaternion.identity);
            newPrefab.name = item.name;
            spawnedPrefabs.Add(item.name, newPrefab);
            newPrefab.SetActive(false);
        }
    }

    private void Start()
    {
        UpdateImageLibrary(runtimeImageLibrary);
    }


    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
        Debug.Log("Subscribing to image manager");
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
        Debug.Log("Unsubscribing to image manager");
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
            if (spawnedPrefabs.TryGetValue(trackedImage.referenceImage.name, out GameObject value))
            {
                value.SetActive(false);
            }
        }
    }

    void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;
        Quaternion rotation = trackedImage.transform.rotation;

        GameObject prefab = spawnedPrefabs[name];
        prefab.transform.position = position;
        prefab.transform.rotation = rotation;
        prefab.SetActive(true);

        foreach (var go in spawnedPrefabs.Values)
        {
            if (go.name != name)
            {
                go.SetActive(false);
            }
        }
    }
}
