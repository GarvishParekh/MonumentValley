using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : EventTrigger
{
    private CameraSettings cameraSettings;
    float dragDelta = 0;

    private void Start()
    {
        cameraSettings = GetComponent<CameraSettings>();    
    }

    public override void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData);
        cameraSettings.rotationAngle += eventData.delta.x * cameraSettings.rotationSpeed * Time.deltaTime;
        cameraSettings.orthographicValue += eventData.delta.y * cameraSettings.scaleSpeed * Time.deltaTime;
    }
}
