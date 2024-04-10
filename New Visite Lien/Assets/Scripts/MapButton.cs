using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class MapButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int iD;
    Image img;

    public void OnPointerClick(PointerEventData eventData)
    {
        InfoManager.instance.ShowInfo(iD);
    }

    public void SetSprite(Sprite sprite)
    {
        img.sprite = sprite;
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
        //transform.forward = Camera.main.transform.forward;
    }
}
