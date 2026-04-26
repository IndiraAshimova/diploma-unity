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
    public AsykPosition Toss()
    {
        int roll = Random.Range(0, 4);
        AsykPosition result =
            (AsykPosition)roll;

        Debug.Log("Выпал асык: " + result);

        return result;
    }
}