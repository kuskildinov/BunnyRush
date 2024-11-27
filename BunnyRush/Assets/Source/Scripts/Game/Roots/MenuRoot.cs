using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuRoot : CompositeRoot
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private GameObject _menuUI;

    public event Action OnStartGameButtonClicked;

    private bool _gameStarted;

    public override void Compose()
    {
        
    }

    private void Update()
    {
        if(_gameStarted == false && Input.GetKeyDown(KeyCode.Space))
        {
            _gameStarted = true;
            StartGame();
        }
    }

    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveAllListeners();
    }

    public void StartGame()
    {
        OnStartGameButtonClicked?.Invoke();

        _menuUI.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        _menuUI.gameObject.SetActive(true);
    }
}
