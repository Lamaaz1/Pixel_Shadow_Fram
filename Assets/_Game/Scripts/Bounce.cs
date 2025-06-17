using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Bounce : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
 

    void Start()
    {
        
    }

    public void OnButtonClick()
    {
        Debug.Log("ok");
        transform.DOComplete(); // Completes any tween on this object
                                // transform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 10, 1); // Apply bounce effect
        transform.DOScale(1.2f, 0.1f).OnComplete(() => { transform.DOScale(1, 0.1f); });
        Debug.Log("okk");

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        // Animate the button when pressed down
        transform.DOScale(Vector3.one * 1.05f, 0.15f).OnComplete(() => { transform.DOScale(1, 0.15f); });
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        // Animate the button back to original size when pointer is released
       transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutBack);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Animate the button when highlighted (pointer enters)
        transform.DOScale(Vector3.one * 1.05f, 0.2f).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Animate the button back to its original size when pointer exits
       transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
    }
}
