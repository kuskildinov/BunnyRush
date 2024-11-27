using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCollector : MonoBehaviour
{  
    [SerializeField] private GameCoinsVisual _gameCoinsVisual;

    private int _currentLevelCoinsCount;

    public void StartGame()
    {
        _currentLevelCoinsCount = 0;
        OnCoinsCountChanged();
    }

    public void OnCoinTaked()
    {
        _currentLevelCoinsCount++;
        OnCoinsCountChanged();
    }

    private void OnCoinsCountChanged()
    {
        _gameCoinsVisual.OnCoinsCountChanged(_currentLevelCoinsCount);
    }


}
