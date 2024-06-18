using System.Runtime.CompilerServices;
using UnityEngine;

public enum CollectiblesStatus
{
    Default,
    Collected
}

[RequireComponent (typeof(BoxCollider))] 
public class Collectibles : MonoBehaviour
{
    StaffManager staffManager;
    public CollectiblesStatus collectibleStatus;

    [SerializeField] private LevelData levelData;

    [Header( "<size=15>[COMPONETNS]" )]
    [SerializeField] GameObject blockAnimation;
    [SerializeField] BoxCollider mainCollider;
    [SerializeField] GameObject mainBall;

    [Header( "<size=15>[VALUES]" )]
    [SerializeField] private float itemRotationSpeed;

    int currentLevelIndex;
    GameObject collectibleItem;
    private void Awake()
    {
        staffManager = StaffManager.Instance;

        mainCollider = GetComponent<BoxCollider>();
        collectibleItem = transform.GetChild(0).gameObject;
        collectibleStatus = CollectiblesStatus.Default;
        currentLevelIndex = levelData.currentLevel;
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
            staffManager.ResetIndex();
            mainCollider.enabled = false;
            collectibleItem.SetActive(false);   
            collectibleStatus = CollectiblesStatus.Collected;
            mainBall.transform.position =levelData.levelsInformation[currentLevelIndex].ballSartingposition;
            
            PlayAnimation();
        }
    }

    private void Update()
    {
        switch (collectibleStatus)
        {
            case CollectiblesStatus.Default:
                CollectibleAnimation();
                break;

            case CollectiblesStatus.Collected:
                break;
        }
    }


    private void PlayAnimation()
    {
        blockAnimation.GetComponent<IBlockAnimation>().PlayAnimation();
    }

    private void ResetAnimation()
    {
        mainCollider.enabled = true;
        collectibleItem.SetActive(true);   
        collectibleStatus = CollectiblesStatus.Default;
        blockAnimation.GetComponent<IBlockAnimation>().RewindAnimation();
    }

    private void CollectibleAnimation()
    {
        transform.Rotate(0, itemRotationSpeed * Time.deltaTime, 0);
    }
}
