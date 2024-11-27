using System;
using UnityEngine;

public class MenuRoot : CompositeRoot
{   
    [SerializeField] private GameObject _menuUI;

    public event Action OnStartGameButtonClicked;

    private bool _gameStarted;

    public override void Compose()
    {
        
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
