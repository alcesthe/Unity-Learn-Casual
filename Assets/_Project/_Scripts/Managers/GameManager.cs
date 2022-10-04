using System;
using UnityEngine;
using System.Collections.Generic;
using UnityLearnCasual;
using System.Collections;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

/// <summary>
/// Nice, easy to understand enum-based game manager. For larger and more complex games, look into
/// state machines. But this will serve just fine for most games.
/// </summary>
public class GameManager : StaticInstance<GameManager>
{
    public static event Action<GameState> onBeforeStateChange;
    public static event Action<GameState> onAfterStateChange;
    public GameState state { get; private set; }
    
    [Header("UI")]
    [SerializeField] GameObject answeringPanelUI;
    [SerializeField] GameObject lostPanelUI;
    [SerializeField] GameObject startPanelUI;
    [SerializeField] GameObject pausePanelUI;
    [SerializeField] GameObject inGamePanelUI;

    [Header("Question")]
    // Question
    [SerializeField] List<Question> listOfQuestions = new List<Question>();
    [SerializeField] public float timeForEachQuestion = 15f;
    [SerializeField] public int baseScore = 100;
    private List<Question> listOfEasyQuestions = new List<Question>();
    private List<Question> listOfHardQuestions = new List<Question>();
    [HideInInspector] public Question currentQuestion;
    [HideInInspector] public Question.Difficulty currentDifficulty;
    [HideInInspector] public Question prevQuestion;


    [Header("Core Element")]
    public float score = 0;
    [SerializeField] Player player;

    private void Start()
    { 
        ChangeState(GameState.Idle); // First State
        foreach (Question question in listOfQuestions)
        {
            if (question.difficulty == Question.Difficulty.Easy)
            {
                listOfEasyQuestions.Add(question);
            }
            else
            {
                listOfHardQuestions.Add(question);
            }
        }
    }

    public void ChangeState(GameState newState) {
        onBeforeStateChange?.Invoke(newState);
        state = newState;
        switch (newState) {
            case GameState.Idle:
                HandleIdle();
                break;
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.Lost:
                HandleLost();
                break;
            case GameState.Answering:
                HandleAnswering();
                break;
            case GameState.Pausing:
                HandlePausing();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        onAfterStateChange?.Invoke(newState);
    }
    
    public void ResetAction()
    {
        onAfterStateChange = null;
        onBeforeStateChange = null;
    }

    public void HandlePausing()
    {
        pausePanelUI.SetActive(true);

        player.playerState = PlayerState.Stop;

        onBeforeStateChange = delegate(GameState newState)
        {
            pausePanelUI.SetActive(false);
        };
    }

    private void HandleAnswering()
    {
        player.playerState = PlayerState.Stop;
        SelectQuestion();
        SetQuestionAnswerUI(true);

        onBeforeStateChange = delegate
        {
            answeringPanelUI.SetActive(false);
        };
    }

    private void HandleIdle()
    {
        startPanelUI.SetActive(true);

        onBeforeStateChange = delegate
        {
            startPanelUI.SetActive(false);
        };
    }

    private void HandleStarting()
    {
        SetQuestionAnswerUI(false);
        StartCoroutine("ProgressUpdate");
        player.playerState = PlayerState.Run;

        onBeforeStateChange = delegate
        {
            StopCoroutine("ProgressUpdate");
            player.playerState = PlayerState.Stop;
        };
    }

    private void HandleLost()
    {
        lostPanelUI.SetActive(true);
    }

    private void SelectQuestion()
    {
        do
        {
            switch (currentDifficulty)
            {
                case (Question.Difficulty.Easy):
                    currentQuestion = listOfEasyQuestions[Random.Range(0, listOfEasyQuestions.Count)];
                    break;
                case (Question.Difficulty.Hard):
                    currentQuestion = listOfHardQuestions[Random.Range(0, listOfHardQuestions.Count)];
                    break;
            }
        } while (prevQuestion == currentQuestion);

        prevQuestion = currentQuestion; // Record the old question when done answering
    }

    IEnumerator ProgressUpdate()
    {
        // Speed up the game based time passed;
        while (true)
        {
            score += 1;
            player.speed += 0.2f;
            yield return new WaitForSecondsRealtime(1);
        }
    }

    private void SetQuestionAnswerUI(bool isActive)
    {
        answeringPanelUI.SetActive(isActive);
    }

    public void AddScoreWhenAnswerRight()
    {
        score += baseScore * currentQuestion.scoreMultiplier;
    }
}

[Serializable]
public enum GameState {
    Idle,
    Pausing,
    Answering,
    Starting,
    Lost,
}