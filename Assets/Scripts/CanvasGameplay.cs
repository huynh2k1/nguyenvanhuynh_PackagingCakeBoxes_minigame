using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameplay : MonoBehaviour
{
    public TMP_Text timeText;
    public Button btnHome;
    public Button btnReplay;

    private void OnEnable()
    {
        btnHome.onClick.AddListener(OnClickHome);
        btnReplay.onClick.AddListener(OnClickReplay);
    }

    private void OnDisable()
    {
        btnHome.onClick.RemoveListener(OnClickHome);    
        btnReplay.onClick.RemoveListener(OnClickReplay);
    }

    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }

    private void OnClickHome()
    {
        GameManager.instance.ChangeState(GameState.Home);
    }
    private void OnClickReplay()
    {
        GameManager.instance.ChangeState(GameState.GenerateLevel, PrefData.CurLevel);
    }

    public void UpdateTextTime(int time)
    {
        timeText.text = IntToTime(time);
    }

    string IntToTime(int time)
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);

        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
