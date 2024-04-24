using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    public int currentLevel = 0;
    public LevelInformation[] levelsInformation;    
}

[System.Serializable]
public class LevelInformation
{
    public string LevelName;
}
