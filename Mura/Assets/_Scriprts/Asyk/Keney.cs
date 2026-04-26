using UnityEngine;

public class TargetAsyk : MonoBehaviour
{
    Vector3 startPos;
    public bool isKnocked;

    void Start()
    {
        startPos = transform.position;
    }

    public void CheckKnock(float distance)
    {
        if (isKnocked) return;

        if (Vector3.Distance(startPos, transform.position) > distance)
        {
            isKnocked = true;
        }
    }
}