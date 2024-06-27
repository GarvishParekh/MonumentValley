using UnityEngine;

public class StarCollectionCheck : MonoBehaviour
{
    UIManager uiManager;

    private void Start()
    {
        uiManager = UIManager.instance;
    }

    private void OnEnable()
    {
        BallFunction.LevelCleared += OnLevelCleared;
    }

    private void OnDisable()
    {
        BallFunction.LevelCleared -= OnLevelCleared;
    }

    private void OnLevelCleared()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                uiManager.LevelFailAnimation();
                break;
            }
            else
            {
                uiManager.LevelCompleteUpdate();
            }
        }
    }
}
