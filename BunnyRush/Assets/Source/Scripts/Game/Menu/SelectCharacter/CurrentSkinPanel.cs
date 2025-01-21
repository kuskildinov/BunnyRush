using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSkinPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> _skinPrefabs;
    [SerializeField] private Text _skinCostText;
    [SerializeField] private SelectSkinMenu _selectSkinMenu;
   
    [Header("State Buttons")]
    [SerializeField] private Button _selectedButton;
    [SerializeField] private Button _notSelectedButton;
    [SerializeField] private Button _buyButton;

    private GameObject _currentSkinView;
    public void Initialize()
    {        
        CheckSkinCost();
        _currentSkinView = _skinPrefabs[0];
    }

    private void OnEnable()
    {
        _notSelectedButton.onClick.AddListener(OnNotSelectButtonClicked);
        _buyButton.onClick.AddListener(OnBuySkinButtonClicked);
    }

    private void OnDisable()
    {
        _notSelectedButton.onClick.RemoveAllListeners();
        _buyButton.onClick.RemoveAllListeners();
    }

    public void ChangeCurrentSkinView(SkinButton newSkin)
    {
        UpdateCurrentSkinView(newSkin);

        if (CheckSkinPurshased(newSkin))
        {
            if(CheckIsCurrent(newSkin))
            {
                ShowSkinBuyButton(SkinState.Selected);
            }
            else
            {
                ShowSkinBuyButton(SkinState.NoSelected);
            }
        }
        else
        {
            ShowSkinBuyButton(SkinState.DontHave);
        }
    }

    private bool CheckSkinPurshased(SkinButton newSkin)
    {
        if (SaveRoot.Instance.PlayerSaveData.MySkinsIndexes.Contains(newSkin.Index))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckIsCurrent(SkinButton newSkin)
    {
        if (SaveRoot.Instance.PlayerSaveData.CurrentSkinIndex == newSkin.Index)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ShowSkinBuyButton(SkinState state)
    {
        HideAllSkinBuyButtons();

        switch (state)
        {
            case SkinState.Selected:
                {
                    _selectedButton.gameObject.SetActive(true);
                    break;
                }
            case SkinState.NoSelected:
                {
                    _notSelectedButton.gameObject.SetActive(true);
                    break;
                }
            case SkinState.DontHave:
                {
                    _buyButton.gameObject.SetActive(true);
                    _skinCostText.text = _selectSkinMenu.CurrentSkin.Cost.ToString();
                    break;
                }
        }
    }

    private void CheckSkinCost()
    {
        var coinsCount = SaveRoot.Instance.PlayerSaveData.TotalCoins;

        if (_selectSkinMenu.CurrentSkin.Cost >= coinsCount)
        {
            _buyButton.interactable = false;
        }
        else
        {
            _buyButton.interactable = true;
        }
    }

    private void HideAllSkinBuyButtons()
    {
        _selectedButton.gameObject.SetActive(false);
        _notSelectedButton.gameObject.SetActive(false);
        _buyButton.gameObject.SetActive(false);
    }

    private void OnNotSelectButtonClicked()
    {
        _selectSkinMenu.SelectNewSkin();
    }

    private void OnBuySkinButtonClicked()
    {
        _selectSkinMenu.TryBuyCurrentSkin();
    }

    private void UpdateCurrentSkinView(SkinButton newSkin)
    {
        if (_currentSkinView != null)
            _currentSkinView.gameObject.SetActive(false);

        for (int i = 0; i < _skinPrefabs.Count; i++)
        {
            if(i == newSkin.Index)
            {
                _skinPrefabs[i].gameObject.SetActive(true);
                _currentSkinView = _skinPrefabs[i];
            }
        }
    }
}

public enum SkinState
{
   Selected,
   NoSelected,
   DontHave
}
