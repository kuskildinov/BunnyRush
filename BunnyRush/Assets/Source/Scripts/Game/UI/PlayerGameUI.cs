using UnityEngine;
using UnityEngine.UI;

public class PlayerGameUI : MonoBehaviour
{
    [SerializeField] private MenuRoot _menuRoot;
    [Header("Panels")]
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private SettingsPanel _settingsPanel;
    [SerializeField] private LosePanel _losePanel;
    [Header("Buttons")]
    [SerializeField] private Button _pauseButton;
    [Header("Pause Panel")]
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _menuButton;
   
    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(PauseGame);
        _resumeButton.onClick.AddListener(ResumeGame);
        _settingsButton.onClick.AddListener(ShowSettingsPanel);
        _menuButton.onClick.AddListener(BackToMenu);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveAllListeners();
        _resumeButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
        _menuButton.onClick.RemoveAllListeners();
    }

    public void StartGame()
    {
        ShowGamePanel();
    }

    public void PlayerLose()
    {
        _losePanel.gameObject.SetActive(true);
        _losePanel.PlayerLose();
    }

    private void ResumeGame()
    {
        ShowGamePanel();
        GameRoot.Instance.ResumeGame();
    }

    private void PauseGame()
    {
        ShowPausePanel();
        GameRoot.Instance.PauseGame();
    }

    private void BackToMenu()
    {
        GameRoot.Instance.BackToMenu();
    }

    private void ShowGamePanel()
    {
        ClosePausePanel();
        _gamePanel.gameObject.SetActive(true);
    }

    private void CloseGamePanel()
    {
        _gamePanel.gameObject.SetActive(false);
    }

    private void ShowPausePanel()
    {
        CloseGamePanel();
        _pausePanel.gameObject.SetActive(true);
       
    }

    private void ClosePausePanel()
    {
        _pausePanel.gameObject.SetActive(false);
    }

    public void ShowSettingsPanel()
    {
        _settingsPanel.OpenSettings();
    }
}
