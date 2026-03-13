using UnityEngine;

public class YurtSlot : MonoBehaviour
{
    public YurtPart partType;
    public SpriteRenderer slotSprite;
    public SpriteRenderer highlight;

    private void Start()
    {
        slotSprite.enabled = false;
        highlight.enabled = false;
    }

    public void ShowHighlight(bool state)
    {
        highlight.enabled = state;
    }

    public void ShowSlot()
    {
        slotSprite.enabled = true;
    }
}