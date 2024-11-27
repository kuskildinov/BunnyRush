using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private MenuRoot _menuRoot;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Text _totalScoreText;

    private void OnEnable()
    {
        UpdateTotalScoreText();

        _startGameButton.onClick.AddListener(OnStartGameButtonClicked);
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveAllListeners();
    }

    private void OnStartGameButtonClicked()
    {
        _menuRoot.StartGame();
    }

    private void UpdateTotalScoreText()
    {
        _totalScoreText.text = SaveRoot.Instance.PlayerSaveData.TotalScore.ToString();
    }
}
