using UnityEngine;
using System.Collections;

public class BallFunction : MonoBehaviour
{
    Rigidbody playerRB;
    WaitForSeconds cooldownTime = new WaitForSeconds(0.2f);
    SFXManager sfxManager;
    UIManager uiManager;

    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform ballModel;
    [SerializeField] private Transform shadowModel;
    [SerializeField] private GameObject dropShadow;
    [SerializeField] private GameObject nextLevelButton;
    
    [SerializeField] private Vector3 startingPositon;
    [SerializeField] private Vector3 endPosition;


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
    }

    private void Update()
    {
        GroundCheck();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (playerData.cooldownStatus)
        {
            case CooldownStatus.READY:
                if (other.CompareTag (playerData.staffTag))
                {
                    playerRB.velocity = -other.transform.forward * playerData.playerSpeed;
                    playerData.cooldownStatus = CooldownStatus.COOLINGDOWN;

                    sfxManager.PlayHitSound();

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
        playerRB.transform.position = startingPositon;
        dropShadow?.SetActive(true);    
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
}
