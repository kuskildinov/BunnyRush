using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMusicPanel : MonoBehaviour
{
    [Header("Buttons")]   
    [SerializeField] private Button _soundButton;
    [Header("Sprites")]
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    public void Initialize()
    {       
        UpdateSoundButton();
    }

    private void OnEnable()
    {
        _soundButton.onClick.AddListener(() =>
        {
            SoundsRoot.Instance.ChangeSoundState();
            UpdateSoundButton();
        });
    }

    private void OnDisable()
    {
        _soundButton.onClick.RemoveAllListeners();
    }

    private void UpdateSoundButton()
    {
        if (SoundsRoot.Instance.SoundOn)
            _soundButton.GetComponent<Image>().sprite = _onSprite;
        else
            _soundButton.GetComponent<Image>().sprite = _offSprite;
    }
}
