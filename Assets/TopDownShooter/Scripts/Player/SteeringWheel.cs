using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SteeringWheel : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public InputSystem inputSystem;
    [Space]
    public bool WheelBeingHeld = false;
    public RectTransform wheel;
    public float wheelAngle = 0f;
    private float LastWheelAngle = 0f;

    private Vector2 center;
    public float MaxSteerAngle;
    public float ReleaseSpeed = 300f;
    public float output;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!WheelBeingHeld && wheelAngle != 0f)
        {
            float DeltaAngle = ReleaseSpeed * Time.deltaTime;
            if (Mathf.Abs(DeltaAngle) > Mathf.Abs(wheelAngle))
                wheelAngle = 0f;
            else if (wheelAngle > 0f)
                wheelAngle -= DeltaAngle;
            else
                wheelAngle += DeltaAngle;
        }
        wheel.localEulerAngles = new Vector3(0, 0, -wheelAngle);
        output = wheelAngle / MaxSteerAngle;

        inputSystem.Steer = output;
    }

    public void OnPointerDown(PointerEventData data)
    {
        WheelBeingHeld = true;

        center = RectTransformUtility.WorldToScreenPoint(data.pressEventCamera, wheel.position);
        LastWheelAngle = Vector2.Angle(Vector2.up, data.position - center);
    }

    public void OnDrag(PointerEventData data)
    {
        float newAngle = Vector2.Angle(Vector2.up, data.position - center);
        if((data.position - center).sqrMagnitude >= 400)
        {
            if(data.position.x > center.x)
            {
                wheelAngle += newAngle - LastWheelAngle;
            }else
            {
                wheelAngle -= newAngle - LastWheelAngle;
            }
        }

        wheelAngle = Mathf.Clamp(wheelAngle, -MaxSteerAngle, MaxSteerAngle);
        LastWheelAngle = newAngle;
    }

    public void OnPointerUp(PointerEventData data)
    {
        OnDrag(data);
        WheelBeingHeld = false;
    }
}
