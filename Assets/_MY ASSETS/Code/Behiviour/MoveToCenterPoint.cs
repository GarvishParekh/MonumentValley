using UnityEngine;

public class MoveToCenterPoint: MonoBehaviour
{
    [SerializeField] private Transform centerPoint;

    public Transform GetCenterPoint()
    {
        return centerPoint;
    }
}
