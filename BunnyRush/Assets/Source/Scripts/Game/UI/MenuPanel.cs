using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private MenuRoot _menuRoot;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Text _totalScoreText;
    [Header("Settings")]
    [SerializeField] private Button _settingsButton;
    [Header("Skin Menu")]
    [SerializeField] private SelectSkinMenu _selecSkinMenu;
    [SerializeField] private Button _openSkinMenuButton;

    public GameObject MainMenuPanel => _mainMenuPanel;

    public void Initialize()
    {
        UpdateTotalScoreText();

        _selecSkinMenu.Initialize(_menuRoot);
    }

    private void OnEnable()
    {
        UpdateTotalScoreText();

        _startGameButton.onClick.AddListener(OnStartGameButtonClicked);
        _openSkinMenuButton.onClick.AddListener(OpenSkinMenuButtonClicked);
        _settingsButton.onClick.AddListener(OpenSettingsButtonClicked);
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveAllListeners();
        _openSkinMenuButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
    }

    private void OnStartGameButtonClicked()
    {
        _menuRoot.StartGame();
    }

    private void OpenSkinMenuButtonClicked()
    {
        _menuRoot.OpenSelectSkinPanel();
    }

    private void OpenSettingsButtonClicked()
    {
        _menuRoot.OpenSettings();
    }   

    private void UpdateTotalScoreText()
    {
        _totalScoreText.text = SaveRoot.Instance.PlayerSaveData.TotalScore.ToString();
    }
}
