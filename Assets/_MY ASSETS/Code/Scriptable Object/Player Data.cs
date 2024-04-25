using UnityEngine;

public enum CooldownStatus
{
    READY,
    COOLINGDOWN
}

public enum GrounCheck
{
    ONGOING,
    STOP
}


[CreateAssetMenu(fileName = "Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public CooldownStatus cooldownStatus;
    public GrounCheck grounCheck;
    public float playerSpeed;

    [Header ("<size=15>[ RAYCAST DETAILS ]")]
    public float rayCastLenght = 2;

    [Header ("<size=15>[ COLLISION DETAILS ]")]
    public LayerMask groundLayer;
    public string staffTag;
    public string springTag;
    public string completeTag;
}
