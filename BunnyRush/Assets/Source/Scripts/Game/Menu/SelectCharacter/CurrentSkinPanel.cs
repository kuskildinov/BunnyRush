using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentSkinPanel : MonoBehaviour
{
    [SerializeField] private List<GameObject> _skinPrefabs;
    [SerializeField] private Text _totalCoinsText;
    [SerializeField] private Text _skinCostText;
    [SerializeField] private SelectSkinMenu _selectSkinMenu;
    [Header("State Buttons")]
    [SerializeField] private Button _selectedButton;
    [SerializeField] private Button _notSelectedButton;
    [SerializeField] private Button _buyButton;
    

    public void Initialize()
    {

    }

    private void OnEnable()
    {
        _selectSkinMenu.OnSkinChanged += ChangeCurrentSkinView;
    }

    private void OnDisable()
    {
        _selectSkinMenu.OnSkinChanged -= ChangeCurrentSkinView;
    }

    private void ChangeCurrentSkinView(SkinButton newSkin)
    {
        if(CheckSkinPurshased(newSkin))
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

    private void HideAllSkinBuyButtons()
    {
        _selectedButton.gameObject.SetActive(false);
        _notSelectedButton.gameObject.SetActive(false);
        _buyButton.gameObject.SetActive(false);
    }
}

public enum SkinState
{
   Selected,
   NoSelected,
   DontHave
}
