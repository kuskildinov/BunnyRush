using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChanceToRestartPanel : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float _saveTime;
    [SerializeField] private Image _timerHeartImage;

    private float _timer;
    private bool _startTimer;

    private LosePanel _losePanel;
    public void Initialize(LosePanel losePanel)
    {
        _losePanel = losePanel;

        _timer = 0;
        _startTimer = true;
    }

    private void Update()
    {
        if (_startTimer == false)
            return;

        _timer += Time.deltaTime;
        UpdateTimerImage();

        if (_timer >= _saveTime)
        {
            _timer = 0;
            _losePanel.TimeToChanceIsOver();
        }
    }

    private void UpdateTimerImage()
    {
        _timerHeartImage.fillAmount = (_saveTime - _timer) / _saveTime;
    }
}
