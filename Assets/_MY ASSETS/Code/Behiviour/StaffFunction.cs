using System.Net.Sockets;
using UnityEngine;

public class StaffFunction : MonoBehaviour, IOneClickAnimation
{
    SFXManager sfxManager;

    Collider mycollider;

    [SerializeField] private AnimationType animationType;   
    [SerializeField] private DistanceType distanceType;

    [Header ("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private StaffData staffData;
    [SerializeField] private PlayerData playerData;

    [Header("<size=15>[SCRIPT]")]
    [SerializeField] private MainClick mainClick;
    [SerializeField] private LevelData levelData;

    [Header ("<size=15>[COMPONENTS]")]
    [SerializeField] private Transform jumpEndPoint;

    [Header("<size=15>[VALUES]")]
    [SerializeField] private float dropSpeed = 0.4f;

    Transform parentObject;
    float distance;

    private void Awake()
    {
        parentObject = transform.parent;

        mycollider = GetComponent<Collider>();  
        switch (animationType)
        {
            case AnimationType.POOL_STICK:
                parentObject.transform.localScale = Vector3.zero;
                if (mycollider != null )
                { 
                    mycollider.enabled = false; 
                }
                break;
        }
    }

    private void Start()
    {
        sfxManager = SFXManager.instance;
    }

    private void Update()
    {
        switch (animationType)
        {
            case AnimationType.POOL_STICK:
                switch (levelData.cameraAnimation)
                {
                    case CameraAnimation.COMPLETED: 
                        CheckPlayerPosition();
                        break;
                }
                break;
        }
    }

    public void Animate()
    {
        switch(animationType)
        {
            case AnimationType.POOL_STICK:
                if (mycollider != null)
                {
                    mycollider.enabled = true;
                }

                LeanTween.moveLocalZ(gameObject, 1.2f, levelData.animationSpeed).setEaseInSine().setLoopPingPong(1).setOnComplete(()=> 
                {
                    if (mycollider != null)
                    {
                        mycollider.enabled = false;
                    }
                });
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

    public void CheckPlayerPosition()
    {
        distance = Vector3.Distance(transform.position, playerData.playerPosition);
        if (distance < staffData.distanceThreshold)
        {
            switch (distanceType)
            {
                case DistanceType.FAR:
                    LeanTween.scale(parentObject.gameObject, Vector3.one, staffData.startAnimationSpeed).setEaseInOutSine();
                    sfxManager.PlayStaffGrowSound();
                    break;
            }
            distanceType = DistanceType.CLOSE;
        }
        else
        {
            switch (distanceType)
            {
                case DistanceType.CLOSE:
                    LeanTween.scale(parentObject.gameObject, Vector3.zero, staffData.endAnimationSpeed).setEaseInOutSine();
                    break;
            }
            distanceType = DistanceType.FAR;
        }
    }

    public float GetDropSpeed()
    {
        return dropSpeed;   
    }
}
