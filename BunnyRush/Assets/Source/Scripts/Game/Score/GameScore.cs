using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    [SerializeField] private GameScoreVisual _gameScoreVisual;
    [SerializeField] private float _oneScorePoint;

    private float _currentLevelScore;
    private bool _isActive;

    public float CurrentLevelScore => _currentLevelScore;

    public void StartGame()
    {
        _currentLevelScore = 0f;
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
    }

    private void Update()
    {        
        if(_isActive)
        {           
            _currentLevelScore += _oneScorePoint;
            GameScoreChanged();
        }
    }
}
