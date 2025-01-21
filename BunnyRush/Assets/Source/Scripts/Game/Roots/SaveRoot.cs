using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class SaveRoot : CompositeRoot
{
    public static SaveRoot Instance;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);
    [DllImport("__Internal")]
    private static extern void LoadExtern();

    private PlayerData _playerSaveData;
    public PlayerData PlayerSaveData { get => _playerSaveData; set => _playerSaveData = value; }

    public event Action TotalScoreChanged;
    public event Action TotalCoinsCountChanged;
    public event Action CurrentSkinChanged;
    public event Action MySkinsCountChanged;

    public override void Compose()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance.gameObject);

        _playerSaveData = new PlayerData();

        DontDestroyOnLoad(this);
    }

    public void StartLoadData()
    {
#if !UNITY_EDITOR
        LoadExtern();
#endif
    }

    public void TrySetNewScore(int score)
    {
        if (score < _playerSaveData.TotalScore || _playerSaveData.TotalScore >= 9999999)
            return;

        SetNewTotalScore(score);
    }

    public void SetNewTotalScore(int score)
    {
        if (score > _playerSaveData.TotalScore)
            _playerSaveData.TotalScore = score;

        TotalScoreChanged?.Invoke();
        MyYandex.Instance.SetLiderboardValue(score);
        SaveData();
    }

    public void AddCoinsCount(int count)
    {
        var currentCount = _playerSaveData.TotalCoins;
        var newCount = currentCount + count;

        SetNewTotalCoinsCout(newCount);
    }

    public void SetNewTotalCoinsCout(int count)
    {
        _playerSaveData.TotalCoins = count;

        if (_playerSaveData.TotalCoins < 0)
            _playerSaveData.TotalCoins = 0;

        TotalCoinsCountChanged?.Invoke();
        SaveData();
    }

    public void ChangeCurrentSkin(int skinIndex)
    {
        _playerSaveData.CurrentSkinIndex = skinIndex;

        CurrentSkinChanged?.Invoke();
    }

    public void BuyNewSkin(int skinIndex)
    {
        if(_playerSaveData.MySkinsIndexes.Contains(skinIndex) == false)
        {
            _playerSaveData.MySkinsIndexes.Add(skinIndex);
        }

        MySkinsCountChanged?.Invoke();
    }

    public void SaveData()
    {
        string jsonString = JsonUtility.ToJson(_playerSaveData);
#if !UNITY_EDITOR
        SaveExtern(jsonString);
#endif
    }

    public void LoadData(string value)
    {
        _playerSaveData = JsonUtility.FromJson<PlayerData>(value);
    }
}
