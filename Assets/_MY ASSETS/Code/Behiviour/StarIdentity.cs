using UnityEngine;

public class StarIdentity : MonoBehaviour
{
    SFXManager sfxManager;
    private float degree = 0;

    Vector3 starPosition;
    float sineWave = 0;

    private enum CollectedStatus
    {
        NO,
        YES
    }

    private enum ConsumedStatus
    {
        NO,
        YES
    }
    [SerializeField] private CollectedStatus collectedStatus;
    [SerializeField] private ConsumedStatus consumedStatus;

    [Space]
    [SerializeField] private GameObject star;
    [SerializeField] private GameObject startMeshHolder;
    [SerializeField] private float rotationSpeed;

    [Space]
    [SerializeField] private float waveSpeed = 1;
    [SerializeField] private float waveHeight = 1;
    [SerializeField] private float heightOffset = 0;

    [Space]
    [SerializeField] private string playerPrefTag = string.Empty;

    private void OnEnable()
    {
        BallFunction.LevelCleared += OnLevelComplete;
        BallFunction.ResetGround += OnLevelReset;   
    }

    private void OnDisable()
    {
        BallFunction.LevelCleared -= OnLevelComplete;
        BallFunction.ResetGround -= OnLevelReset;   
    }

    private void Awake()
    {
        SetPlayerPref();
        SetConsumedStatus();

        switch (consumedStatus)
        {
            case ConsumedStatus.YES:
                this.gameObject.SetActive(false);
                break;
            case ConsumedStatus.NO:
                this.gameObject.SetActive(true);
                break;
        }
    }

    private void Start()
    {
        sfxManager = SFXManager.instance;
    }

    private void SetConsumedStatus()
    {
        int lifeInt = PlayerPrefs.GetInt(playerPrefTag, 0);
        
        if (lifeInt == 0)
            consumedStatus = ConsumedStatus.NO;
        
        else if (lifeInt == 1)
            consumedStatus = ConsumedStatus.YES;
    }

    private void Update()
    {
        StarAnimation();
        WaveAnimation();
    }

    private void StarAnimation()
    {
        star.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    
    private void WaveAnimation()
    {
        degree += Time.deltaTime * waveSpeed;
        sineWave = (Mathf.Sin(degree) * waveHeight) + heightOffset;

        starPosition.y = sineWave;  
        star.transform.localPosition = starPosition;
    }

    [ContextMenu("NOTE PLAYERPREF TAG")]
    private void SetPlayerPref()
    {
        playerPrefTag = transform.parent.parent.name + "_" + transform.GetSiblingIndex().ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collectedStatus = CollectedStatus.YES;

            startMeshHolder.SetActive(false);
            sfxManager.PlayStarCollectionSound();
        }
    }

    private void AddCountToLevel()
    {
        int currentUnlockCount = PlayerPrefs.GetInt(ConstantKeys.LEVEL_UNLOCKED, 0);
        currentUnlockCount += 1;
        PlayerPrefs.SetInt(ConstantKeys.LEVEL_UNLOCKED, currentUnlockCount);
    }

    private void OnLevelComplete()
    {
        switch (collectedStatus)
        {
            case CollectedStatus.YES:
                PlayerPrefs.SetInt(playerPrefTag, 1);
                consumedStatus = ConsumedStatus.YES;
                AddCountToLevel();
                break;
        }
    }

    private void OnLevelReset()
    {
        switch (consumedStatus)
        {
            case ConsumedStatus.NO:
                collectedStatus = CollectedStatus.NO;
                startMeshHolder.SetActive(true);
                break;
        }
    }
}
