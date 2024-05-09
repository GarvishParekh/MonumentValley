using System;
using UnityEngine;

public class BallFunction : MonoBehaviour
{
    public static Action TrapActivate;

    Rigidbody playerRB;
    
    SFXManager sfxManager;
    UIManager uiManager;

    [Header("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private LevelData levelData;
    [SerializeField] private PlayerData playerData;

    [Header("<size=15>[COMPONENTS]")]
    [SerializeField] private SphereCollider myCollider;
    [SerializeField] private Transform ballModel;
    [SerializeField] private Transform shadowModel;
    [SerializeField] private GameObject dropShadow;

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

