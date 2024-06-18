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
    [SerializeField] private WaitForSeconds animationScale = new WaitForSeconds(0.4f);
    public HashSet<GameObject> staffsCollection = new HashSet<GameObject>();

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
            staff.GetComponent<IOneClickAnimation>().Animate();
        }

        yield return animationScale;
        staffAnimationState = StaffAnimationState.STATIC;   
    }

    public void AddToStaffCollection(GameObject _staff)
    {
        staffsCollection.Add(_staff);   
    }

}
