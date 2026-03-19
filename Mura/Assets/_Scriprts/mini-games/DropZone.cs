using UnityEngine;

public class DropZoneSprite : MonoBehaviour
{
    [Header("Назначь в инспекторе подарок, который сюда подходит")]
    public DraggableSprite requiredItem; // сюда перетащить нужный объект

    public MiniGameManager gameManager;

    //public AudioSource successSound;
    //public AudioSource failSound;

    public bool Accepts(DraggableSprite item)
    {
        bool result = item == requiredItem;
        Debug.Log($"DropZone '{gameObject.name}' проверяет '{item.name}' => {result}");
        return result;
    }
    public void OnCorrectDrop(DraggableSprite item)
    {
        Debug.Log($"Правильный дроп! {item.name} на {gameObject.name}");

        item.gameObject.SetActive(false);

        gameManager.RegisterCorrect();
    }

    public void OnWrongDrop()
    {
        Debug.Log($"Неправильный дроп на {gameObject.name}");
        //failSound?.Play();
    }
}