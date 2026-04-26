using UnityEngine;
using System.Collections.Generic;

public class LineManager : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject asykPrefab;

    [Header("Settings")]
    public int count = 13;
    public float spacing = 0.15f;

    public Transform startPoint;

    private List<GameObject> asyks =
        new List<GameObject>();

    void Start()
    {
        SpawnLine();
    }

    void SpawnLine()
    {
        Vector3 pos =
            startPoint.position;

        for (int i = 0; i < count; i++)
        {
            GameObject obj =
                Instantiate(
                    asykPrefab,
                    pos,
                    Quaternion.identity
                );

            asyks.Add(obj);

            pos.x += spacing;
        }
    }

    public List<GameObject> GetAsyks()
    {
        return asyks;
    }

    public void RemoveNulls()
    {
        asyks.RemoveAll(x => x == null);
    }
}