using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private MenuRoot _menuRoot;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Text _totalScoreText;
    [Header("Skin Menu")]
    [SerializeField] private SelectSkinMenu _selecSkinMenu;
    [SerializeField] private Button _openSkinMenuButton;

    public GameObject MainMenuPanel => _mainMenuPanel;

    public void Initialize()
    {
        _selecSkinMenu.Initialize(_menuRoot);
    }

    private void OnEnable()
    {
        UpdateTotalScoreText();

        _startGameButton.onClick.AddListener(OnStartGameButtonClicked);
        _openSkinMenuButton.onClick.AddListener(OpenSkinMenuButtonClicked);
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveAllListeners();
        _openSkinMenuButton.onClick.RemoveAllListeners();
    }

    private void OnStartGameButtonClicked()
    {
        _menuRoot.StartGame();
    }

    private void OpenSkinMenuButtonClicked()
    {
        _menuRoot.OpenSelectSkinPanel();
    }

    private void UpdateTotalScoreText()
    {
        _totalScoreText.text = SaveRoot.Instance.PlayerSaveData.TotalScore.ToString();
    }
}
