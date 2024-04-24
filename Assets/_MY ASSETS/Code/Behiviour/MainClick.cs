using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum StaffAnimationState
{
    STATIC,
    ANIMATION
}

public class MainClick : MonoBehaviour
{
    [SerializeField] private StaffAnimationState staffAnimationState;
    [SerializeField] private List<GameObject> staffsCollection = new List<GameObject>();
    [SerializeField] private float staffSpeed = 0.15f;
    [SerializeField] private WaitForSeconds animationScale = new WaitForSeconds(0.4f);

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        switch (staffAnimationState)
        {
            case StaffAnimationState.STATIC:
                if (Input.GetKeyDown(KeyCode.Space))
                    OnMainClick();
                break;
        }
    }

    public void _UserClick()
    {
        switch (staffAnimationState)
        {
            case StaffAnimationState.STATIC:
                OnMainClick();
                break;
        }
    }

    private void OnMainClick()
    {
        StartCoroutine(nameof(StaffHit));
    }

    IEnumerator StaffHit()
    {
        staffAnimationState = StaffAnimationState.ANIMATION;   
        foreach (GameObject staff in staffsCollection)
        {
            LeanTween.moveLocalZ(staff, 1.2f, staffSpeed).setEaseInSine().setLoopPingPong(1);
        }

        yield return animationScale;
        staffAnimationState = StaffAnimationState.STATIC;   
    }
}
