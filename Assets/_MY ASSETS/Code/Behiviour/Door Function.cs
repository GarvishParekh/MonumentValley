using UnityEngine;

public class DoorFunction : MonoBehaviour
{
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;

    public Vector3 leftDoorRotation;
    public Vector3 rightDoorRotation;

    [SerializeField] private Vector3 defaultRotationLeft;
    [SerializeField] private Vector3 defaultRotationRight;



    private void OnEnable()
    {
        BallFunction.BallReset += resetDoorAnimation;
    }

    private void OnDisable()
    {
        BallFunction.BallReset -= resetDoorAnimation;   
    }


    public void DoorAnimation()
    {
        LeanTween.rotateLocal(leftDoor, leftDoorRotation, 0.8f);
        LeanTween.rotateLocal(rightDoor, rightDoorRotation, 0.8f);
    }


    public void resetDoorAnimation()
    {
        LeanTween.rotateLocal(leftDoor, defaultRotationLeft, 0.8f);
        LeanTween.rotateLocal(rightDoor, defaultRotationRight, 0.8f);
    }
}
