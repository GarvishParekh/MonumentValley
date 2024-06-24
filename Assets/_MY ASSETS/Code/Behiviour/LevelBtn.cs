using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelBtn : MonoBehaviour
{
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

    private void SetLevelIndex()
    {
        PlayerPrefs.SetInt(ConstantKeys.LEVEL_INDEX, levelIndex);
        levelData.currentLevel = levelIndex;
        SceneManager.LoadScene(1);
    }
}
