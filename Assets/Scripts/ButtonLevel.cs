using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ButtonLevel : MonoBehaviour
{
    public List<GameObject> listStarActive;
    public TMP_Text textLevel;
    public GameObject log;
    public Button btn;
    public int id;

    private void OnEnable()
    {
        LoadData(PrefData.GetStarActive(id));
        btn.onClick.AddListener(OnClick);
        UpdateText();
        UpdateStateStar(PrefData.GetStarActive(id));
    }

    private void OnDisable()
    {
        btn.onClick.RemoveListener(OnClick);    
        DeActiveAllStar();
    }

    public void UnLockLevel()
    {
        btn.interactable = true;
        textLevel.gameObject.SetActive(true);
        log.SetActive(false);
    }

    private void UpdateText()
    {
        textLevel.text = (id + 1).ToString();
    }

    private void UpdateStateStar(int num)
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

    public void LoadData(int value)
    {
        for(int i = 0; i < value; i++)
        {
            listStarActive[i].SetActive(true);
        }
    }

    private void OnClick()
    {
        if (id > GameManager.instance.levelSO.listLevels.Count - 1)
            return;
        PrefData.CurLevel = id;
        GameManager.instance.ChangeState(GameState.GenerateLevel, id);
        CanvasManager.instance.ShowHome(false);
        PopupController.instance.ShowPopupSelectLevel(false);
    }
}
