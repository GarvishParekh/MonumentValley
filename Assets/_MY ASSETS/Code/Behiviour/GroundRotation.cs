using UnityEngine;

public class GroundRotation : MonoBehaviour
{
    [SerializeField] private GameObject groundRotationHolder;

    private void OnEnable()
    {
        BallFunction.RotateGround += RotateGorund;
    }

    private void OnDisable()
    {
        BallFunction.RotateGround -= RotateGorund;
    }

    private void RotateGorund(float _rotationValue)
    {
        Debug.Log(_rotationValue);
        LeanTween.rotateY(groundRotationHolder, _rotationValue, 0.8f);
    }

}
