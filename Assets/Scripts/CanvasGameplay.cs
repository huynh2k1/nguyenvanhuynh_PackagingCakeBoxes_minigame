using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameplay : MonoBehaviour
{
    public TMP_Text timeText;
    public List<Target> listTarget;
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

    public void LoadMission(int num)
    {
        foreach(Target obj in listTarget)
        {
            obj.Show(false);
        }
        for (int i = 0; i < num; i++)
        {
            listTarget[i].Show(true);
            listTarget[i].InitTarget();
        }
    }
    public void UpdateMission(int num)
    {
        listTarget[num].TargetSuccess();
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
    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
