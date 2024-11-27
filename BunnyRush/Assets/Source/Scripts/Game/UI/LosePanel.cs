using UnityEngine;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private ChanceToRestartPanel _chanceToRestartPanel;
    [SerializeField] private GameObject _endGamePanel;

    public void PlayerLose()
    {
        _chanceToRestartPanel.gameObject.SetActive(true);
        _chanceToRestartPanel.Initialize(this);
    }

    public void TimeToChanceIsOver()
    {
        _chanceToRestartPanel.gameObject.SetActive(false);
        _endGamePanel.gameObject.SetActive(true);
    }
}
