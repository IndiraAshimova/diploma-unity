using UnityEngine;

public class AsykLineSpawner : MonoBehaviour
{
    [Header("Asyk Settings")]
    public GameObject targetAsykPrefab;

    public int asykCount = 7;

    public float spacing = 0.6f;

    void Start()
    {
        SpawnLine();
    }

    void SpawnLine()
    {
        float totalWidth =
            (asykCount - 1) * spacing;

        float startX =
            transform.position.x - totalWidth / 2f;

        float y =
            transform.position.y;

        for (int i = 0; i < asykCount; i++)
        {
            Vector2 spawnPosition =
                new Vector2(
                    startX + i * spacing,
                    y
                );

            GameObject newAsyk =
                Instantiate(
                    targetAsykPrefab,
                    spawnPosition,
                    Quaternion.identity
                );

            TargetAsyk target =
                newAsyk.GetComponent<TargetAsyk>();

            if (target != null)
            {
                target.requiredDistance = 2.0f;
            }
        }
    }
}