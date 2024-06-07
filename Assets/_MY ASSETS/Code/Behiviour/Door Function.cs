using UnityEngine;

public class DoorFunction : MonoBehaviour
{
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;
    [SerializeField] private BoxCollider boxCollider;

    [SerializeField] private Vector3 leftDoorRotation;
    [SerializeField] private Vector3 rightDoorRotation;

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


    //This function is called when ingame door button is trigger or pressed.
    public void DoorAnimation()
    {
        //Collider is off so ball can pass.
        boxCollider.enabled = false;

        LeanTween.rotateLocal(leftDoor, leftDoorRotation, 0.8f);
        LeanTween.rotateLocal(rightDoor, rightDoorRotation, 0.8f);
    }


    public void resetDoorAnimation()
    {
        //Collider is on beacuse door is closed.
        boxCollider.enabled = true;

        LeanTween.rotateLocal(leftDoor, defaultRotationLeft, 0.8f);
        LeanTween.rotateLocal(rightDoor, defaultRotationRight, 0.8f);
    }
}
