using UnityEngine;


public enum GroundRotationStatus
{
    Default,
    Rotated
}

public enum GroundLiftStatus
{
    Default,
    Lifted
}

public enum HoldAnimationType
{
    RotateGround,
    LiftGround
}


public class HoldFunction : MonoBehaviour
{
    public HoldAnimationType animationType;

    [Header("[ SCRIPTABLE OBJECT ")]
    [SerializeField] private StaffData saffdata;

    [Header ("[ COMPONENTS ]")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerCenterPoint;
    [SerializeField] private GameObject groundRotationHolder;
    [SerializeField] private GameObject groundLiftHolder;


    [Header ("[ VALUES ]")]
    [SerializeField] private float defaultRotationValue;
    [SerializeField] private float finalRotationValue;
    [Space]
    [SerializeField] private float defaultLiftValue;
    [SerializeField] private float finalLiftValue;

    Vector3 rotationToApply;
    Vector3 positionToApply;

    private void OnEnable()
    {
        BallFunction.ResetGround += ResetGroundAnimation;
    }

    private void OnDisable()
    {
        BallFunction.ResetGround -= ResetGroundAnimation;
    }
    
    
    public void PlayAnimation()
    {
        switch (animationType)
        {
            case HoldAnimationType.RotateGround:
                RotateGround(GroundRotationStatus.Rotated);
                break;

            case HoldAnimationType.LiftGround:
                LiftGround(GroundLiftStatus.Lifted);
                break;
        }
    }

    public Transform GetParent()
    {
        return playerCenterPoint;
    }

    /// <summary>
    /// Only when user hits restart or falls
    /// </summary>
    private void ResetGroundAnimation()
    {
        switch (animationType)
        {
            case HoldAnimationType.RotateGround:
                RotateGround(GroundRotationStatus.Default);
                break;

            case HoldAnimationType.LiftGround:
                LiftGround(GroundLiftStatus.Default);
                break;
        }
    }

    //For Rotation Animation
    private void RotateGround(GroundRotationStatus status)
    {
        switch (status)
        {
            case GroundRotationStatus.Default:
                rotationToApply.y = defaultRotationValue;
                break;

            case GroundRotationStatus.Rotated:
                rotationToApply.y = finalRotationValue;
                break;
        }
        saffdata.animationState = AnimationState.PAUSE;
        LeanTween.rotate(groundRotationHolder, rotationToApply, 0.8f).setOnComplete(() =>
        {
            player.parent = null;
            saffdata.animationState = AnimationState.PLAY;
        });
    }

    //For Lift Animation
    private void LiftGround(GroundLiftStatus status)
    {
        switch (status)
        {
            case GroundLiftStatus.Default:
                positionToApply.y = defaultLiftValue;
                break;

            case GroundLiftStatus.Lifted:
                positionToApply.y = finalLiftValue;
                break;
        }
        saffdata.animationState = AnimationState.PAUSE;
        LeanTween.moveLocalY(groundLiftHolder, positionToApply.y, 0.8f).setOnComplete(() =>
        {
            player.parent = null;
            saffdata.animationState = AnimationState.PLAY;
        });
    }
}
