using TMPro;
using UnityEngine;

public class GameplayCanvasAnimation : MonoBehaviour, ICanvasAnimation
{
    [SerializeField] private LevelData levelData;

    [Space]
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject levelName;
    [SerializeField] private TMP_Text levelNameTtxt;
    [SerializeField] private GameObject nextLevelButton;

    public float closingPosition = 50;
    public float animationSpeed = 0.5f;

    public void ClosingAnimation()
    {
        LeanTween.moveLocalY(pauseButton, closingPosition, animationSpeed).setEaseInOutSine();
        LeanTween.moveLocalY(restartButton, closingPosition, animationSpeed).setEaseInOutSine();
        LeanTween.moveLocalY(levelName, closingPosition, animationSpeed).setEaseInOutSine();
        LeanTween.moveLocalY(nextLevelButton, -closingPosition, animationSpeed).setEaseInOutSine();
    }

    public void ResetElements()
    {
        LeanTween.moveLocalY(pauseButton, closingPosition, 0);
        LeanTween.moveLocalY(restartButton, closingPosition, 0);
        LeanTween.moveLocalY(levelName, closingPosition, 0);
        LeanTween.moveLocalY(nextLevelButton, -closingPosition, 0);
    }

    public void StartingAnimation()
    {
        ResetElements();

        if (levelData.levelsInformation.Length > levelData.currentLevel)
        {
            levelNameTtxt.text = levelData.levelsInformation[levelData.currentLevel].LevelName;
            LeanTween.moveLocalY(levelName, 0, animationSpeed).setEaseInOutSine();
        }

        LeanTween.moveLocalY(pauseButton, 0, animationSpeed).setEaseInOutSine();
        LeanTween.moveLocalY(restartButton, 0, animationSpeed).setEaseInOutSine();
    }

    public void UpdateAnimation()
    {
        LeanTween.moveLocalY(nextLevelButton, 0, animationSpeed).setEaseInOutSine();
    }

    public void LevelFailed()
    {
        LeanTween.scale(restartButton, Vector3.one * 0.5f, 0.25f).setEaseInOutSine().setLoopPingPong(3);
    }
}
