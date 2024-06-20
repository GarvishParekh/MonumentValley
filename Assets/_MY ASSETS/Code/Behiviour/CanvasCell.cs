using UnityEngine;

public class CanvasCell : MonoBehaviour
{
    CanvasGroup canvasGroup;
    [SerializeField] private CanvasCellName myCanvasCellName;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void OpenCanvas()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void CloseCanvas()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public CanvasCellName GetCanvasName()
    {
        return myCanvasCellName;
    }
}
