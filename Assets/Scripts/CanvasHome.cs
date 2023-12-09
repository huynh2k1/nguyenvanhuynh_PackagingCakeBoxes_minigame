using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHome : MonoBehaviour
{
    public Button btnPlay;
    public Button btnHowToPlay;

    public PopupHowToPlay popupHowToPlay;
    public PopupSelectLevel popupSelectLevel;

    private void OnEnable()
    {
        btnPlay.onClick.AddListener(OnClickBtnPlay);
        btnHowToPlay.onClick.AddListener(OnClickBtnHowToPlay);
    }
    private void OnDisable()
    {
        btnPlay.onClick.RemoveListener(OnClickBtnPlay);
        btnHowToPlay.onClick.RemoveListener(OnClickBtnHowToPlay);
    }

    private void OnClickBtnPlay()
    {
        PopupController.instance.ShowPopupSelectLevel(true);

    }
    private void OnClickBtnHowToPlay()
    {
        PopupController.instance.ShowPopupHowToPlay(true);
    }
    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
