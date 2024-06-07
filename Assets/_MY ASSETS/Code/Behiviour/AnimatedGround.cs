using UnityEngine;

public class AnimatedGround : MonoBehaviour, IBlockAnimation
{
    Vector3 defaultPosition;
    [SerializeField] private Vector3 finalPosition;

    [SerializeField] private float animationDelay;
    [SerializeField] private float reverseAnimationDelay;

    private void Awake()
    {
        defaultPosition = transform.position;
        //animationDelay = transform.GetSiblingIndex() / 5;
    }

    public void PlayAnimation()
    {
        LeanTween.move(gameObject, finalPosition, 0.8f).setEaseInOutSine().setDelay(animationDelay);
    }

    public void RewindAnimation()
    {
        LeanTween.move(gameObject, defaultPosition, 0.8f).setEaseInOutSine().setDelay(reverseAnimationDelay);
    }


    [ContextMenu("GetFinalPosition")]
    public void GetFinalPosition()
    {
        finalPosition = transform.position;
    }

    [ContextMenu("Set default position")]
    public void SetDefault()
    {
        defaultPosition = transform.position;
    }

    [ContextMenu("Get to default position")]
    public void GetToDefaultPosition()
    {
        transform.position = defaultPosition;   
    }

}
