using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScoreVisual : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    public void OnScoreChanged(float count)
    {
        _scoreText.text = ConvertInputValue(count);
    }

    public string ConvertInputValue(float value)
    {        
        if (value < 10)
        {
            return $"000000{value.ToString("#.")}";
        }
        else if(value >= 10 && value < 100)
        {
            return $"00000{value.ToString("#.")}";
        }
        else if(value >= 100 && value < 1000)
        {
            return $"0000{value.ToString("#.")}";
        }
        else if (value >= 1000 && value < 10000)
        {
            return $"000{value.ToString("#.")}";
        }
        else if (value >= 10000 && value < 100000)
        {
            return $"00{value.ToString("#.")}";
        }
        else if (value >= 100000 && value < 1000000)
        {
            return $"0{value.ToString("#.")}";
        }
        else if (value >= 1000000 && value < 10000000)
        {
            return $"{value.ToString("#.")}";
        }

        return "0";
    }
}
