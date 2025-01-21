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

    public int GetCoinsCount()
    {
        return _currentLevelCoinsCount;
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
