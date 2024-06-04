using UnityEngine;

public class GroundFunction : MonoBehaviour
{
    [Header("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private PlayerData playerData;

    private float groundStartPosition;
    
    private void OnEnable()
    {
        BallFunction.TrapActivate += GroundAnimation;
    }

    private void OnDisable()
    {
        BallFunction.TrapActivate -= GroundAnimation;
    }

    private void Start()
    {
        groundStartPosition = transform.localPosition.y;
    }
    

    private void GroundAnimation(Material _TrapMat)
    {
        Debug.Log("Animation method called");
        int animationLoopcount = 5;

        float vibrationAnimationSpeed = 0.08f;
        float vibrationAnimationDelay = Random.Range(0.1f, 0.5f);
        float fallingAnimationSpeed = Random.Range(0.8f, 1.15f);
        float resetAnimationSpeed = 0.8f;

        //First Vibration animation
        LeanTween.moveLocalY(gameObject, 0.12f, vibrationAnimationSpeed).setEaseOutSine().setLoopPingPong(animationLoopcount).setDelay(vibrationAnimationDelay).setOnComplete(() =>
        {
            Debug.Log("First Animation");

            //Second Fall Animation
            LeanTween.moveLocalY(gameObject, -20, fallingAnimationSpeed).setEaseInOutSine().setOnComplete(() =>
            {
                Debug.Log("Second Animation");
                //Third back to start position Animation
                LeanTween.moveLocalY(gameObject, groundStartPosition, resetAnimationSpeed).setEaseInOutSine().setOnComplete(() =>
                {
                    Debug.Log("Third Animation");
                    _TrapMat.SetFloat("_Intensity", 0.2f);
                });
            });
        }); 
    }
}
