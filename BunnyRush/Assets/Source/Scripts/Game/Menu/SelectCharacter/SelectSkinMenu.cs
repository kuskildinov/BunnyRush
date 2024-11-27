using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkinMenu : MonoBehaviour
{
   [SerializeField] private Button _closeButton;
   [SerializeField] private CurrentSkinPanel _currentSkinPanel;
   [SerializeField] private List<SkinButton> _skinButtons;

    private SkinButton _currentSkin;

    public SkinButton CurrentSkin => _currentSkin;

    public event Action<SkinButton> OnSkinChanged; 

    public void Initialize()
    {
        _currentSkinPanel.Initialize();
    }

    public void OpenNextSkin(SkinButton skinButton)
    {
        if (skinButton == null)
            return;

        _currentSkin = skinButton;
        OnSkinChanged?.Invoke(_currentSkin);
    }
   
}
