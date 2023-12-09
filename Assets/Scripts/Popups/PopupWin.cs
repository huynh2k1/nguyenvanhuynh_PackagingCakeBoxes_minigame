using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupWin : BasePopup
{
    public List<GameObject> listStarActive;
    public Button btnHome;
    public Button btnNext;
    public Button btnReplay;

    private void OnEnable()
    {
        btnHome.onClick.AddListener(OnClickHome);
        btnNext.onClick.AddListener(OnClickNext);
        btnReplay.onClick.AddListener(OnClickReplay);
        UpdateStar(GameManager.instance.score);
    }
    private void OnDisable()
    {
        btnHome.onClick.RemoveListener(OnClickHome);
        btnNext.onClick.RemoveListener(OnClickNext);
        btnReplay.onClick.RemoveListener(OnClickReplay);
        DeActiveAllStar();
    }

    public void UpdateStar(int num)
    {
        for(int i = 0; i < num; i++)
        {
            listStarActive[i].SetActive(true);
        }
    }
    private void DeActiveAllStar()
    {
        foreach(GameObject star in listStarActive)
        {
            star.SetActive(false);
        }
    }

    private void OnClickHome()
    {
        GameManager.instance.ChangeState(GameState.Home);
        Show(false);
    }
    private void OnClickReplay()
    {
        GameManager.instance.ChangeState(GameState.GenerateLevel, PrefData.CurLevel);
        Show(false);
    }
    private void OnClickNext()
    {
        PrefData.CurLevel = PrefData.LevelUnlocked;
        GameManager.instance.ChangeState(GameState.GenerateLevel, PrefData.LevelUnlocked);
        Show(false);
    }
}
