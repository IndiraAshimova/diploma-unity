using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string itemName;
    public TMP_Text hoverText;

    public Vector2 offset = new Vector2(50, 25);

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverText.text = itemName;
        hoverText.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (hoverText.gameObject.activeSelf)
        {
            hoverText.transform.position = Input.mousePosition + (Vector3)offset;
        }
    }
}