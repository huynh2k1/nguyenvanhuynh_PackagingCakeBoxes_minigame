using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupHowToPlay : BasePopup
{
    public Button btnClose;

    private void OnEnable()
    {
        btnClose.onClick.AddListener(OnClickBtnClose);
    }
    private void OnDisable()
    {
        btnClose.onClick.RemoveListener(OnClickBtnClose);
    }

    void OnClickBtnClose()
    {
        Show(false);
    }

    public void Show(bool isShow)
    {
        base.Show(isShow);
    }
}
