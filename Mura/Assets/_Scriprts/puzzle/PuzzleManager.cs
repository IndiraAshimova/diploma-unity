using UnityEngine;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public TMP_Text taskText;

    public YurtSlot doorSlot;
    public YurtSlot keregeSlot;
    public YurtSlot shanyrakSlot;
    public YurtSlot uykSlot;
    public YurtSlot feltSlot;

    private int step = 0;

    private YurtPart[] order =
    {
        YurtPart.Door,
        YurtPart.Kerege,
        YurtPart.Shanyrak,
        YurtPart.Uyk,
        YurtPart.Felt
    };

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateTask();
    }

    void UpdateTask()
    {
        if (step >= order.Length)
        {
            taskText.text = "Yurt Completed!";
            return;
        }

        YurtSlot slot = GetSlot(order[step]);
        slot.ShowHighlight(true);

        taskText.text = "Place the " + order[step];
    }

    public bool IsCorrectPart(YurtPart part)
    {
        return part == order[step];
    }

    public YurtSlot GetSlot(YurtPart part)
    {
        switch (part)
        {
            case YurtPart.Door: return doorSlot;
            case YurtPart.Kerege: return keregeSlot;
            case YurtPart.Shanyrak: return shanyrakSlot;
            case YurtPart.Uyk: return uykSlot;
            case YurtPart.Felt: return feltSlot;
        }

        return null;
    }

    public void PlacePiece(YurtPiece piece)
    {
        YurtSlot slot = GetSlot(piece.partType);

        slot.ShowHighlight(false);
        slot.ShowSlot();   // показываем Slot

        piece.gameObject.SetActive(false); // скрываем деталь

        step++;

        UpdateTask();
    }
}