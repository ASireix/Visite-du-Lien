using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody))]
public class TouchRotation : PressInputBase
{
    [SerializeField] float speed;
    [SerializeField] float maxRotationZ;
    bool _isPressed;
    public int xFactor;
    public int yFactor;

    Rigidbody rb;
    [SerializeField] float initialTorque = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPressed)
        {
            rb.AddTorque(Vector3.down * _axisAction.ReadValue<Vector2>().x * speed * Time.deltaTime);
            rb.AddTorque(Vector3.right * _axisAction.ReadValue<Vector2>().y * speed * Time.deltaTime);
        }
    }

    protected override void OnPress(Vector3 position)
    {
        base.OnPress(position);
        _isPressed = true;
        rb.angularVelocity = Vector3.zero;
    }

    protected override void OnPressCancel()
    {
        base.OnPressCancel();
        _isPressed = false;
    }
    
    protected override void OnEnable(){
        base.OnEnable();
        if (!rb) rb = GetComponent<Rigidbody>();
        rb.AddTorque(Vector3.right * initialTorque * Time.deltaTime);
        rb.AddTorque(Vector3.right * initialTorque * Time.deltaTime);
    }

}
