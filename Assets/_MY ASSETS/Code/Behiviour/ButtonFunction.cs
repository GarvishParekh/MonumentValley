using UnityEngine;

public class ButtonFunction : MonoBehaviour
{
    [Header (" [ SCRIPTS ] ")]
    [SerializeField] private DoorFunction doorFunction;

    [Header (" [ COMONENTS ] ")]
    [SerializeField] private GameObject button;

    private float animationTime = 0.5f;

    private void OnEnable()
    {
        BallFunction.BallReset += resetButton;
    }

    private void OnDisable()
    {
        BallFunction.BallReset -= resetButton;
    }

    public void buttonPressed()
    {
        LeanTween.moveLocalZ(button, -0.1f, animationTime);

        doorFunction.DoorAnimation();
    }

    public void resetButton()
    {
        LeanTween.moveLocalZ(button, 0, animationTime);
    }
}
