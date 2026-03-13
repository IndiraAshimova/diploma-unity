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

    [SerializeField] private OpenAnswerData openAnswerPrefab;
    public OpenAnswerData OpenAnswerPrefab => openAnswerPrefab;

    private OpenAnswerData currentOpenAnswer;

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

    private List<AnswerData> currentAnswers = new List<AnswerData>();

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
        uIElements.QuestionInfoTextObject.text = question.Info;
        CreateAnswers(question);
    }

    private void CreateAnswers(Question question)
    {
        EraseAnswers();

        float offset = -parameters.Margins;

        if (question.GetAnswerType == Question.AnswerType.Open)
        {
            currentOpenAnswer = Instantiate(
                uIElements.OpenAnswerPrefab,
                uIElements.AnswersContentArea
            );

            return;
        }


        for (int i = 0; i < question.Answers.Length; i++)
        {
            AnswerData newAnswer = Instantiate(answerPrefab, uIElements.AnswersContentArea);
            newAnswer.UpdateData(question.Answers[i].Info, i);

            newAnswer.Rect.anchoredPosition = new Vector2(0, offset);

            offset -= (newAnswer.Rect.sizeDelta.y + parameters.Margins);

            uIElements.AnswersContentArea.sizeDelta =
                new Vector2(
                    uIElements.AnswersContentArea.sizeDelta.x,
                    -offset
                );

            currentAnswers.Add(newAnswer);
        }
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
        uIElements.ScoreText.text = "Score: " + events.CurrentFinalScore;
    }
}