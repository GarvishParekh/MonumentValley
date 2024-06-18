using UnityEngine;

public class PortalFunction : MonoBehaviour
{
    public Transform portalEndPoint;
    [SerializeField] private Transform endPortalTransform;
    [SerializeField] private Transform startPortalTransform;


    public Transform GetPortalEndPosition()
    {
        return portalEndPoint;
    }

    public Transform GetEndPortalTransform()
    {
        return endPortalTransform;
    }

    public Transform GetStartPortalTransform()
    {
        return startPortalTransform;
    }
}
