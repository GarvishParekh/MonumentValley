using System;
using UnityEngine;

public enum SnowStatus
{
    NORMAL,
    COLLECTED
}

[CreateAssetMenu(fileName = "Collectibles Data", menuName = "Collectibles Data")]
public class CollectiblesData : ScriptableObject
{
    public SnowStatus snowStatus;
}
