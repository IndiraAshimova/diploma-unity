using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapPoint
{
    [Header("Название этапа")]
    public string title;

    [Header("Источник урока")]
    public YurtButtons yurtSource;

    [Header("С какого шага начать")]
    public MonoBehaviour startStep;
}