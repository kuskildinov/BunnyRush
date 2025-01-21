using System;
using UnityEngine;

public class MenuRoot : CompositeRoot
{   
    [SerializeField] private MenuPanel _menuPanel;
    [SerializeField] private SelectSkinMenu _skinsMenu;
    [SerializeField] private PlayerRoot _playerRoot;
    [SerializeField] private SettingsPanel _settingsPanel;

    public event Action OnStartGameButtonClicked;

    private bool _gameStarted;

    public override void Compose()
    {
        MyYandex.Instance.ReadyGameplay();
        AdvRoot.Instance.ShowInterstitialAdv();
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

    public void OpenSettings()
    {
        _settingsPanel.gameObject.SetActive(true);
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

    public void OnPlayerSkinChanged()
    {
        _playerRoot.SetNewPlayer();
    }
}
