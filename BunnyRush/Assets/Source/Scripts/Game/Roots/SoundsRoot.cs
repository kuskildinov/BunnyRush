using UnityEngine;
using UnityEngine.UI;

public class SoundsRoot : CompositeRoot
{
    public static SoundsRoot Instance;

    private bool _soundOn;
    public override void Compose()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _soundOn = SaveRoot.Instance.PlayerSaveData.SoundOn;

        if (_soundOn)
            EnableSound();
        else
            DisableSound();
    }

    private void ChangeSoundState()
    {
        if (_soundOn)
        {
            _soundOn = false;
            SoundButtonOff();
            DisableSound();
        }

        else
        {
            _soundOn = true;
            EnableSound();
        }

        CheckSoundButtonState();
    }

    public void CheckSoundButtonState()
    {
        if (_soundOn)
        {
            SoundButtonOn();
        }
        else
        {
            SoundButtonOff();
        }

        SaveRoot.Instance.PlayerSaveData.SoundOn = _soundOn;
        SaveRoot.Instance.SaveData();
    }

    private void SoundButtonOn()
    {
        _soundOn = true;      
    }

    private void SoundButtonOff()
    {
        _soundOn = false;
    }


    private void OnEnable()
    {
        //_soundButton.onClick.AddListener(ChangeSoundState);
    }

    private void OnDisable()
    {
        //_soundButton.onClick.RemoveAllListeners();
    }

    public void TryEnableSound()
    {
        //if (_soundOn && AdsRoot.Instance.StartShowAd == false)
        //    EnableSound();
    }

    public void EnableSound()
    {
        AudioListener.volume = 1f;
    }

    public void DisableSound()
    {
        AudioListener.volume = 0f;
    }

}
