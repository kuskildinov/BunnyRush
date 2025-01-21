using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyYandex : CompositeRoot
{
    public static MyYandex Instance;
    [DllImport("__Internal")]
    private static extern void CheckIsInited();
    [DllImport("__Internal")]
    private static extern void ReadyGameplayInternal();
    [DllImport("__Internal")]
    private static extern void CheckGameFocusInternal();
    [DllImport("__Internal")]
    private static extern void StartGameplayInternal();
    [DllImport("__Internal")]
    private static extern void StopGameplayInternal();   
    [DllImport("__Internal")]
    private static extern string GetLang();
    [DllImport("__Internal")]
    private static extern void SetToLeaderBoard(int value);

    public string Language; // en ru

    public override void Compose()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

#if !UNITY_EDITOR
        CheckIsInited();     
       
#else
        InitializeGame();
#endif

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
#if !UNITY_EDITOR
       CheckGameFocusInternal();
#endif
    }

    public void GameWindowOnFocus()
    {
        SoundsRoot.Instance.TryEnableSound();
    }

    public void GameWindowOnBlur()
    {
        SoundsRoot.Instance.TryDisableSound();
    }

    public void InitializeGame()
    {
        StartCoroutine(StartInit());
    }
    public void ReadyGameplay()
    {
#if !UNITY_EDITOR
        ReadyGameplayInternal();
#endif
    }

    public void StartGameplay()
    {
#if !UNITY_EDITOR
       StartGameplayInternal();
#endif
    }
    public void StopGameplay()
    {
#if !UNITY_EDITOR
        StopGameplayInternal();
#endif
    }

    private IEnumerator StartInit()
    {       
        yield return new WaitForSecondsRealtime(1f);       
        SaveRoot.Instance.StartLoadData();
        yield return SetLanguageRoutine();
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("GameScene");
    }

    private IEnumerator SetLanguageRoutine()
    {
#if !UNITY_EDITOR
        SetLanguage();
#endif
        yield return null;
    }

    private void SetLanguage()
    {
        Language = GetLang();
        if (Language == "en")
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll("English");
        else if (Language == "ru")
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Russian");
    }

    public void SetLiderboardValue(int value)
    {
#if !UNITY_EDITOR
        SetToLeaderBoard(value);
#endif
    }
}
