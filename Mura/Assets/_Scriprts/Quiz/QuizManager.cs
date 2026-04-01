using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private LevelScoreManager levelScore;
    public System.Action onQuizFinished;

    [SerializeField] private GameEvents events = null;
    [SerializeField] private GameObject quizWindow;

    [Header("Сервисы")]
    [SerializeField] private XPService xpService;

    [Header("Настройки")]
    [SerializeField] private bool useRandomOrder = true;

    [SerializeField] private LessonSO currentLesson;

    private Question[] _questions = null;
    private List<AnswerData> PickedAnswers = new List<AnswerData>();
    private List<int> FinishedQuestions = new List<int>();

    private int currentQuestion = -1;
    private List<int> questionOrder = new List<int>();

    private bool IsFinished => FinishedQuestions.Count >= Questions.Length;
    public Question[] Questions => _questions;

    private void OnEnable()
    {
        events.UpdateQuestionAnswer += UpdateAnswers;
    }

    private void OnDisable()
    {
        events.UpdateQuestionAnswer -= UpdateAnswers;
    }
    public void StartQuiz()
    {
        events.CurrentFinalScore = 0;

        FinishedQuestions.Clear();
        PickedAnswers.Clear();
        currentQuestion = -1;
        questionOrder.Clear();

        _questions = currentLesson.LessonQuestions.ToArray();

        // Заполняем порядок вопросов
        for (int i = 0; i < _questions.Length; i++)
            questionOrder.Add(i);

        int maxScore = _questions.Sum(q => q.AddScore);
        levelScore?.AddMaxScore(maxScore);

        ShowQuiz();
        DisplayNextQuestion();
    }

    public void UpdateAnswers(AnswerData newAnswer)
    {
        var currentQ = Questions[currentQuestion];

        if (currentQ.GetAnswerType == Question.AnswerType.Single)
        {
            foreach (var answer in PickedAnswers)
            {
                if (answer != newAnswer)
                    answer.Reset();
            }

            PickedAnswers.Clear();
            PickedAnswers.Add(newAnswer);
        }
        else
        {
            bool alreadyPicked = PickedAnswers.Exists(x => x == newAnswer);

            if (alreadyPicked)
                PickedAnswers.Remove(newAnswer);
            else
                PickedAnswers.Add(newAnswer);
        }
    }

    private void DisplayNextQuestion()
    {
        PickedAnswers.Clear();

        if (IsFinished)
        {
            Debug.Log("[Quiz] Quiz Finished!");

            onQuizFinished?.Invoke();

            return;
        }

        var question = GetNextQuestion();

        events.UpdateQuestionUI?.Invoke(question);
    }

    private Question GetNextQuestion()
    {
        if (useRandomOrder)
        {
            int randomIndex =
                UnityEngine.Random.Range(0, questionOrder.Count);

            currentQuestion = questionOrder[randomIndex];
            questionOrder.RemoveAt(randomIndex);
        }
        else
        {
            currentQuestion++;
        }

        return _questions[currentQuestion];
    }

    public void Accept()
    {
        bool isCorrect = CheckAnswers();

        FinishedQuestions.Add(currentQuestion);

        int addScore =
            isCorrect ? Questions[currentQuestion].AddScore : 0;

        UpdateScore(addScore);

        DisplayNextQuestion();
    }

    private bool CheckAnswers()
    {
        if (PickedAnswers.Count == 0)
            return false;

        List<int> correctAns =
            Questions[currentQuestion].GetCorrectAnswers();

        List<int> pickedAns =
            PickedAnswers.Select(x => x.AnswerIndex).ToList();

        return !correctAns.Except(pickedAns).Any()
            && !pickedAns.Except(correctAns).Any();
    }

    private void UpdateScore(int add)
    {
        if (levelScore != null)
            levelScore.AddScore(add);

        events.CurrentFinalScore += add;
        events.ScoreUpdated?.Invoke();;
    }

    public void CloseQuizWindow()
    {
        quizWindow.SetActive(false);
    }

    public void ShowQuiz()
    {
        quizWindow.SetActive(true);
    }

    public void HideQuiz()
    {
        quizWindow.SetActive(false);
    }
}