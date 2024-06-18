using UnityEngine;

public enum GroundAnimation
{
    Default,
    Fall_Trap
}

public enum CameraAnimation
{
    ON_GOING,
    COMPLETED
}

public enum StarStatus
{
    Default,
    Collected
}

[CreateAssetMenu(fileName = "Level Data", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    public CameraAnimation cameraAnimation;
    public GroundAnimation groundAnimation;

    [Space]
    public int currentLevel = 0;
    public float animationSpeed = 0.2f;
    public LevelInformation[] levelsInformation;    
}

[System.Serializable]
public class LevelInformation
{
    [Header ("[ Name ]")]
    public string LevelName;

    [Header ("[ Set Up Values ]")]
    public float minZoomLevel = 4;
    public float startingZoomLevel = 5;
    public float maxZoomLevel = 4;
    public Vector3 levelCenterPoint = Vector3.zero;
    public Vector3 ballSartingposition = Vector3.zero;
}
