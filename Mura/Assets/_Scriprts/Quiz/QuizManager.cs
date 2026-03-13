using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    private Question[] _questions = null;
    public Question[] Questions { get { return _questions; } }

    [SerializeField] GameEvents events = null;
    [SerializeField] private GameObject quizWindow;
    [SerializeField] public FinishUI finishUI = null;

    [Header("Сервисы")]
    [SerializeField] private XPService xpService; // теперь назначаем через инспектор

    private List<AnswerData> PickedAnswers = new List<AnswerData>();
    private List<int> FinishedQuestions = new List<int>();
    private int currentQuestion = 0;
    [SerializeField] private LessonSO currentLesson;

    private bool IsFinished => FinishedQuestions.Count >= Questions.Length;

    void OnEnable()
    {
        events.UpdateQuestionAnswer += UpdateAnswers;
    }
    void OnDisable()
    {
        events.UpdateQuestionAnswer -= UpdateAnswers;
    }

    public void StartQuiz()
    {
        quizWindow.SetActive(true);
        FinishedQuestions.Clear();
        PickedAnswers.Clear();
        ResetScore();
        _questions = currentLesson.LessonQuestions.ToArray();
        var seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        UnityEngine.Random.InitState(seed);

        Display();
    }

    public void UpdateAnswers(AnswerData newAnswer)
    {
        if (Questions[currentQuestion].GetAnswerType == Question.AnswerType.Single)
        {
            foreach (var answer in PickedAnswers)
            {
                if (answer != newAnswer)
                {
                    answer.Reset();
                }
            }
            PickedAnswers.Clear();
            PickedAnswers.Add(newAnswer);
        }
        else
        {
            bool alreadyPicked = PickedAnswers.Exists(x => x == newAnswer);
            if (alreadyPicked)
            {
                PickedAnswers.Remove(newAnswer);
            }
            else
            {
                PickedAnswers.Add(newAnswer);
            }
        }
    }

    public void EraseAnswers()
    {
        PickedAnswers = new List<AnswerData>();
    }

    void Display()
    {
        EraseAnswers();
        var question = GetRandomQuestion();

        if (events.UpdateQuestionUI != null)
            events.UpdateQuestionUI(question);
        else
            Debug.LogWarning("GameEvents.UpdateQuestionUI is null!");
    }

    public void Accept()
    {
        bool isCorrect = CheckAnswers();
        FinishedQuestions.Add(currentQuestion);

        UpdateScore((isCorrect) ? Questions[currentQuestion].AddScore : 0);

        if (!IsFinished)
        {
            Display();
        }
        else
        {
            Debug.Log("Quiz Finished!");
            if (finishUI != null)
            {
                finishUI.Show(events.CurrentFinalScore);
            }
        }
    }

    bool CheckAnswers() => CompairAnswers();

    bool CompairAnswers()
    {
        if (PickedAnswers.Count > 0)
        {
            List<int> correctAns = Questions[currentQuestion].GetCorrectAnswers();
            List<int> pickedAns = PickedAnswers.Select(x => x.AnswerIndex).ToList();

            return !correctAns.Except(pickedAns).Any() && !pickedAns.Except(correctAns).Any();
        }
        return false;
    }

    Question GetRandomQuestion()
    {
        currentQuestion = GetRandomQuestionIndex();
        return Questions[currentQuestion];
    }

    int GetRandomQuestionIndex()
    {
        if (FinishedQuestions.Count >= Questions.Length) return 0;

        int random;
        do
        {
            random = UnityEngine.Random.Range(0, Questions.Length);
        } while (FinishedQuestions.Contains(random) || random == currentQuestion);

        return random;
    }

    private void UpdateScore(int add)
    {
        events.CurrentFinalScore += add;
        events.ScoreUpdated?.Invoke();

        if (xpService != null)
        {
            StartCoroutine(xpService.AddXP(add));
        }
        else
        {
            Debug.LogWarning("XPService не назначен на QuizManager!");
        }
    }

    public void ResetScore()
    {
        events.CurrentFinalScore = 0;
        events.ScoreUpdated?.Invoke();
    }
}