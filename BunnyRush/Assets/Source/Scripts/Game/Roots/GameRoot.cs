using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : CompositeRoot
{
    public static GameRoot Instance;

    [SerializeField] private MenuRoot _menuRoot;
    [SerializeField] private RoadTilesRoot _roadRoot;
    [SerializeField] private CoinsCollector _coinsCollector;
    [SerializeField] private GameScore _gameScore;
    [SerializeField] private PlayerGameUI _playerGameUI;
    public CoinsCollector CoinsCollector => _coinsCollector;
    public GameScore GameScore => _gameScore;

    public override void Compose()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        _menuRoot.OnStartGameButtonClicked += StartGame;
    }

    private void OnDisable()
    {
        _menuRoot.OnStartGameButtonClicked -= StartGame;
    }

    public void StartGame()
    {
        MyYandex.Instance.StartGameplay();
        _playerGameUI.StartGame();
        _coinsCollector.StartGame();
        _gameScore.StartGame();
    }

    public void PauseGame()
    {
        _gameScore.EndGame();
        Time.timeScale = 0f;
        MyYandex.Instance.StopGameplay();
    }

    public void ResumeGame()
    {
        _gameScore.ResumeGame();
        Time.timeScale = 1f;
        MyYandex.Instance.StartGameplay();
    }

    public void PlayerLose()
    {       
        _playerGameUI.PlayerLose();
        _roadRoot.EndGame();
        _gameScore.EndGame();
    }

    public void EndGame()
    {
        _gameScore.EndGame();
        _playerGameUI.gameObject.SetActive(false);
        SaveRoot.Instance.AddCoinsCount(_coinsCollector.GetCoinsCount());
        MyYandex.Instance.StopGameplay();
    }

    public void OnCoinTaked()
    {
        _coinsCollector.OnCoinTaked();
    }

    public void BackToMenu()
    {
        MyYandex.Instance.StopGameplay();
    }
}
