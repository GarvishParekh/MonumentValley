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
    [SerializeField] private float neededRotationValue;

    private void OnEnable()
    {
        BallFunction.RotateGround += ManageGroundRotation;
    }

    private void OnDisable()
    {
        BallFunction.RotateGround -= ManageGroundRotation;
    }

    private void ManageGroundRotation()
    {
        switch (rotationStatus)
        {
            case GroundRotationStatus.Default:
                RotateGorund(neededRotationValue);
                rotationStatus = GroundRotationStatus.Rotated;
                break;

            case GroundRotationStatus.Rotated:
                RotateGorund(defaultRotationValue);
                rotationStatus = GroundRotationStatus.Default;
                break;
        }
    }

    private void RotateGorund(float _rotationValue)
    {
        Debug.Log(_rotationValue);
        LeanTween.rotateY(groundRotationHolder, _rotationValue, 0.8f);
    }

}
