using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    public CanvasHome canvasHome;
    public CanvasGameplay canvasGame;

    private void Awake()
    {
        instance = this;
    }

    public void ShowHome(bool isShow)
    {
        canvasHome.Show(isShow);
    }

    public void ShowUIGame(bool isShow)
    {
        canvasGame.Show(isShow);
    }
}
