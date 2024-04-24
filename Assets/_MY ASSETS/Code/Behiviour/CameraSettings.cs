using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public Transform cameraHolder;
    public Camera mainCamera;

    public float rotationAngle = 0;
    public float lerpedRotation = 0;
    public float rotationSpeed = 0.2f;
    public float movementSmoothness = 10f;

    public float orthographicValue = 5;
    public float lerpedOrthographicValue = 5;
    public float scaleSpeed = 0.2f;
    public float OrthographicSmoothness = 10f;

    [Space]
    public float minZoomValue = 2;
    public float maxZoomValue = 10;

    
    private void Update()
    {
        //lerpedRotation = Mathf.MoveTowards(lerpedRotation, rotationAngle, Time.deltaTime * movementSmoothness);
        lerpedRotation = rotationAngle;
        cameraHolder.transform.rotation = Quaternion.Euler(0, lerpedRotation, 0);

        orthographicValue = Mathf.Clamp(orthographicValue, minZoomValue, maxZoomValue);
        lerpedOrthographicValue = Mathf.MoveTowards(lerpedOrthographicValue, orthographicValue, Time.deltaTime * OrthographicSmoothness);
        mainCamera.orthographicSize = lerpedOrthographicValue;
    }
}