using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Определяет тип менеджера (какой именно он)
public enum DialogueManagerType
{
    Story,
    Dialogue
}

// Определяет способ отображения
public enum DialogueDisplayMode
{
    Story,
    Dialogue
}

public class DialogueManager : MonoBehaviour
{
    // Очередь строк диалога
    private Queue<DialogueLine> lines;

    [Header("UI (Story Mode)")]

    // Имя персонажа (используется в Story)
    public TextMeshProUGUI nameText;

    // Основной текст (Story)
    public TextMeshProUGUI dialogueText;

    // Окно диалога
    public GameObject dialogWindow;

    // Кнопка старта (если используется)
    public GameObject startButton;

    // Событие окончания диалога
    public System.Action onDialogueEnd;

    [Header("Characters")]

    // Персонажи слева и справа
    public GameObject leftCharacterObj;
    public GameObject rightCharacterObj;

    private Image leftCharacterImg;
    private Image rightCharacterImg;

    [Header("Manager Settings")]

    // Тип менеджера (Story или Dialogue)
    public DialogueManagerType managerType;

    // Как отображать диалог
    public DialogueDisplayMode displayMode;

    [Header("Dialogue Bubbles")]

    // Левый пузырь
    public GameObject leftBubble;
    public TextMeshProUGUI leftNameText;
    public TextMeshProUGUI leftMessageText;

    // Правый пузырь
    public GameObject rightBubble;
    public TextMeshProUGUI rightNameText;
    public TextMeshProUGUI rightMessageText;

    [Header("Typing Settings")]

    private Coroutine typingCoroutine;

    // Скорость печати
    public float typingSpeed = 0.03f;

    private bool skipTyping = false;

    void Awake()
    {
        // Создаём очередь
        lines = new Queue<DialogueLine>();

        // Получаем Image внутри персонажей
        if (leftCharacterObj != null)
            leftCharacterImg =
                leftCharacterObj.GetComponentInChildren<Image>();

        if (rightCharacterObj != null)
            rightCharacterImg =
                rightCharacterObj.GetComponentInChildren<Image>();

        // Скрываем персонажей
        if (leftCharacterObj != null)
            leftCharacterObj.SetActive(false);

        if (rightCharacterObj != null)
            rightCharacterObj.SetActive(false);

        // Скрываем окно диалога
        if (dialogWindow != null)
            dialogWindow.SetActive(false);

        // Скрываем пузыри
        if (leftBubble != null)
            leftBubble.SetActive(false);

        if (rightBubble != null)
            rightBubble.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogWindow != null)
            dialogWindow.SetActive(true);

        Debug.Log("Starting dialogue");

        lines.Clear();
        skipTyping = false;

        // Сначала скрываем пузыри
        if (leftBubble != null)
            leftBubble.SetActive(false);

        if (rightBubble != null)
            rightBubble.SetActive(false);

        // Настройка UI по режиму
        if (displayMode == DialogueDisplayMode.Story)
        {
            // Показываем обычный текст
            if (dialogueText != null)
                dialogueText.gameObject.SetActive(true);
        }
        else
        {
            // Скрываем обычный текст
            if (dialogueText != null)
                dialogueText.gameObject.SetActive(false);
        }

        // Скрываем персонажей
        if (leftCharacterObj != null)
            leftCharacterObj.SetActive(false);

        if (rightCharacterObj != null)
            rightCharacterObj.SetActive(false);

        // Добавляем строки в очередь
        foreach (DialogueLine line in dialogue.lines)
            lines.Enqueue(line);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // Если строк нет — конец
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        // Берём следующую строку
        DialogueLine line = lines.Dequeue();

        // Имя только для Story режима
        if (displayMode == DialogueDisplayMode.Story)
        {
            if (nameText != null)
                nameText.text = line.speakerName;
        }

        // Останавливаем старую печать
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        // Запускаем новую
        typingCoroutine =
            StartCoroutine(TypeSentence(line));

        // Показываем персонажа
        UpdateCharacter(line);
    }

    IEnumerator TypeSentence(DialogueLine line)
    {
        TextMeshProUGUI targetText = null;

        if (displayMode == DialogueDisplayMode.Story)
        {
            // Story режим
            dialogueText.text = "";
            targetText = dialogueText;
        }
        else
        {
            // Dialogue режим

            if (line.side == CharacterSide.Left)
            {
                if (leftBubble != null)
                    leftBubble.SetActive(true);

                if (rightBubble != null)
                    rightBubble.SetActive(false);

                if (leftNameText != null)
                    leftNameText.text = line.speakerName;

                targetText = leftMessageText;
            }
            else
            {
                if (rightBubble != null)
                    rightBubble.SetActive(true);

                if (leftBubble != null)
                    leftBubble.SetActive(false);

                if (rightNameText != null)
                    rightNameText.text = line.speakerName;

                targetText = rightMessageText;
            }

            if (targetText != null)
                targetText.text = "";
        }

        // Печать текста
        foreach (char letter in line.sentence.ToCharArray())
        {
            if (skipTyping)
            {
                targetText.text = line.sentence;
                break;
            }

            targetText.text += letter;

            yield return
                new WaitForSeconds(typingSpeed);
        }

        skipTyping = false;
        typingCoroutine = null;
    }

    void UpdateCharacter(DialogueLine line)
    {
        if (line.characterSprite == null)
            return;

        if (line.side == CharacterSide.Left)
        {
            if (leftCharacterObj != null)
                leftCharacterObj.SetActive(true);

            if (leftCharacterImg != null)
                leftCharacterImg.sprite =
                    line.characterSprite;

            SetDim(rightCharacterImg, true);
            SetDim(leftCharacterImg, false);
        }
        else
        {
            if (rightCharacterObj != null)
                rightCharacterObj.SetActive(true);

            if (rightCharacterImg != null)
                rightCharacterImg.sprite =
                    line.characterSprite;

            SetDim(leftCharacterImg, true);
            SetDim(rightCharacterImg, false);
        }
    }

    void SetDim(Image img, bool dim)
    {
        if (img == null)
            return;

        img.color =
            dim ? new Color(1, 1, 1, 0.5f)
                : Color.white;
    }

    public void OnClickDialogue()
    {
        if (typingCoroutine != null)
        {
            skipTyping = true;
        }
        else
        {
            DisplayNextSentence();
        }
    }

    void EndDialogue()
    {
        if (dialogWindow != null)
            dialogWindow.SetActive(false);

        Debug.Log("End Conversation");

        onDialogueEnd?.Invoke();
    }
}