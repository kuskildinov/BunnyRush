using System.Collections;
using System.Collections.Generic;
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
        SaveRoot.Instance.StartLoadData();
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
        //SoundsRoot.Instance.TryEnableSound();
    }

    public void GameWindowOnBlur()
    {
        //SoundsRoot.Instance.DisableSound();
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
        StartCoroutine(StartGameplayRoutine());
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
        yield return SetLanguageRoutine();
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("GameScene");
        yield return null;
    }

    private IEnumerator SetLanguageRoutine()
    {

#if !UNITY_EDITOR
        SetLanguage(GetLang());
#else
        SetLanguage("ru");
#endif
        yield return null;
    }

    private void SetLanguage(string lang)
    {
        Language = lang;
        if (lang == "en")
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll("English");
        else if (lang == "ru")
            Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Russian");
    }

    public void SetLiderboardValue(int value)
    {
#if !UNITY_EDITOR
        SetToLeaderBoard(value);
#endif
    }

    private IEnumerator StartGameplayRoutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        StartGameplayInternal();
    }

}
