using UnityEngine;
using UnityEngine.UI;

public class YurtUnlock : MonoBehaviour
{
    public Button button;

    public void Lock()
    {
        button.interactable = false;
    }

    public void Unlock()
    {
        button.interactable = true;
    }
}