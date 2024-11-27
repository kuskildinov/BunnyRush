using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private int _index;
    [SerializeField] private int _cost;
    [SerializeField] private SelectSkinMenu _selectMenu;

    public int Index => _index;
    public int Cost => _cost;

    private void OnEnable()
    {
        _button.onClick.AddListener(SetNewSkinPage);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void SetNewSkinPage()
    {
        _selectMenu.OpenNextSkin(this);
    }
}
