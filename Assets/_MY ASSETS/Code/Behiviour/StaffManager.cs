using System;
using UnityEngine;

public class StaffManager : MonoBehaviour
{
    public static Action<int> IndexChange;
    public static StaffManager Instance;

    public int staffIndex = 0;
    public int currentIndex = 0;

    private void OnEnable()
    {
        BallFunction.BallReset += ResetIndex;
    }

    private void OnDisable()
    {
        BallFunction.BallReset -= ResetIndex;
    }

    private void Awake()
    {
        Instance = this;    

        ResetIndex();
    }

    private void Start()
    {
        Invoke(nameof(EnableStick), 0.2f);
    }

    private void EnableStick()
    {
        IndexChange?.Invoke(staffIndex);
    }

    private void Update()
    {
        manageActiveStaff();
    }

    private void manageActiveStaff()
    {
        if (currentIndex != staffIndex)
        {
            Debug.Log("Index change");
            staffIndex = currentIndex;
            IndexChange?.Invoke(staffIndex);
        }
    }

    public void ResetIndex()
    {
        currentIndex = 0;
        staffIndex = 0;
        IndexChange?.Invoke(staffIndex);
    }
}
