using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string sentence;
    public string speakerName;
    public Sprite characterSprite;
    public CharacterSide side;
}

public enum CharacterSide
{
    Left,
    Right
}