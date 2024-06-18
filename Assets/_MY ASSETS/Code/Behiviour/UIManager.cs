using UnityEngine;
using System.Collections.Generic;

public enum CanvasCellName
{
    MAINMENU,
    GAMEPLAY,
    LEVEL_SELECTION,
    BACKGROUND_CANVAS,
    END_CANVAS
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameplayCanvasAnimation gameplayCanvas;
    [SerializeField] private List<CanvasCell> canvasCells = new List<CanvasCell>();   

    private void Awake()
    {
        instance = this;
    }

    public void OpenCanvas(CanvasCellName canvasToOpen)
    {
        foreach (CanvasCell cell in canvasCells)
        {
            if (canvasToOpen == cell.GetCanvasName())
            {
                cell.OpenCanvas();
            }
            else
            {
                cell.CloseCanvas();
            }
        }
    }

    private void Start()
    {
        if (gameplayCanvas != null)
        {
            gameplayCanvas.StartingAnimation();
        }
    }

    public void LevelCompleteUpdate()
    {
        gameplayCanvas.UpdateAnimation();
    }

    public void LevelEndAnimation()
    {
        gameplayCanvas.ClosingAnimation();
    }
}
