using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DigicodeButton : Interactable
{
    [SerializeField] string codeButton;
    [SerializeField] bool reset;
    [SerializeField] bool validate;
    [System.NonSerialized]
    public UnityEvent<string,bool,bool> onButtonPressed = new UnityEvent<string,bool,bool>();

    [SerializeField] float zMovement = -0.005f;
    [SerializeField] float pressSpeed = 0.1f;
    Vector3 spawnPos;

    private void Start()
    {
        spawnPos = transform.localPosition;
    }

    public override void OnTouch()
    {
        base.OnTouch();
        onButtonPressed?.Invoke(codeButton, reset, validate);
        transform.localPosition = spawnPos;
        LeanTween.moveLocalZ(gameObject, zMovement, pressSpeed).setEaseInOutBounce().setOnComplete(value =>
        {
            LeanTween.moveLocalZ(gameObject, 0, pressSpeed);
        });
    }
}
