using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public struct UIManagerParameters
{
    [Header("Answers Options")]
    [SerializeField] private float margins;
    public float Margins => margins;
}

[Serializable]
public struct UIElements
{
    [SerializeField] private RectTransform answersContentArea;
    public RectTransform AnswersContentArea => answersContentArea;

    [SerializeField] private TextMeshProUGUI questionInfoTextObject;
    public TextMeshProUGUI QuestionInfoTextObject => questionInfoTextObject;

    [SerializeField] private TextMeshProUGUI scoreText;
    public TextMeshProUGUI ScoreText => scoreText;

    [Header("Open Answer")]
    [SerializeField] private OpenAnswerData openAnswerPrefab;
    public OpenAnswerData OpenAnswerPrefab => openAnswerPrefab;

    [Space]

    [SerializeField] private CanvasGroup mainCanvasGroup;
    public CanvasGroup MainCanvasGroup => mainCanvasGroup;

    [SerializeField] private RectTransform finishUIElements;
    public RectTransform FinishUIElements => finishUIElements;
}

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameEvents events = null;

    [Header("UI Elements (Prefabs)")]
    [SerializeField] private AnswerData answerPrefab = null;

    [SerializeField] private UIElements uIElements;
    [SerializeField] private UIManagerParameters parameters;

    private OpenAnswerData currentOpenAnswer;

    private List<AnswerData> currentAnswers =
        new List<AnswerData>();

    private void OnEnable()
    {
        events.UpdateQuestionUI += UpdateQuestionUI;
        events.ScoreUpdated += UpdateScoreUI;
    }

    private void OnDisable()
    {
        events.UpdateQuestionUI -= UpdateQuestionUI;
        events.ScoreUpdated -= UpdateScoreUI;
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    private void UpdateQuestionUI(Question question)
    {
        uIElements.QuestionInfoTextObject.text =
            question.Info;

        CreateAnswers(question);
    }

    private void CreateAnswers(Question question)
    {
        EraseAnswers();

        RectTransform area =
            uIElements.AnswersContentArea;

        float margin =
            parameters.Margins;

        // ?? Если открытый вопрос
        if (question.GetAnswerType ==
            Question.AnswerType.Open)
        {
            currentOpenAnswer =
                Instantiate(
                    uIElements.OpenAnswerPrefab,
                    area
                );

            RectTransform rect =
                currentOpenAnswer
                .GetComponent<RectTransform>();

            // Растягиваем на всю область (как у тебя сейчас)
            rect.anchorMin =
                new Vector2(0f, 0f);

            rect.anchorMax =
                new Vector2(1f, 1f);

            rect.offsetMin =
                new Vector2(margin, margin);

            rect.offsetMax =
                new Vector2(-margin, -margin);

            return;
        }

        // ?? Размер области
        float areaWidth =
            area.rect.width;

        float areaHeight =
            area.rect.height;

        float sideX =
            areaWidth / 4f;

        int total =
            question.Answers.Length;

        for (int i = 0; i < total; i++)
        {
            AnswerData newAnswer =
                Instantiate(
                    answerPrefab,
                    area
                );

            newAnswer.UpdateData(
                question.Answers[i].Info,
                i
            );

            RectTransform rect =
                newAnswer.Rect;

            int row = i / 2;
            int column = i % 2;

            float rectHeight =
                rect.sizeDelta.y;

            // ?? Начинаем сверху
            float startY =
                areaHeight / 2f
                - rectHeight / 2f
                - margin;

            float yPos =
                startY
                - row *
                (rectHeight + margin);

            float xPos;

            bool isLastOdd =
                (total % 2 != 0)
                && (i == total - 1);

            if (isLastOdd)
            {
                // последний по центру
                xPos = 0f;
            }
            else
            {
                // левая / правая колонка
                xPos =
                    (column == 0)
                    ? -sideX
                    : sideX;
            }

            rect.anchoredPosition =
                new Vector2(xPos, yPos);

            currentAnswers.Add(newAnswer);
        }
    }

    private void CreateOpenAnswer()
    {
        RectTransform area =
            uIElements.AnswersContentArea;

        currentOpenAnswer =
            Instantiate(
                uIElements.OpenAnswerPrefab,
                area
            );

        RectTransform rect =
            currentOpenAnswer
            .GetComponent<RectTransform>();

        // Центрируем InputField
        rect.anchoredPosition =
            Vector2.zero;
    }

    private void EraseAnswers()
    {
        foreach (var answer in currentAnswers)
        {
            Destroy(answer.gameObject);
        }

        currentAnswers.Clear();

        if (currentOpenAnswer != null)
        {
            Destroy(currentOpenAnswer.gameObject);
            currentOpenAnswer = null;
        }
    }

    private void UpdateScoreUI()
    {
        uIElements.ScoreText.text =
            "Score: " +
            events.CurrentFinalScore;
    }

    public string GetOpenAnswerText()
    {
        if (currentOpenAnswer == null)
            return "";

        return currentOpenAnswer
            .GetInputText();
    }
}