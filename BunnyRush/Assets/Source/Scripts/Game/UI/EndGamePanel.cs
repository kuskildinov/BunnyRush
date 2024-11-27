using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{   
    [SerializeField] private Text _totalScoreText;
    [SerializeField] private Text _coinsCountText;
    [SerializeField] private Button _backToMenuButton;

    public void Initialize (int coinsCount, int totalScore)
    {
        _coinsCountText.text = coinsCount.ToString();
        _totalScoreText.text = totalScore.ToString();
    }

    private void ExitGame()
    {
        GameRoot.Instance.EndGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnEnable()
    {
        _backToMenuButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        _backToMenuButton.onClick.RemoveAllListeners();
    }
}
