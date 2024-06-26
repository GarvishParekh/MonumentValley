using UnityEngine;

public class StarIdentity : MonoBehaviour
{
    SFXManager sfxManager;
    private enum LifeStatus
    {
        ALIVE,
        CONSUMED
    }
    private LifeStatus lifeStatus;
    private GameObject star;
    private float degree = 0;

    Vector3 starPosition;
    float sineWave = 0;


    [SerializeField] private float rotationSpeed;

    [Space]
    [SerializeField] private float waveSpeed = 1;
    [SerializeField] private float waveHeight = 1;
    [SerializeField] private float heightOffset = 0;

    [SerializeField] private string playerPrefTag = string.Empty;


    private void Awake()
    {
        star = transform.GetChild(0).gameObject;

        NotePlayerPref();
        SetLifeStatus();

        switch (lifeStatus)
        {
            case LifeStatus.ALIVE:
                this.gameObject.SetActive(true);
                break;
            case LifeStatus.CONSUMED:
                this.gameObject.SetActive(false);
                break;
        }
    }

    private void Start()
    {
        sfxManager = SFXManager.instance;
    }

    private void SetLifeStatus()
    {
        int lifeInt = PlayerPrefs.GetInt(playerPrefTag, 0);
        
        if (lifeInt == 0)
            lifeStatus = LifeStatus.ALIVE;
        
        else if (lifeInt == 1)
            lifeStatus = LifeStatus.CONSUMED;
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
    private void NotePlayerPref()
    {
        playerPrefTag = transform.parent.parent.name + "_" + transform.GetSiblingIndex().ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt(playerPrefTag, 1);
            AddCountToLevel();

            this.gameObject.SetActive(false);
            sfxManager.PlayStarCollectionSound();
        }
    }

    private void AddCountToLevel()
    {
        int currentUnlockCount = PlayerPrefs.GetInt(ConstantKeys.LEVEL_UNLOCKED, 0);
        currentUnlockCount += 1;
        PlayerPrefs.SetInt(ConstantKeys.LEVEL_UNLOCKED, currentUnlockCount);
    }
}
