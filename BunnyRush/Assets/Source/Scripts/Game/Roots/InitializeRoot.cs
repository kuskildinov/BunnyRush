using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeRoot : CompositeRoot
{
    private const string GameScene = "GameScene";
    public override void Compose()
    {
#if !UNITY_EDITOR
        StartCoroutine(StartInit());
#else
        SceneManager.LoadScene(GameScene);
#endif

    }

    private IEnumerator StartInit()
    {
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(GameScene);
        yield return null;
    }
}
