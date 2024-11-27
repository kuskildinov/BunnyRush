using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkinMenu : MonoBehaviour
{
   [SerializeField] private Button _closeButton;
   [SerializeField] private CurrentSkinPanel _currentSkinPanel;
   [SerializeField] private List<SkinButton> _skinButtons;
    [SerializeField] private Text _currentSkinIndexText;

    private MenuRoot _menuRoot;
    private SkinButton _currentSkin;

    public SkinButton CurrentSkin => _currentSkin;

    public event Action<SkinButton> OnSkinChanged; 

    public void Initialize(MenuRoot menuRoot)
    {
        _menuRoot = menuRoot;
        _currentSkin = _skinButtons[SaveRoot.Instance.PlayerSaveData.CurrentSkinIndex];
        _currentSkinPanel.Initialize();

    }

    public void OpenNextSkin(SkinButton skinButton)
    {
        if (skinButton == null)
            return;

        _currentSkin = skinButton;
        OnSkinChanged?.Invoke(_currentSkin);

        _currentSkinIndexText.text = skinButton.Index.ToString();
    }

   
    private void BackToMainMenu()
    {
        _menuRoot.OpenMainMenu();
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(BackToMainMenu);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
    }

}
