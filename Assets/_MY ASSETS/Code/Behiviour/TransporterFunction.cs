using UnityEngine;

public class TransporterFunction : MonoBehaviour
{
    [SerializeField] private Transform endPoint;

    public Transform GetTransform()
    {
        return endPoint;
    }

}
