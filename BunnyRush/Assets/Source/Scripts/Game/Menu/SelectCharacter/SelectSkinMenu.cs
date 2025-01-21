using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkinMenu : MonoBehaviour
{
   [SerializeField] private Button _closeButton;
   [SerializeField] private CurrentSkinPanel _currentSkinPanel;
   [SerializeField] private List<SkinButton> _skinButtons;
   [SerializeField] private Text _totalCoinsText;

    private MenuRoot _menuRoot;
    private SkinButton _currentSkin;

    public SkinButton CurrentSkin => _currentSkin;

    public event Action<SkinButton> OnSkinChanged; 

    public void Initialize(MenuRoot menuRoot)
    {
        _menuRoot = menuRoot;
        _currentSkin = _skinButtons[SaveRoot.Instance.PlayerSaveData.CurrentSkinIndex];
        _currentSkinPanel.Initialize();
        UpdateCoinsCountText();
        UpdateCurrentSkinPanel();
    }

    public void OpenNextSkin(SkinButton skinButton)
    {
        if (skinButton == null)
            return;

        _currentSkin = skinButton;
        _currentSkinPanel.ChangeCurrentSkinView(skinButton);
    }

    public void SelectNewSkin()
    {
        Debug.Log(_currentSkin);
        SaveRoot.Instance.PlayerSaveData.CurrentSkinIndex = _currentSkin.Index;
        SaveRoot.Instance.SaveData();

        _currentSkinPanel.ChangeCurrentSkinView(_currentSkin);
        _menuRoot.OnPlayerSkinChanged();
    }

    public void TryBuyCurrentSkin()
    {
        var totalCoinsCount = SaveRoot.Instance.PlayerSaveData.TotalCoins;
        var currentSkinCost = _currentSkin.Cost;
        if (currentSkinCost > totalCoinsCount)
            Debug.Log("Монет недостаточно");
        else
        {
            totalCoinsCount -= currentSkinCost;

            SaveRoot.Instance.SetNewTotalCoinsCout(totalCoinsCount);
            SaveRoot.Instance.PlayerSaveData.MySkinsIndexes.Add(_currentSkin.Index);
            SaveRoot.Instance.SaveData();

            _currentSkinPanel.ChangeCurrentSkinView(_currentSkin);
        }
        
    }
   
    private void BackToMainMenu()
    {
        _menuRoot.OpenMainMenu();
    }

    private void UpdateCoinsCountText()
    {
        _totalCoinsText.text = SaveRoot.Instance.PlayerSaveData.TotalCoins.ToString();
    }

    private void UpdateCurrentSkinPanel()
    {
        _currentSkinPanel.ChangeCurrentSkinView(_currentSkin);
    }

    private void OnEnable()
    {        
        _closeButton.onClick.AddListener(BackToMainMenu);
        UpdateCurrentSkinPanel();
        UpdateCoinsCountText();
        SaveRoot.Instance.TotalCoinsCountChanged += UpdateCoinsCountText;

    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
        SaveRoot.Instance.TotalCoinsCountChanged -= UpdateCoinsCountText;
    }
}
