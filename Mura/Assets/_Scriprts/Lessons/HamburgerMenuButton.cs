using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class HamburgerMenuButton : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Button button;

    public void Setup(string text, Action onClick)
    {
        if (buttonText != null) buttonText.text = text;
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClick?.Invoke());
        }
    }
}