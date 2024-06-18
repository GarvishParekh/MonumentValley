using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;



public class LevelManager : MonoBehaviour
{
    UIManager uiManager;
    StaffManager staffManager;

    [Header ("<size=15>[SCRIPTS]")]
    [SerializeField] private CameraSettings cameraSettings;
    [SerializeField] private BallFunction ballFunction;

    [Header ("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private LevelData levelData;
    [SerializeField] private CameraData cameraData;

    [Header ("<size=15>[COMPONENTS]")]
    [SerializeField] private GameObject mainCamera;

    [Header ("<size=15>[LEVELS]")]
    [SerializeField] private List<GameObject> Levels = new List<GameObject>();

    [Header ("<size=15>[UI]")]
    [SerializeField] private GameObject commingSoonCanvas;
    [SerializeField] private GameObject mainCanvas;

    LevelInformation currentLevelInfo;


    private void Awake()
    {
        levelData.currentLevel = PlayerPrefs.GetInt(ConstantKeys.LEVEL_INDEX, 0);
    }

    private void Start()
    {
        if (levelData.currentLevel < levelData.levelsInformation.Length)
        {
            currentLevelInfo = levelData.levelsInformation[levelData.currentLevel];
        }
        
        uiManager = UIManager.instance;
        staffManager = StaffManager.Instance;

        SetupLevel();
        CameraOpeningAnimation();
    }

    private void SetupLevel()
    {
        if (levelData.currentLevel >= Levels.Count)
        {
            commingSoonCanvas.SetActive(true);

            mainCanvas.SetActive(false);
            PlayerPrefs.DeleteKey(ConstantKeys.LEVEL_INDEX);

            return;
        }
        foreach (GameObject level in Levels) 
        {
            //set-up camera
            if (currentLevelInfo != null)
            {
                cameraSettings.ChangeZoomLevel(currentLevelInfo.startingZoomLevel);
                ballFunction.ChangeMyPosition(currentLevelInfo.ballSartingposition);
            }

            //enable level
            if (level.transform.GetSiblingIndex() == levelData.currentLevel)
            {
                level.SetActive(true);
            }
            else
            {
                level.SetActive(false);
            }
        }
    }

    public void NextLevelButton()
    {
        levelData.currentLevel += 1;
        PlayerPrefs.SetInt(ConstantKeys.LEVEL_INDEX, levelData.currentLevel);

        uiManager.LevelEndAnimation();
        CameraClosingAnimation();
    }

    public void HardReset()
    {
        SceneManager.LoadScene(0);
    }

    private void CameraOpeningAnimation()
    {
        levelData.cameraAnimation = CameraAnimation.ON_GOING;
        mainCamera.transform.position = new Vector3(0, cameraData.cameraStartingpoint, 0);
        LeanTween.moveY(mainCamera, cameraData.cameraGameplayPoint, cameraData.animationSpeed).setEaseInOutSine().setOnComplete(() =>
        {
            levelData.cameraAnimation = CameraAnimation.COMPLETED;
            if (currentLevelInfo != null)
            {
                cameraSettings.SetLevelCenterPoint(currentLevelInfo.levelCenterPoint);
            }
        });
    }

    private void CameraClosingAnimation()
    {
        LeanTween.moveY(mainCamera, cameraData.cameraEndPoint, cameraData.animationSpeed).setEaseInOutSine().setOnComplete(()=>
        {
            SceneManager.LoadScene(0);
        });
    }

}
