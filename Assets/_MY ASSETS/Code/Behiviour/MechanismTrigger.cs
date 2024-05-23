using UnityEngine;
using System.Collections;
using UnityEngine.Splines;

[RequireComponent(typeof(BallFunction))]
public class MechanismTrigger : MonoBehaviour
{
    Rigidbody playerRB;
    BallFunction ballFunction;  

    [Header ("<size=15>[SCRIPT]")]
    SFXManager sfxManager;
    PortalFunction portalFunction;

    [Header ("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private PlayerData playerData;

    [Header("<size=15>[COMPONENTS]")]
    [SerializeField] private Transform ballModel;
    //[SerializeField] private SplineAnimate splineAnimate;

    [Header("<size=15>[SHADER VALUES]")]
    [SerializeField] private Material shaderMat;
    [SerializeField] private float dissolveValue = 0.5f;

    [SerializeField] private float shaderShowvalue = 0.5f;
    [SerializeField] private float shaderHidevalue = -0.5f;

    [SerializeField] private float dissolveSpeed = 1.0f;

    WaitForSeconds cooldownTime = new WaitForSeconds(0.2f);

    private void Awake()
    {
        ballFunction = GetComponent<BallFunction>();    
        playerRB = GetComponent<Rigidbody>();

        Changeshadervalue(shaderShowvalue);
        //Time.timeScale = 0.2f;
    }

    private void Start()
    {
        sfxManager = SFXManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (playerData.cooldownStatus)
        {
            case CooldownStatus.READY:
                if (other.CompareTag(playerData.staffTag))
                {
                    playerRB.velocity = -other.transform.forward * playerData.playerSpeed;
                    playerData.cooldownStatus = CooldownStatus.COOLINGDOWN;

                    sfxManager.PlayHitSound();

                    StartCoroutine(nameof(CoolingDown));
                }
                else if (other.CompareTag(playerData.springTag))
                {
                    // manager velocity
                    Vector3 lastVelocity = playerRB.velocity;
                    playerRB.velocity = Vector3.zero;

                    // get jump end point
                    Vector3 getEndPoint = other.GetComponent<StaffFunction>().GetEndPosition();
                    playerData.grounCheck = GrounCheck.STOP;

                    // jump animation
                    LeanTween.move(gameObject, getEndPoint, 0.4f).setEaseOutSine();
                    LeanTween.moveY(ballModel.gameObject, 2f, 0.2f).setEaseOutSine().setLoopPingPong(1).setOnComplete(() =>
                    {
                        // restore velocity
                        playerRB.velocity = lastVelocity;
                        playerData.grounCheck = GrounCheck.ONGOING;
                    });
                    playerData.cooldownStatus = CooldownStatus.COOLINGDOWN;

                    // play sound
                    sfxManager.PlayJumpSound();
                    StartCoroutine(nameof(CoolingDown));
                }
                else if (other.CompareTag(playerData.fallTag))
                {
                    StaffFunction staffFunction = other.GetComponent<StaffFunction>();
                    
                    // manager velocity
                    Vector3 lastVelocity = playerRB.velocity;
                    playerRB.velocity = Vector3.zero;

                    // get jump end point
                    Vector3 getEndPoint = staffFunction.GetEndPosition();
                    playerData.grounCheck = GrounCheck.STOP;

                    // jump animation
                    LeanTween.moveX(gameObject, getEndPoint.x, staffFunction.GetDropSpeed());
                    LeanTween.moveZ(gameObject, getEndPoint.z, staffFunction.GetDropSpeed());
                    LeanTween.moveY(gameObject, getEndPoint.y, staffFunction.GetDropSpeed()).setEaseInSine().setOnComplete(() =>
                    {
                        // restore velocity
                        playerRB.velocity = lastVelocity;
                        playerData.grounCheck = GrounCheck.ONGOING;
                    });
                    playerData.cooldownStatus = CooldownStatus.COOLINGDOWN;

                    // play sound
                    // play fall sound

                    StartCoroutine(nameof(CoolingDown));
                }

                //Trap Fall Animation
                else if (other.CompareTag(playerData.trapTag))
                {
                    switch (playerData.playerTrap)
                    {
                        case PlayerTrap.FREE:
                            Debug.Log("Trap");
                    
                            BallFunction.TrapActivate?.Invoke();
                            playerRB.velocity = Vector3.zero;

                            playerData.playerTrap = PlayerTrap.TRAPPED;
                        break;
                    }
                }

                // PORTAL EFFECT
                else if (other.CompareTag(playerData.portalTag))
                {
                    StartCoroutine(nameof(ProtalEffect), other);
                }

                //Star Collect...
                else if (other.CompareTag(playerData.starTag))
                {
                    StarIdentity starIdentity = other.GetComponentInParent<StarIdentity>();
                    starIdentity.starStatus = StarStatus.Collected;
                }

                //Ground Rotate Effect
                else if(other.CompareTag(playerData.holdTag))
                {
                    playerRB.velocity = Vector3.zero;

                    HoldFunction holdFunction = other.GetComponentInParent<HoldFunction>();
                   
                    transform.SetParent(holdFunction.GetParent());
                    transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 1);
                    
                    holdFunction.PlayAnimation();
                }

                //Bezier Curve Effect
                else if (other.CompareTag(playerData.bezierCurve))
                {
                    Debug.Log("Turn START");

                    Bezier bezire = other.GetComponentInParent<Bezier>();
                    SplineAnimate splineAnimate = bezire.GetSpline();

                    splineAnimate.enabled = true;

                    playerRB.velocity = Vector3.zero;
                    playerData.grounCheck = GrounCheck.STOP;

                    splineAnimate.Restart(true);
                    transform.SetParent(bezire.GetParent());
                    transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 1);

                    endDirection = bezire.GetEndDirection();
                    StartCoroutine(nameof(BezierAnimation), splineAnimate);        
                }

                else if (other.CompareTag(playerData.buttonTag))
                {
                    ButtonFunction buttonFunction = other.GetComponentInParent<ButtonFunction>();

                    buttonFunction.buttonPressed();
                }
                break;
        }

        if (other.CompareTag(playerData.completeTag))
        {
            other.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            ballFunction.LevelComplete(other.transform.GetChild(0).position);
        }

    }

    Transform endDirection;
    IEnumerator BezierAnimation(SplineAnimate sa)
    {
        bool isplaying = true;
        while (isplaying != false)
        {
            Debug.Log($"Animation is playing");
            Debug.Log(isplaying);

            isplaying = sa.IsPlaying;
            yield return null;
        }
        Debug.Log($"Animation completed");
        playerData.grounCheck = GrounCheck.ONGOING;
        
        transform.parent = null;
        playerRB.velocity = endDirection.forward * playerData.playerSpeed;
    }

    private IEnumerator ProtalEffect(Collider other)
    {
        portalFunction = other.GetComponent<PortalFunction>();

        ballModel.rotation = Quaternion.Euler(portalFunction.GetStartPortalTransform().rotation.eulerAngles);

        playerData.grounCheck = GrounCheck.STOP;
        while (dissolveValue != shaderHidevalue)
        {
            dissolveValue = Mathf.MoveTowards(dissolveValue, shaderHidevalue, Time.deltaTime * dissolveSpeed);
            Changeshadervalue(dissolveValue);

            yield return null;
        }

        TeleportPosition(other);
        playerData.grounCheck = GrounCheck.ONGOING;

        while (dissolveValue != shaderShowvalue)
        {
            dissolveValue = Mathf.MoveTowards(dissolveValue, shaderShowvalue, Time.deltaTime * dissolveSpeed);
            Changeshadervalue(dissolveValue);

            yield return null;
        }
    }

    private void TeleportPosition(Collider other)
    {
        //playerRB.velocity = Vector3.zero;

        Transform endPoint = portalFunction?.portalEndPoint;

        transform.position = endPoint.position;
        playerRB.velocity = endPoint.forward * playerData.playerSpeed;

        ballModel.rotation = Quaternion.Euler(portalFunction.GetEndPortalTransform().rotation.eulerAngles);
    }
    
    private void Changeshadervalue(float _values)
    {
        shaderMat.SetFloat("_DissolveValue", _values);
        Debug.Log(shaderMat.GetFloat("_DissolveValue"));
    }

    private IEnumerator CoolingDown()
    {
        yield return cooldownTime;
        playerData.cooldownStatus = CooldownStatus.READY;
    }
}
