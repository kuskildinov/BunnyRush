using System;
using UnityEngine;

public class MenuRoot : CompositeRoot
{   
    [SerializeField] private MenuPanel _menuPanel;
    [SerializeField] private SelectSkinMenu _skinsMenu;

    public event Action OnStartGameButtonClicked;

    private bool _gameStarted;

    public override void Compose()
    {
        _menuPanel.Initialize();
    }

    public void OpenMainMenu()
    {
        _skinsMenu.gameObject.SetActive(false);
        _menuPanel.MainMenuPanel.gameObject.SetActive(true);
    }

    public void OpenSelectSkinPanel()
    {
        _skinsMenu.gameObject.SetActive(true);
        _menuPanel.MainMenuPanel.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        OnStartGameButtonClicked?.Invoke();

        _menuPanel.gameObject.SetActive(false);
        _skinsMenu.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        _menuPanel.gameObject.SetActive(true);
    }
}
