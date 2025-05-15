using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite onTop;
    [SerializeField]
    private bool ToActivate;
    [SerializeField]
    private GameObject imageObj;
    

    private Sprite initialSprite;

    void Start()
    {
        initialSprite = image.sprite;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!ToActivate)
            image.sprite = onTop;
        else
            imageObj.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!ToActivate)
            image.sprite = initialSprite;
        else
            imageObj.SetActive(false);
    }
    public void Down()
    {
        if (!ToActivate)
            image.sprite = initialSprite;
        else
            imageObj.SetActive(false);
    }
}
