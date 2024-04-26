using UnityEngine;

public enum DistanceType
{
    FAR,
    CLOSE
}
public enum AnimationType
{
    POOL_STICK,
    SPRING
}

[CreateAssetMenu (fileName = "Staff Data", menuName = "Staff Data")]
public class StaffData : ScriptableObject
{
    public float distanceThreshold = 5f;

    [Space]
    public float startAnimationSpeed = 0.2f;
    public float endAnimationSpeed = 0.2f;
}
