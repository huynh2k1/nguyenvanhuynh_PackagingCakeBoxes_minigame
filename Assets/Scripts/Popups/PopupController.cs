using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public static PopupController instance;

    public PopupSelectLevel popupSelectLevel;
    public PopupHowToPlay popupHowToPlay;
    public PopupLose popupLose;
    public PopupWin popupWin;

    private void Awake()
    {
        instance = this;
    }
    public void ShowPopupWin(bool isShow)
    {
        popupWin.Show(isShow);
    }
    public void ShowPopupLose(bool isShow)
    {
        popupLose.Show(isShow);
    }
    public void ShowPopupHowToPlay(bool isShow)
    {
        popupHowToPlay.Show(isShow);    
    }
    public void ShowPopupSelectLevel(bool isShow)
    {
        popupSelectLevel.Show(isShow);
    }
}
