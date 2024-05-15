using UnityEngine;

public enum GroundRotationStatus
{
    Default,
    Rotated
}

public class GroundRotation : MonoBehaviour
{
    public GroundRotationStatus rotationStatus; 

    [Header ("[ COMPONENTS ]")]
    [SerializeField] private GameObject groundRotationHolder;

    [Header ("[ VALUES ]")]
    [SerializeField] private float defaultRotationValue;
    [SerializeField] private float finalRotationValue;

    Vector3 rotationToApply;

    private void OnEnable()
    {
        BallFunction.RotateGround += ManageGroundRotation;
    }

    private void OnDisable()
    {
        BallFunction.RotateGround -= ManageGroundRotation;
    }
    /// <summary>
    /// Only when user hits restart or falls
    /// </summary>
    private void ManageGroundRotation()
    {
        RotateGorund(GroundRotationStatus.Default);
    }

    public void RotateLevel()
    {
        RotateGorund(GroundRotationStatus.Rotated);
    }

    private void RotateGorund(GroundRotationStatus status)
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
        LeanTween.rotate(groundRotationHolder, rotationToApply, 0.8f);
    }
}
