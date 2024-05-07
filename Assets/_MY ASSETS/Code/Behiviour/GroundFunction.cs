using UnityEngine;

public class GroundFunction : MonoBehaviour
{
    [Header("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private PlayerData playerData;

    [SerializeField] private float distanceFromPlayer;

    private Vector3 playerPosition;
    private Vector3 myStartPosition;

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
        myStartPosition = transform.position * 2;
    }

    private void Update()
    {
        CalculateDistanceFromPlayer();
    }

    private void CalculateDistanceFromPlayer()
    {
        playerPosition = playerData.playerPosition;

        distanceFromPlayer = Vector3.Distance(transform.position, playerPosition);
    }

    
    private void GroundAnimation()
    {
        int animationLoopcount = 5;

        float vibrationAnimationSpeed = 0.08f;
        //float vibrationAnimationDelay = distanceFromPlayer * 0.1f;
        float vibrationAnimationDelay = Random.Range(0.1f, 0.5f);
        float fallingAnimationSpeed = Random.Range(0.8f, 1.15f);

        Debug.Log("Play Animation");
        LeanTween.moveLocalY(gameObject, 0.12f, vibrationAnimationSpeed).setEaseOutSine().setLoopPingPong(animationLoopcount).setDelay(vibrationAnimationDelay).setOnComplete(() =>
        {
            LeanTween.moveLocalY(gameObject, -10, fallingAnimationSpeed).setEaseInOutSine();
        }); 
        //LeanTween.moveY(gameObject, -10, distanceFromPlayer / 0.8f).setEaseShake();
    }
}
