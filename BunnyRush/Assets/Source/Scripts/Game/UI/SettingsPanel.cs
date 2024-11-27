using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
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
        _settingsPanel.gameObject.SetActive(true);
    }

    public void CloseSettings()
    {
        _settingsPanel.gameObject.SetActive(false);
    }
}
