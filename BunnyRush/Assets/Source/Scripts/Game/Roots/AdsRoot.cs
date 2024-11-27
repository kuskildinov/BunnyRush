using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class AdsRoot : MonoBehaviour
{
    public static AdsRoot Instance;

    [DllImport("__Internal")]
    private static extern void ShowAdInternal();
    [DllImport("__Internal")]
    private static extern void ShowRewardVideoAdInternal();

    [SerializeField] private Player _player;
    [SerializeField] private SoundsRoot _soundRoot;
    [SerializeField] private Button _videoAdButton;

    private bool _startShowAd = false;
    public bool StartShowAd => _startShowAd;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        _videoAdButton.onClick.AddListener(OnVideoAdButtonClick);
    }

    private void OnDisable()
    {
        _videoAdButton.onClick.RemoveListener(OnVideoAdButtonClick);
    }

    #region RewardVideoAds

    private void OnVideoAdButtonClick()
    {
        ShowRewardVideoAdInternal();
    }
    public void OnAdOpend()
    {
        _startShowAd = true;
        StopGame();
    }

    public void OnAdRewarded()
    {
        _startShowAd = false;
        ResumeGame();
      
    }
    public void OnAdClosed()
    {
        _startShowAd = false;
        ResumeGame();
    }
    public void OnAdErrorOccured() { }

    #endregion
    #region InterstitialAds

    public void ShowInterstitialAd() => ShowAdInternal();
    public void OnInterstitialAdOpend()
    {
        _startShowAd = true;
        StopGame();
    }

    public void OnInterstitialAdClosed()
    {
        _startShowAd = false;
        ResumeGame();
    }

    #endregion 

    public void ResumeGame()
    {
        _soundRoot.TryEnableSound();
        MyYandex.Instance.StartGameplay();
        Time.timeScale = 1f;
    }

    public void StopGame()
    {
        _soundRoot.DisableSound();
        MyYandex.Instance.StopGameplay();
        Time.timeScale = 0f;
    }

}
