using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public Transform cameraHolder;
    public Camera mainCamera;

    [Header ("<size=15>[SCRIPTABLE OBJECTS]")]
    public LevelData levelData;

    [SerializeField] private Transform traget; 
    public float rotationAngle = 0;
    public float lerpedRotation = 0;
    public float rotationSpeed = 0.2f;
    public float movementSmoothness = 10f;

    public float orthographicValue = 5;
    public float lerpedOrthographicValue = 5;
    public float scaleSpeed = 0.2f;
    public float OrthographicSmoothness = 10f;

    [Space]
    public float minZoomValue = 3;
    public float maxZoomValue = 10;


    private void Awake()
    {
        maxZoomValue = levelData.levelsInformation[levelData.currentLevel].maxZoomLevel;
        minZoomValue = levelData.levelsInformation[levelData.currentLevel].minZoomLevel;
    }

    private void Update()
    {
        //lerpedRotation = Mathf.MoveTowards(lerpedRotation, rotationAngle, Time.deltaTime * movementSmoothness);
        lerpedRotation = rotationAngle;
        cameraHolder.transform.rotation = Quaternion.Euler(0, lerpedRotation, 0);

        orthographicValue = Mathf.Clamp(orthographicValue, minZoomValue, maxZoomValue);
        lerpedOrthographicValue = Mathf.MoveTowards(lerpedOrthographicValue, orthographicValue, Time.deltaTime * OrthographicSmoothness);
        mainCamera.orthographicSize = lerpedOrthographicValue;
    }   

    public void ChangeZoomLevel(float desireZoomLevel)
    {
        mainCamera.orthographicSize = desireZoomLevel;
        orthographicValue = desireZoomLevel;
        lerpedOrthographicValue = desireZoomLevel; 
    }

    public void SetLevelCenterPoint(Vector3 centerPosition)
    {
        mainCamera.transform.SetParent(null);
        cameraHolder.position = centerPosition;
        mainCamera.transform.SetParent(cameraHolder);
    }
}