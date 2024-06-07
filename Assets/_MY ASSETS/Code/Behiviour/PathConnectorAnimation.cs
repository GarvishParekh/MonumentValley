using UnityEngine;
using System.Collections.Generic;


public class PathConnectorAnimation : MonoBehaviour, IBlockAnimation
{

    [Header ("<size=15>[OBJECT TO DISABLE]>")]
    [SerializeField] private List<GameObject> objectToDisable = new List<GameObject>();    

    [Header ("<size=15>[ANIMATER GROUNDS]>")]
    [SerializeField] private List<GameObject> blocks = new List<GameObject>();    


    public void PlayAnimation()
    {
        ObjectStatus(false);
        foreach (var block in blocks) 
        {
            block.GetComponent<IBlockAnimation>().PlayAnimation();
        }
    }

    public void RewindAnimation()
    {
        ObjectStatus(true);

        for (int i = blocks.Count - 1; i >=0; i--)
        {
            blocks[i].GetComponent<IBlockAnimation>().RewindAnimation();
        }

        /*foreach (var block in blocks)
        {
            block.GetComponent<IBlockAnimation>().RewindAnimation();
        }*/
    }

    private void ObjectStatus(bool check)
    {
        foreach (GameObject obj in objectToDisable)
        {
            obj.SetActive(check);
        }
    }
}
