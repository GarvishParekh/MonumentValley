using UnityEngine;

public class PortalFunction : MonoBehaviour
{
    public Transform portalEndPoint;
    [SerializeField] private Transform endPortalTransform;


    public Transform GetPortalEndPosition()
    {
        return portalEndPoint;
    }

    public Transform GetEndPortalTransform()
    {
        return endPortalTransform;
    }
}
