using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChanceToRestartPanel : MonoBehaviour
{
    [SerializeField] private Button _secondChanceButton;
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

    private void OnEnable()
    {
        _secondChanceButton.onClick.AddListener(OnSecondChanseButtonClicked);
    }

    private void OnDisable()
    {
        _secondChanceButton.onClick.RemoveAllListeners();
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

    public void Close()
    {
        StopTimer();
        gameObject.SetActive(false);
    }

    public void StopTimer()
    {
        _timer = 0;
        _startTimer = false;
    }

    private void UpdateTimerImage()
    {
        _timerHeartImage.fillAmount = (_saveTime - _timer) / _saveTime;
    }

    private void OnSecondChanseButtonClicked()
    {
        StopTimer();
#if !UNITY_EDITOR       
        _losePanel.TryContinueGame();
#else
        _losePanel.ContinueGame();
#endif
    }
}
