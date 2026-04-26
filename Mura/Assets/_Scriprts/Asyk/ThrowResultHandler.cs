using UnityEngine;
using System.Collections.Generic;

public class ThrowResultHandler : MonoBehaviour
{
    public TurnManager turnManager;
    public LineManager lineManager;
    public DistanceChecker distanceChecker;

    void Start()
    {
        turnManager.playerThrow.OnThrowFinished += OnThrowFinished;
        turnManager.botThrow.OnThrowFinished += OnThrowFinished;
    }

    void OnThrowFinished()
    {
        Debug.Log("[RESULT] THROW FINISHED");

        List<GameObject> asyks = lineManager.GetAsyks();

        bool hit = false;

        List<GameObject> remove = new List<GameObject>();

        foreach (var obj in asyks)
        {
            if (obj == null) continue;

            TargetAsyk t = obj.GetComponent<TargetAsyk>();
            if (t == null) continue;

            t.CheckKnock(distanceChecker.knockDistance);

            if (t.isKnocked)
            {
                hit = true;
                remove.Add(obj);
                Debug.Log("[RESULT] KNOCKED ? " + obj.name);
            }
        }

        foreach (var obj in remove)
            Destroy(obj);

        lineManager.RemoveNulls();

        Debug.Log("[RESULT] HIT = " + hit);

        turnManager.NextTurn(hit);
    }
}