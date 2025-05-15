using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite onTop;
    

    private Sprite initialSprite;

    void Start()
    {
        initialSprite = image.sprite;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = onTop;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = initialSprite;
    }
    void OnPointerDown(PointerEventData eventData)
    {
        image.sprite = initialSprite;
    }
}
