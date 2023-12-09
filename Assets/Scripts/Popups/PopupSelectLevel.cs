using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSelectLevel : BasePopup
{
    public Button btnBackHome;
    public List<ButtonLevel> listButtonLevel;

    private void Start()
    {
    }

    private void OnEnable()
    {
        InitIdButtonLevels();
        GenerateLevelUnlocked(PrefData.LevelUnlocked);
        btnBackHome.onClick.AddListener(OnClickBtnBackHome);
    }

    private void OnDisable()
    {
        btnBackHome.onClick.RemoveListener(OnClickBtnBackHome); 
    }

    private void InitIdButtonLevels()
    {
        for(int i = 0; i < listButtonLevel.Count; i++)
        {
            listButtonLevel[i].id = i;
        }
    }

    private void GenerateLevelUnlocked(int value)
    {
        for(int i = 0; i <= value; i++)
        {
            listButtonLevel[i].UnLockLevel();
        }
    }

    private void OnClickBtnBackHome()
    {
        Show(false);
        CanvasManager.instance.ShowHome(true);
    }
}
