using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class BallFunction : MonoBehaviour
{
    Rigidbody playerRB;
    WaitForSeconds cooldownTime = new WaitForSeconds(0.2f);
    SFXManager sfxManager;
    UIManager uiManager;

    [SerializeField] private LevelData levelData;

    [SerializeField] private SphereCollider myCollider;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform ballModel;
    [SerializeField] private Transform shadowModel;
    [SerializeField] private GameObject dropShadow;
    [SerializeField] private GameObject nextLevelButton;

    [SerializeField] private Vector3 startingPositon;

    LevelInformation currentLevelInfo;


    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        playerData.grounCheck = GrounCheck.ONGOING;
        playerData.cooldownStatus = CooldownStatus.READY;

    }

    private void Start()
    {
        sfxManager = SFXManager.instance;
        uiManager = UIManager.instance;

        if (levelData.currentLevel < levelData.levelsInformation.Length)
        {
            currentLevelInfo = levelData.levelsInformation[levelData.currentLevel];
        }
    }

    private void Update()
    {
        GroundCheck();
        AutoResetCheck();
        UpdatePlayerPosition();
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
                    LeanTween.move(gameObject.gameObject, getEndPoint, 0.4f).setEaseOutSine();
                    LeanTween.moveY(ballModel.gameObject, 2f, 0.2f).setEaseOutSine().setLoopPingPong(1).setOnComplete(()=>
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
                break;
        }

        if (other.CompareTag(playerData.completeTag))
        {
            other.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            LevelComplete(other.transform.GetChild(0).position);
        }
    }

    private IEnumerator CoolingDown()
    {
        yield return cooldownTime;
        playerData.cooldownStatus = CooldownStatus.READY;
    }

    private void GroundCheck()
    {
        switch (playerData.grounCheck)
        {
            case GrounCheck.ONGOING:
                if (!Physics.Raycast(transform.position, -Vector3.up, playerData.rayCastLenght, playerData.groundLayer))
                {
                    myCollider.isTrigger = false;
                    Debug.Log("Fall");
                    playerRB.useGravity = true;
                    dropShadow.SetActive(false);
                }
                break;
        }
    }

    public void ResetBall()
    {
        ResetAnimation();
        StopPlayerMotion();
        playerData.grounCheck = GrounCheck.ONGOING;
        if (currentLevelInfo != null)
        {
            playerRB.transform.position = currentLevelInfo.ballSartingposition;
        }
        dropShadow?.SetActive(true);
        myCollider.isTrigger = true;
        playerRB.isKinematic = true;
        playerRB.isKinematic = false;
    }

    private void ResetAnimation()
    {
        ballModel.localScale = Vector3.zero;
        shadowModel.localScale = Vector3.zero;
        LeanTween.scale(ballModel.gameObject, Vector3.one, 0.3f).setEaseInOutSine();
        LeanTween.scale(shadowModel.gameObject, Vector3.one, 0.3f).setEaseInOutSine();
    }

    public void LevelComplete(Vector3 finalPosition)
    {
        StopPlayerMotion();

        playerData.grounCheck = GrounCheck.STOP;
        playerRB.transform.position = finalPosition;

        dropShadow?.SetActive(true);
        uiManager.LevelCompleteUpdate();

        sfxManager.PlayLevelCompleteSound();
    }

    private void StopPlayerMotion()
    {
        playerRB.velocity = Vector3.zero;
        playerRB.useGravity = false;
    }

    public void ChangeMyPosition(Vector3 desirePosition)
    {
        transform.position = desirePosition;
    }

    public void AutoResetCheck()
    {
        if (transform.position.y < -20)
        {
            ResetBall();
        }
    }

    private void UpdatePlayerPosition()
    {
        playerData.playerPosition = transform.position; 
    }
}
