using System;
using UnityEngine;

public enum DistanceType
{
    FAR,
    CLOSE
}

public enum AnimationType
{
    POOL_STICK,
    SPRING,
    FALL,
    GRAB_NET
}

public enum AnimationState
{
    PLAY,
    PAUSE
}

public enum StaffHitState
{
    Not_Hit,
    Hit
}

[CreateAssetMenu (fileName = "Staff Data", menuName = "Staff Data")]
public class StaffData : ScriptableObject
{
    public AnimationState animationState;
    public float distanceThreshold = 4f;


    [Space]
    public float startAnimationSpeed = 0.2f;
    public float endAnimationSpeed = 0.2f;
}

