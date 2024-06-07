using System.Transactions;
using UnityEngine;

[RequireComponent (typeof(BoxCollider))] 
public class Collectibles : MonoBehaviour
{
    [SerializeField] GameObject blockAnimation;
    [SerializeField] BoxCollider mainCollider;

    private void Awake()
    {
        mainCollider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        BallFunction.BallReset += ResetAnimation;
    }

    private void OnDisable()
    {
        BallFunction.BallReset -= ResetAnimation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainCollider.enabled = false;
            PlayAnimation();
        }
    }

    private void PlayAnimation()
    {
        blockAnimation.GetComponent<IBlockAnimation>().PlayAnimation();
    }

    private void ResetAnimation()
    {
        mainCollider.enabled = true;
        blockAnimation.GetComponent<IBlockAnimation>().RewindAnimation();
    }
}
