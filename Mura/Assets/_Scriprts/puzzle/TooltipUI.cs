using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;

    public GameObject tooltipPanel;
    public TMP_Text tooltipText;

    // Отступ от мыши
    public Vector2 offset = new Vector2(50, 25);

    private void Awake()
    {
        Instance = this;
        Hide();
    }

    private void Update()
    {
        if (tooltipPanel.activeSelf)
        {
            tooltipPanel.transform.position = (Vector2)Input.mousePosition + offset;
        }
    }

    public void Show(string text)
    {
        tooltipPanel.SetActive(true);
        tooltipText.text = text;
    }

    public void Hide()
    {
        tooltipPanel.SetActive(false);
    }
}