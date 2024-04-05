using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class GPS : MonoBehaviour
{
    public float latitude;
    public TextMeshProUGUI latText;
    public float longitude;
    public TextMeshProUGUI lonText;
    public TextMeshProUGUI debugText;
    public bool isUpdating;

    [SerializeField] List<float> qrCodePositions;
    Dictionary<int, (float, float)> qrPositionDico;

    private void Start()
    {
        UpdateLocation();   
    }

    public void UpdateLocation()
    {
        if (!isUpdating)
        {
            isUpdating = true;
            StartCoroutine(GetLocation());
            
        }
    }
    IEnumerator GetLocation()
    {
        while (isUpdating)
        {
            debugText.text = "Request perm";
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
                Permission.RequestUserPermission(Permission.CoarseLocation);
            }
            // First, check if user has location service enabled
            if (!Input.location.isEnabledByUser)
            {
                yield return new WaitForSeconds(10);
                debugText.text = "Location is not enabled";
            }

            debugText.text = "Starting input location";
            // Start service before querying location
            Input.location.Start();

            // Wait until service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // Service didn't initialize in 20 seconds
            if (maxWait < 1)
            {
                print("Timed out");
                debugText.text = "timeout";
                yield break;
            }

            // Connection has failed
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                print("Unable to determine device location");
                debugText.text = "Unable to determine location";
                yield break;
            }
            else
            {
                latitude = Input.location.lastData.latitude;
                latText.text = latitude + "";
                longitude = Input.location.lastData.longitude;
                lonText.text = longitude + "";
                // Access granted and location value could be retrieved
                print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
                debugText.text = "we found the location";
            }

        }
        Input.location.Stop();
        isUpdating = false;
    }
}
