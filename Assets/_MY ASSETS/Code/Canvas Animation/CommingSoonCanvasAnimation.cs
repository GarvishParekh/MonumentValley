using UnityEngine;

public class CommingSoonCanvasAnimation : MonoBehaviour
{
    [SerializeField] private GameObject mainText;
    [SerializeField] private GameObject buttonImage;

    private void OnEnable()
    {
        LeanTween.moveLocal(mainText, Vector3.zero, 1.5f).setEaseInOutSine().setOnComplete(()=>
        {
            LeanTween.moveLocal(buttonImage, Vector3.zero, 0.5f).setEaseInOutSine();
        });
    }
}
