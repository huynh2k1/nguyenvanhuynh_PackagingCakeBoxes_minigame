using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : MonoBehaviour
{
    public void Show(bool isShow)
    {
        transform.DOKill();
        if (isShow)
        {
            transform.localScale = Vector3.zero;
            gameObject.SetActive(isShow);
            transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.Linear);
        }
        else
        {
            transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                gameObject.SetActive(isShow);
            });
        }
    }
}
