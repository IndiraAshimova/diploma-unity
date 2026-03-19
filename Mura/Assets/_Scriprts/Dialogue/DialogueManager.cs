using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<DialogueLine> lines;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogWindow;
    public GameObject startButton;
    public System.Action onDialogueEnd;

    [Header("Персонажи")]
    public GameObject leftCharacterObj;  // пустой объект, внутри Image
    public GameObject rightCharacterObj;
    private Image leftCharacterImg;
    private Image rightCharacterImg;

    private Coroutine typingCoroutine;
    public float typingSpeed = 0.03f;
    private bool skipTyping = false;

    void Awake()
    {
        lines = new Queue<DialogueLine>();

        // Получаем Image внутри пустых объектов
        if (leftCharacterObj != null)
            leftCharacterImg = leftCharacterObj.GetComponentInChildren<Image>();
        if (rightCharacterObj != null)
            rightCharacterImg = rightCharacterObj.GetComponentInChildren<Image>();

        // Скрываем персонажей изначально
        if (leftCharacterObj != null) leftCharacterObj.SetActive(false);
        if (rightCharacterObj != null) rightCharacterObj.SetActive(false);

        // Скрываем диалоговое окно
        if (dialogWindow != null) dialogWindow.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogWindow != null)
            dialogWindow.SetActive(true);

        Debug.Log("Starting dialogue");

        lines.Clear();
        skipTyping = false;

        // Скрываем персонажей перед началом
        if (leftCharacterObj != null) leftCharacterObj.SetActive(false);
        if (rightCharacterObj != null) rightCharacterObj.SetActive(false);

        foreach (DialogueLine line in dialogue.lines)
            lines.Enqueue(line);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = lines.Dequeue();

        nameText.text = line.speakerName;

        // Запускаем печать текста
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeSentence(line));

        // Показываем персонажа
        UpdateCharacter(line);
    }

    IEnumerator TypeSentence(DialogueLine line)
    {
        dialogueText.text = "";

        foreach (char letter in line.sentence.ToCharArray())
        {
            if (skipTyping)
            {
                dialogueText.text = line.sentence; // сразу весь текст
                break;
            }

            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        skipTyping = false;
        typingCoroutine = null; // Сигнал, что печать завершена
    }

    void UpdateCharacter(DialogueLine line)
    {
        if (line.characterSprite == null) return;

        if (line.side == CharacterSide.Left)
        {
            if (leftCharacterObj != null) leftCharacterObj.SetActive(true);
            if (leftCharacterImg != null) leftCharacterImg.sprite = line.characterSprite;

            // Диммим правого
            SetDim(rightCharacterImg, true);
            SetDim(leftCharacterImg, false);
        }
        else
        {
            if (rightCharacterObj != null) rightCharacterObj.SetActive(true);
            if (rightCharacterImg != null) rightCharacterImg.sprite = line.characterSprite;

            // Диммим левого
            SetDim(leftCharacterImg, true);
            SetDim(rightCharacterImg, false);
        }
    }

    void SetDim(Image img, bool dim)
    {
        if (img == null) return;

        img.color = dim ? new Color(1, 1, 1, 0.5f) : Color.white;
    }

    // Эта функция привязывается к кнопке
    public void OnClickDialogue()
    {
        if (typingCoroutine != null)
        {
            // Если текст печатается, скипаем его
            skipTyping = true;
        }
        else
        {
            // Если текст уже показан, идем к следующей строке
            DisplayNextSentence();
        }
    }

    void EndDialogue()
    {
        if (dialogWindow != null) dialogWindow.SetActive(false);
        Debug.Log("End Conversation");

        onDialogueEnd?.Invoke();
    }
}