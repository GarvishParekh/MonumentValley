using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameplayCanvasAnimation gameplayCanvas;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameplayCanvas.StartingAnimation();
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
