using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCoinsVisual : MonoBehaviour
{
    [SerializeField] private Text _currentLevelCoinsCountText;

    public void OnCoinsCountChanged(int count)
    {
        _currentLevelCoinsCountText.text = count.ToString();
    }
}
