using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject cake;
    public GameObject tick;

    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);   
    }

    public void InitTarget()
    {
        cake.SetActive(true);
        tick.SetActive(false);
    }

    public void TargetSuccess()
    {
        DOVirtual.DelayedCall(0.5f, () =>
        {
            cake.SetActive(false);
            tick.SetActive(true);  

        });
    }
}
