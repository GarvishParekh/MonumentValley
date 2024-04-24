using UnityEngine;

[CreateAssetMenu (fileName = "Camera Data", menuName = "Camera Data")]
public class CameraData : ScriptableObject
{
    public float cameraStartingpoint;
    public float cameraGameplayPoint;
    public float cameraEndPoint;
 
    public float animationSpeed = 2;
}
