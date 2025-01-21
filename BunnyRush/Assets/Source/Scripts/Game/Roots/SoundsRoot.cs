using UnityEngine;
using UnityEngine.UI;

public class SoundsRoot : CompositeRoot
{
    public static SoundsRoot Instance;

    [SerializeField] private AudioSource _playerSource;
    [SerializeField] private AudioSource _coinsSoundsSource;
    [Header("Sounds")]
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _takeHitSound;
    [SerializeField] private AudioClip _coinTakedSound;

    private bool _soundOn;    

    public bool SoundOn => _soundOn;   

    public override void Compose()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _soundOn = SaveRoot.Instance.PlayerSaveData.SoundOn;        

        CheckSounds();
    }

    #region Sounds Configs
    public void ChangeSoundState()
    {
        if (_soundOn)
        {
            _soundOn = false;
            SaveRoot.Instance.PlayerSaveData.SoundOn = false;
            DisableSound();
        }

        else
        {
            _soundOn = true;
            SaveRoot.Instance.PlayerSaveData.SoundOn = true;
            EnableSound();
        }        
    }

    private void CheckSounds()
    {
        if (_soundOn)
            EnableSound();

        else
            TryDisableSound();
    }

    public void TryEnableSound()
    {
        if (_soundOn && AdvRoot.Instance.StartShowAd == false)
            EnableSound();
    }
    public void TryDisableSound()
    {
        DisableSound();
    }

    private void EnableSound()
    {
        AudioListener.volume = 1f;
    }

    private void DisableSound()
    {
        AudioListener.volume = 0f;
    }

    #endregion

    #region All Sounds Play

    public void PlayJumpSound()
    {
        _playerSource.PlayOneShot(_jumpSound);
    }

    public void PlayTakeHitSound()
    {
        _playerSource.PlayOneShot(_takeHitSound);
    }
    public void PlayCoinTakeSound()
    {
        _coinsSoundsSource.PlayOneShot(_coinTakedSound);
    }

    #endregion

}
