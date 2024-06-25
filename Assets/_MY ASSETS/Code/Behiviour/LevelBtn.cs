using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelBtn : MonoBehaviour
{
    [SerializeField] private GameObject lockImage;
    [SerializeField] private GameObject currentLevelImage;

    public int levelIndex;
    public TMP_Text levelIndexTxt;
    
    public LevelData levelData;
    
    private Button myButton;

    private void Awake()
    {
        myButton = GetComponent<Button>();
        levelIndex = transform.GetSiblingIndex();

        myButton.onClick.AddListener(SetLevelIndex);
        int tempIndex = levelIndex + 1;
        levelIndexTxt.text = tempIndex.ToString();

    }

    private void Start()
    {
        CheckLockStatus();
    }

    private void SetLevelIndex()
    {
        PlayerPrefs.SetInt(ConstantKeys.LEVEL_INDEX, levelIndex);
        levelData.currentLevel = levelIndex;
        SceneManager.LoadScene(1);
    }

    private void CheckLockStatus()
    {
        int currentLockStatus = PlayerPrefs.GetInt(ConstantKeys.LEVEL_UNLOCKED, 0);
        Debug.Log("Unlocked level: " + currentLockStatus);

        if (currentLockStatus >= levelIndex)
        {
            myButton.interactable = true;
            lockImage.SetActive(false);
            
            if (currentLockStatus == levelIndex)
            {
                currentLevelImage.SetActive(true);
                float randomRot = Random.Range(-24f, 97f);
                currentLevelImage.transform.rotation = Quaternion.Euler(0, 0, randomRot);
            }
            else
                currentLevelImage.SetActive(false);
        }
        else
        {
            myButton.interactable = false;
            lockImage.SetActive(true);
        }
    }
}
