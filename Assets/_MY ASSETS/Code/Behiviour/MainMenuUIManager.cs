using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    UIManager uiManager;
    [SerializeField] private LevelData levelData;

    private void Start()
    {
        uiManager = UIManager.instance;
        OpenMainmenu();
    }


    public void levelSelectionButton()
    {
        OpenLevelSelection();
    }

    public void backToMainmenuBtn()
    {
        OpenMainmenu();
    }

    private void OpenLevelSelection()
    {
        uiManager.OpenCanvas(CanvasCellName.LEVEL_SELECTION);
    }

    private void OpenMainmenu()
    {
        uiManager.OpenCanvas(CanvasCellName.MAINMENU);
    }
}
