using System;
using UnityEngine;
using System.Collections.Generic;
using UnityLearnCasual;
using Random = UnityEngine.Random;

/// <summary>
/// Nice, easy to understand enum-based game manager. For larger and more complex games, look into
/// state machines. But this will serve just fine for most games.
/// </summary>
public class GameManager : StaticInstance<GameManager> {
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;
    [SerializeField] List<Question> listOfQuestion = new List<Question>();
    [SerializeField] GameObject answeringPanelUI;

    [HideInInspector] public Question _currentQuestion;
    [HideInInspector] public Question _prevQuestion;


    public GameState State { get; private set; }

    // Kick the game off with the first state
    void Start() => ChangeState(GameState.Answering); // TODO return back Idle

    public void ChangeState(GameState newState) {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
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

        OnAfterStateChanged?.Invoke(newState);
    }

    private void HandlePausing()
    {
        
    }

    private void HandleAnswering()
    {
        //speed = 0;
        _currentQuestion = listOfQuestion[Random.Range(0, listOfQuestion.Count)];
        answeringPanelUI.SetActive(true);
        /*do { _currentQuestion = listOfQuestion[Random.Range(0, listOfQuestion.Count)]; }
        while (_prevQuestion == _currentQuestion);*/
    }

    private void HandleIdle()
    {

    }

    private void HandleStarting() {
        _prevQuestion = _currentQuestion;
    }

    private void HandleLost()
    {

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