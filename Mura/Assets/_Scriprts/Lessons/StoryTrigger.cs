using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    [Header("История, которая запускается при взаимодействии")]
    public Story storyPart;

    [Header("Включить объект или оставить активным после триггера")]
    public GameObject objectToEnable; // необязательно, можно оставить пустым

    private bool triggered = false;

    // Этот метод можно вызывать через OnMouseDown или кнопкой
    public void TriggerStory()
    {
        if (triggered) return; // чтобы не срабатывало несколько раз
        triggered = true;

        if (objectToEnable != null)
            objectToEnable.SetActive(true);

        if (storyPart != null)
            storyPart.TrigerStory();
    }

    // Пример для клика мышкой на объект
    private void OnMouseDown()
    {
        TriggerStory();
    }
}