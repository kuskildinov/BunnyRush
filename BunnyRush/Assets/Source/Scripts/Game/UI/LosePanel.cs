using UnityEngine;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private ChanceToRestartPanel _chanceToRestartPanel;
    [SerializeField] private EndGamePanel _endGamePanel;

    public void PlayerLose()
    {
        _chanceToRestartPanel.gameObject.SetActive(true);
        _chanceToRestartPanel.Initialize(this);
        _endGamePanel.Initialize(GameRoot.Instance.CoinsCollector.GetCoinsCount(), (int)GameRoot.Instance.GameScore.CurrentLevelScore);
    }

    public void TimeToChanceIsOver()
    {
        _chanceToRestartPanel.gameObject.SetActive(false);
        _endGamePanel.gameObject.SetActive(true);
    }
}
