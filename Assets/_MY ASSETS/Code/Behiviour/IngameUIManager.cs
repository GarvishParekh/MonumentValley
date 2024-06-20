using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameUIManager : MonoBehaviour
{
    UIManager uiManager;
    [SerializeField] private LevelData levelData;

    private void Start()
    {
        uiManager = UIManager.instance;

        if (levelData.currentLevel >= levelData.maxLevelCount)
        {
            Debug.Log("End Canvas Called");
            OpenEndCanvas();
            return;
        }
        OpenGamePlayCanvas();
    }

    public void OpenGamePlayCanvas()
    {
        uiManager.OpenCanvas(CanvasCellName.GAMEPLAY);
    }

    public void OpenEndCanvas()
    {
        uiManager.OpenCanvas(CanvasCellName.END_CANVAS);
    }

    public void BackButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

}
