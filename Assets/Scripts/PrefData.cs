using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefData
{
    public static int CurLevel
    {
        get => PlayerPrefs.GetInt("Cur_Level", 0);
        set => PlayerPrefs.SetInt("Cur_Level", value);
    }
    public static int LevelUnlocked
    {
        get => PlayerPrefs.GetInt("Level_Unlocked", 0);
        set => PlayerPrefs.SetInt("Level_Unlocked", value);
    }
    public static int GetStarActive(int idLevel)
    {
        return PlayerPrefs.GetInt("Star_Number" + idLevel, -1);
    }
    public static void SetStarActive(int idLevel, int value)
    {
        PlayerPrefs.SetInt("Star_Number" + idLevel, value);
    }
}