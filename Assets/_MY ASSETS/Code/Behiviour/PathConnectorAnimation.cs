using UnityEngine;
using System.Collections.Generic;


public class PathConnectorAnimation : MonoBehaviour, IBlockAnimation
{

    [Header ("<size=15>[OBJECT TO DISABLE]>")]
    [SerializeField] private List<GameObject> objectToDisable = new List<GameObject>();    

    [Header ("<size=15>[ANIMATER GROUNDS]>")]
    [SerializeField] private List<GameObject> blocks = new List<GameObject>();    

    [Header ("<size=15>[Staffs]>")]
    [SerializeField] private List<GameObject> staffs = new List<GameObject>();    


    public void PlayAnimation()
    {
        Invoke("InvokeObjectStatus", 0.4f);

        //ObjectStatus(false);

        foreach (var block in blocks) 
        {
            block.GetComponent<IBlockAnimation>().PlayAnimation();
        }

        foreach (var staff in staffs)
        {
            staff.GetComponent<IBlockAnimation>().PlayAnimation();
        }
    }

    public void RewindAnimation()
    {
        ObjectStatus(true);

        for (int i = blocks.Count - 1; i >=0; i--)
        {
            blocks[i].GetComponent<IBlockAnimation>().RewindAnimation();
        }

        foreach (var staff in staffs)
        {
            staff.GetComponent<IBlockAnimation>().RewindAnimation();
        }
    }
    
    private void InvokeObjectStatus()
    {
        ObjectStatus(false);
    }


    private void ObjectStatus(bool check)
    {
        foreach (GameObject obj in objectToDisable)
        {
            if (check)
                LeanTween.scale(obj, Vector3.one, 0.25f).setEaseInOutSine().setEaseInOutBounce();
            else if (!check)
                LeanTween.scale(obj, Vector3.zero, 0.25f).setEaseInOutSine().setEaseInOutBounce();
        }
    }
}
