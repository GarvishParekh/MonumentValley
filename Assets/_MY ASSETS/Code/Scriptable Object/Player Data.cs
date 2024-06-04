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

public enum PlayerTrap
{
    TRAPPED,
    FREE
}

public enum GrabStatus
{
    Grabbed,
    NORMAL
}

public enum PlayerDirection
{
    X,
    Y,
    Z
}

[CreateAssetMenu(fileName = "Player Data", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public PlayerDirection playerDirection;
    public CooldownStatus cooldownStatus;
    public GrounCheck grounCheck;
    public PlayerTrap playerTrap;
    public GrabStatus grabStatus;   
    public float playerSpeed;

    public Vector3 playerPosition;

    [Header ("<size=15>[ RAYCAST DETAILS ]")]
    public float rayCastLenght = 2;

    [Header ("<size=15>[ COLLISION DETAILS ]")]
    public LayerMask groundLayer;
    public string staffTag;
    public string springTag;
    public string fallTag;
    public string completeTag;
    public string trapTag;
    public string portalTag;
    public string shaderTriggerTag;
    public string starTag;
    public string holdTag;
    public string bezierCurve;
    public string buttonTag;
    public string transporterTag;
    public string centerPointTag;
    public string reverseTag;
    public string doorClosedTag;
}
