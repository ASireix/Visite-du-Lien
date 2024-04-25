using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISocial : MonoBehaviour
{
    public void OpenLink(string url)
    {
        Application.OpenURL($"http://{url}");
    }
}
