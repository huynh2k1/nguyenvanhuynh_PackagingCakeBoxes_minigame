using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupLose : BasePopup
{
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
        btnReplay?.onClick.RemoveListener(OnClickReplay);
    }

    private void OnClickHome()
    {
        Show(false);
        GameManager.instance.ChangeState(GameState.Home);
    }
    private void OnClickReplay()
    {
        Show(false);
        GameManager.instance.ChangeState(GameState.GenerateLevel, PrefData.CurLevel);
    }

}
