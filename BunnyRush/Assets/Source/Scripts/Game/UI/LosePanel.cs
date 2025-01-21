using UnityEngine;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private ChanceToRestartPanel _chanceToRestartPanel;
    [SerializeField] private EndGamePanel _endGamePanel;
    [SerializeField] private RestartPanel _restartPanel;

    private bool _isSecondChance = false;

    public bool IsSeconChance { get => _isSecondChance; set => _isSecondChance = value; }
    public void PlayerLose()
    {
        if(_isSecondChance == false)
        {
            _isSecondChance = true;
            _chanceToRestartPanel.gameObject.SetActive(true);

            _endGamePanel.Close();
            _restartPanel.Close();


        }
        else
        {
            _isSecondChance = false;
            _endGamePanel.gameObject.SetActive(true);

            _chanceToRestartPanel.Close();
            _restartPanel.Close();
        }

        _chanceToRestartPanel.Initialize(this);
        _endGamePanel.Initialize(GameRoot.Instance.CoinsCollector.GetCoinsCount(), (int)GameRoot.Instance.GameScore.CurrentLevelScore);
    }

    public void TimeToChanceIsOver()
    {
        _endGamePanel.gameObject.SetActive(true);

        _chanceToRestartPanel.Close();
        _restartPanel.Close();

    }

    public void CloseLosePanel()
    {
        _endGamePanel.Close();
        _chanceToRestartPanel.Close();
        _restartPanel.Close();
    }

    public void TryContinueGame()
    {      
        GameRoot.Instance.TryRestartGame();
    }

    public void ContinueGame()
    {
        _endGamePanel.Close();
        _chanceToRestartPanel.Close();

        _restartPanel.Open();
    }
}
