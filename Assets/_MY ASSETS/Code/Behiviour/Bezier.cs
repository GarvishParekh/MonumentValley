using UnityEngine;
using UnityEngine.Splines;
using System.Collections;

public class Bezier : MonoBehaviour
{
    [Header ("[ COMPONENTS ]")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerCenterPoint;
    [SerializeField] private Transform endVelocity;

    [Space]
    [SerializeField] private SplineAnimate splineAnimate;

    private Vector3 playerVelocity;

    public Transform GetParent()
    {
        return playerCenterPoint;
    }

    public Transform GetEndDirection()
    {
        return endVelocity;
    }

    public SplineAnimate GetSpline()
    {
        return splineAnimate;
    }
}
