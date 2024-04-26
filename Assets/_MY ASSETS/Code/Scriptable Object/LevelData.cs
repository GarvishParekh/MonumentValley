using UnityEngine;

public enum CameraAnimation
{
    ON_GOING,
    COMPLETED
}

[CreateAssetMenu(fileName = "Level Data", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    public CameraAnimation cameraAnimation;

    [Space]
    public int currentLevel = 0;
    public float animationSpeed = 0.2f;
    public LevelInformation[] levelsInformation;    
}

[System.Serializable]
public class LevelInformation
{
    public string LevelName;
    public float startingZoomLevel = 5;
    public Vector3 levelCenterPoint = Vector3.zero;
    public Vector3 ballSartingposition = Vector3.zero;
}
