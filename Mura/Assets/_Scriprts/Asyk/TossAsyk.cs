using UnityEngine;

public enum AsykPosition
{
    Alshy,
    Taike,
    Buk,
    Shige
}

public class TossAsyk : MonoBehaviour
{
    public AsykPosition tossResult; // результат жребия хранится здесь

    // Симулируем случайное выпадение
    public void Toss()
    {
        int roll = Random.Range(0, 4);
        tossResult = (AsykPosition)roll;

        Debug.Log("Выпал асик: " + tossResult);
    }
}