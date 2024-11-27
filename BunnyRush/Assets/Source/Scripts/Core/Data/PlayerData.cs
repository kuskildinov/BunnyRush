using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int TotalScore;
    public int TotalCoins;
    public bool SoundOn;
    public int CurrentSkinIndex;
    public List<int> MySkinsIndexes;

    public PlayerData()
    {
        TotalScore = 0;
        TotalCoins = 0;
        SoundOn = true;
        CurrentSkinIndex = 0;
        MySkinsIndexes = new List<int>();
        MySkinsIndexes.Add(CurrentSkinIndex);
    }
}
