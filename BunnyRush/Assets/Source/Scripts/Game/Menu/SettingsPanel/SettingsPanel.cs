using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private MenuPanel _menuPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private SoundMusicPanel _soundMusicPanel;
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(CloseSettings);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveAllListeners();
    }

    public void OpenSettings()
    {
        gameObject.SetActive(true);
        _soundMusicPanel.Initialize();
    }

    public void CloseSettings()
    {
        gameObject.SetActive(false);
    }
}
