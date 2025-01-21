using System.Runtime.InteropServices;
using UnityEngine;

public class AdvRoot : CompositeRoot
{
    public static AdvRoot Instance;


    [DllImport("__Internal")]
    private static extern void ShowAdInternal();
    [DllImport("__Internal")]
    private static extern void ShowDoubleCoinRewardAdInternal();
    [DllImport("__Internal")]
    private static extern void ShowRestartRewardAdInternal();

    private bool _startShowAdv;
    public bool StartShowAd { get => _startShowAdv; private set { } }

    public override void Compose()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }

    public void ShowInterstitialAdv()
    {
#if !UNITY_EDITOR
        ShowAdInternal();
#endif
    }

    public void ShowDoubleCoinRewardAdv()
    {
#if !UNITY_EDITOR
        ShowDoubleCoinRewardAdInternal();
#endif
    }

    public void ShowRestartRewardAdv()
    {
#if !UNITY_EDITOR
        ShowRestartRewardAdInternal();
#endif
    }

    public void OnRestartAdvRewarded()
    {
        GameRoot.Instance.OpenRestartPanel();
    }

    public void OnDoubleCoinAdvRewarded()
    {

    }

    public void OnAdvOpend()
    {
        _startShowAdv = true;

        MyYandex.Instance.StopGameplay();
        SoundsRoot.Instance.TryDisableSound();
    }

    public void OnAdvClosed()
    {
        _startShowAdv = false;

       SoundsRoot.Instance.TryEnableSound();
    }

}
