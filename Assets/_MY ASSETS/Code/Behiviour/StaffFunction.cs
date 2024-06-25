using UnityEngine;

public class StaffFunction : MonoBehaviour, IOneClickAnimation
{
    SFXManager sfxManager;
    StaffManager staffManager;

    Collider mycollider;

    [SerializeField] private AnimationType animationType;   
    public DistanceType distanceType;

    [Header ("<size=15>[SCRIPTABLE OBJECT]")]
    [SerializeField] private StaffData staffData;
    [SerializeField] private PlayerData playerData;

    [Header("<size=15>[SCRIPT]")]
    [SerializeField] private MainClick mainClick;
    [SerializeField] private LevelData levelData;

    [Header ("<size=15>[COMPONENTS]")]
    [SerializeField] private Transform siblingParent;
    [SerializeField] private Transform jumpEndPoint;

    [Header("<size=15>[VALUES]")]
    [SerializeField] private float dropSpeed = 0.4f;

    public int staffIndex;

    Transform parentObject;
    float distance;

    private void Awake()
    {
        if (siblingParent == null)
        {
            staffIndex = transform.parent.GetSiblingIndex();
        }
        else 
        {
            staffIndex = siblingParent.parent.GetSiblingIndex();
        }

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
        staffManager = StaffManager.Instance;
    }

    private void Update()
    {
        switch (animationType)
        {
            case AnimationType.POOL_STICK:
                switch (levelData.cameraAnimation)
                {
                    case CameraAnimation.COMPLETED: 
                        //CheckPlayerPosition(staffManager.staffIndex);
                        //playStaffAnimation(0);
                        break;
                }
                break;
        }
    }

    public void Animate()
    {
        if (staffData.animationState == AnimationState.PAUSE)
            return;

        switch(animationType)
        {
            case AnimationType.POOL_STICK:
                if (mycollider != null)
                {
                    //mycollider.enabled = true;
                }

                LeanTween.moveLocalZ(gameObject, 1.2f, levelData.animationSpeed).setEaseInSine().setLoopPingPong(1).setOnComplete(()=> 
                {
                    if (mycollider != null)
                    {
                        //mycollider.enabled = false;
                    }
                });
                return;

            case AnimationType.SPRING:
                LeanTween.moveLocalY(gameObject, 0.22f, levelData.animationSpeed).setEaseInSine().setLoopPingPong(1);
                return;

            case AnimationType.GRAB_NET:
                if (mycollider != null)
                {
                    mycollider.enabled = true;
                }
                LeanTween.scaleY(gameObject, 1.2f, levelData.animationSpeed).setEaseInSine().setLoopPingPong(1).setOnComplete(() =>
                {
                    if (mycollider != null)
                    {
                        mycollider.enabled = false;
                    }
                });
                return;
        }
    }

    private void OnEnable()
    {
        mainClick.AddToStaffCollection(this.gameObject);

        StaffManager.IndexChange += CheckPlayerPosition;
    }

    private void OnDisable()
    {
        StaffManager.IndexChange -= CheckPlayerPosition;
    }

    public Vector3 GetEndPosition()
    {
        return jumpEndPoint.position;
    }

    public void CheckPlayerPosition(int _index)
    {
        Debug.Log("Player position check");
        distance = Vector3.Distance(transform.position, playerData.playerPosition);

        switch (animationType)
        {
            case AnimationType.POOL_STICK:
                    if (_index != staffIndex)
                    {
                        mycollider.enabled = false;
                        LeanTween.scale(parentObject.gameObject, Vector3.zero, staffData.endAnimationSpeed).setEaseInOutSine();
                        return;
                    }

                    mycollider.enabled = true;
                    LeanTween.scale(parentObject.gameObject, Vector3.one, staffData.startAnimationSpeed).setEaseInOutSine();
                    sfxManager.PlayStaffGrowSound();
                break;
        }
    }

    public void staffCloseAnimation()
    {
        LeanTween.scale(parentObject.gameObject, Vector3.zero, staffData.endAnimationSpeed).setEaseInOutSine();
    }

    /*private void playStaffAnimation(int _staffIndex)
    {
        if (_staffIndex == staffIndex)
        {
            if (distance < staffData.distanceThreshold)
            {
                Debug.Log(staffIndex);
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
    }*/

    public float GetDropSpeed()
    {
        return dropSpeed;   
    }
}
