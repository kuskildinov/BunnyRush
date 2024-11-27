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
    [SerializeField] private PlayerGameUI _playerGameUI;

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
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        MyYandex.Instance.StopGameplay();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        MyYandex.Instance.StartGameplay();
    }

    public void PlayerLose()
    {
        _playerGameUI.PlayerLose();
        _roadRoot.EndGame();
    }

    public void EndGame()
    {
        _playerGameUI.gameObject.SetActive(false);
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
