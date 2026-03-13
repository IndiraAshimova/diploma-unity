using TMPro;
using UnityEngine;

public class OpenAnswerData : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    public string GetInputText()
    {
        return inputField.text;
    }
}