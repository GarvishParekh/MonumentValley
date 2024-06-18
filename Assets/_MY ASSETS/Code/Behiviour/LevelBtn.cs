using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelBtn : MonoBehaviour
{
    public int levelIndex;
    
    public LevelData levelData;
    
    private Button myButton;

    private void Awake()
    {
        myButton = GetComponent<Button>();
        levelIndex = transform.GetSiblingIndex();

        myButton.onClick.AddListener(SetLevelIndex);
    }

    private void SetLevelIndex()
    {
        PlayerPrefs.SetInt(ConstantKeys.LEVEL_INDEX, levelIndex);
        levelData.currentLevel = levelIndex;
        SceneManager.LoadScene(1);
    }
}
