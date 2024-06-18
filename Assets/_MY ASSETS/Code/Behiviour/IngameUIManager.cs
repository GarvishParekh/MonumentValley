using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameUIManager : MonoBehaviour
{
    UIManager uiManager;

    private void Start()
    {
        uiManager = UIManager.instance;
        OpenGamePlayCanvas();
    }

    public void OpenGamePlayCanvas()
    {
        uiManager.OpenCanvas(CanvasCellName.GAMEPLAY);
    }

    public void BackButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

}
