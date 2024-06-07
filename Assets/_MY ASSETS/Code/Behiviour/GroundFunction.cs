using UnityEngine;

public enum GroundType
{
    Normal,
    Moving,
    Rotation
}

public enum MovingGroundDirection
{
    X,
    Y,
    Z
}

public class GroundFunction : MonoBehaviour
{
    public GroundType groundType;
    public MovingGroundDirection groundDirection;
    
    [Header("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private PlayerData playerData;

    [Header("<size=15>[COMPONENTS]")]
    [SerializeField] private Transform movingTile;

    [Header("<size=15>[VALUES]")]
    [SerializeField] private float Degree = 0;
    [SerializeField] private float waveSpeed = 1;
    [SerializeField] private float waveHeight = 1;
    [SerializeField] private float heightOffset = 0;
    [SerializeField] private float rotationValue;
    [SerializeField] private float rotationSpeed;

    private Vector3 tilePositionVector;
    private float groundStartPosition;
    private float sineWave = 0;
    private float cosWave = 0;

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

    private void Update()
    {
        switch (groundType)
        {
            case GroundType.Normal:
                break;

            case GroundType.Moving:
                Debug.Log("Tile is moving");
                    MovingGroundAnimation();
                break;

            case GroundType.Rotation:
                groundRotateAroundAnimation();
                break;

        }
    }


    //This animation is play when player activate Trap.
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


    //Moving Ground Animation according to direction
    private void MovingGroundAnimation()
    {
        float tempSpeed = (Mathf.Abs(sineWave) + 0.2f) * waveSpeed; 
        Degree += Time.deltaTime * tempSpeed;
        sineWave = (Mathf.Sin(Degree) * waveHeight) + heightOffset;

        switch (groundDirection)
        {
            case MovingGroundDirection.X:
                    tilePositionVector.x = sineWave;
                break;

            case MovingGroundDirection.Y:
                    tilePositionVector.y = sineWave;
                break;

            case MovingGroundDirection.Z:
                tilePositionVector.z = sineWave;  
                break;
        }
        transform.localPosition = tilePositionVector;
    }


    //To move ground in circle animation
    float tempSpeed = 0;
    private void groundRotateAroundAnimation()
    {
        //To slow down ground animation at sin0 and cos90 angel
        if (cosWave < 0 && sineWave > 0)
        {
            tempSpeed = ((Mathf.Abs(cosWave) * Mathf.Abs(sineWave)) + 0.05f) * waveSpeed;
        }
        else
        {
            tempSpeed = waveSpeed;
        }

        tempSpeed = Mathf.Clamp(tempSpeed, 0, waveSpeed);
        Degree += Time.deltaTime * tempSpeed;

        sineWave = (Mathf.Sin(Degree) * waveHeight) + heightOffset;
        cosWave = (Mathf.Cos(Degree) * waveHeight) + heightOffset;

        tilePositionVector.x = cosWave;
        tilePositionVector.z = sineWave;

        transform.localPosition = tilePositionVector;
    }
}
