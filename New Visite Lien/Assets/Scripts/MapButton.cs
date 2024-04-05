using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}
