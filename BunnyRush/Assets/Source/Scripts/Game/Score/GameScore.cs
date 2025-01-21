using System;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    [SerializeField] private GameScoreVisual _gameScoreVisual;
    [SerializeField] private BestPlayerScoreView _bestPlayerScoreView;

    private float _currentLevelScore;
    private bool _isActive;

    public float CurrentLevelScore => _currentLevelScore;

    public event Action OnScoreChanged;

    public void StartGame()
    {
        _currentLevelScore = 0;
        GameScoreChanged();
        _isActive = true;             
    }

    public void ResumeGame()
    {
        _isActive = true;
    }

    public void EndGame()
    {
        _isActive = false;
        SaveRoot.Instance.TrySetNewScore((int)_currentLevelScore);
    }

    public void GameScoreChanged()
    {
        _gameScoreVisual.OnScoreChanged(_currentLevelScore);
        OnScoreChanged?.Invoke();
    }

    private void Update()
    {        
        if(_isActive)
        {           
            _currentLevelScore += Time.deltaTime * 5;
            GameScoreChanged();
        }
    }
}
