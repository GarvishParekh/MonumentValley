using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : EventTrigger
{
    private enum DragType
    {
        ROTATION,
        ZOOMING
    }

    private DragType dragType; 
    private CameraSettings cameraSettings;

    float XStartingDelta = 0;
    float YStartingDelta = 0;

    float XEndingDelta = 0;
    float YEndingDelta = 0;

    float XdragDelta = 0;
    float YdragDelta = 0;

    private void Start()
    {
        cameraSettings = GetComponent<CameraSettings>();    
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        XStartingDelta = 0;
        YStartingDelta = 0;

        XEndingDelta = 0;
        YEndingDelta = 0;

        XdragDelta = 0;
        YdragDelta = 0;

        XStartingDelta = Mathf.Abs(eventData.delta.x);
        XStartingDelta = Mathf.Abs(eventData.delta.y);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        XEndingDelta = Mathf.Abs(eventData.delta.x);
        YEndingDelta = Mathf.Abs(eventData.delta.y);

        XdragDelta = XEndingDelta - XStartingDelta;
        YdragDelta = YEndingDelta - YStartingDelta;

        if (XdragDelta > YdragDelta)
        {
            dragType = DragType.ROTATION;
        }
        else
        {
            dragType = DragType.ZOOMING;
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        switch (dragType)
        {
            case DragType.ROTATION:
                cameraSettings.rotationAngle += eventData.delta.x * cameraSettings.rotationSpeed * Time.deltaTime;
                break;
            case DragType.ZOOMING:
                cameraSettings.orthographicValue += eventData.delta.y * cameraSettings.scaleSpeed * Time.deltaTime;
                break;
        }
    }
}
