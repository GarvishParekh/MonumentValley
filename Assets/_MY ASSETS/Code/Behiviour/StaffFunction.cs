using UnityEngine;

public class StaffFunction : MonoBehaviour, IOneClickAnimation
{
    public enum AnimationType
    {
        POOL_STICK,
        SPRING
    }

    [SerializeField] private AnimationType animationType;   
    [SerializeField] private MainClick mainClick;
    [SerializeField] private LevelData levelData;
    [SerializeField] private Transform jumpEndPoint;
    
    public void Animate()
    {
        switch(animationType)
        {
            case AnimationType.POOL_STICK:
                LeanTween.moveLocalZ(gameObject, 1.2f, levelData.animationSpeed).setEaseInSine().setLoopPingPong(1);
                return;
            case AnimationType.SPRING:
                LeanTween.moveLocalY(gameObject, 0.22f, levelData.animationSpeed).setEaseInSine().setLoopPingPong(1);
                return;
        }
    }

    private void OnEnable()
    {
        mainClick.AddToStaffCollection(this.gameObject);
    }

    public Vector3 GetEndPosition()
    {
        return jumpEndPoint.position;
    }
}
