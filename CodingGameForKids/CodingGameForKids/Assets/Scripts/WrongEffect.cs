using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WrongEffect : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(1, 1, 1);
    public float popupDuration = 0.3f;
    public float stayDuration = 1f;
    public float hideDuration = 0.3f;

    private void OnEnable()
    {
        ShowPopup();
    }

    public void ShowPopup()
    {
        transform.localScale = Vector3.zero;

        transform.DOScale(targetScale, popupDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(stayDuration, () =>
                {
                    HidePopup();
                });
            });
    }

    public void HidePopup()
    {
        transform.DOScale(Vector3.zero, hideDuration)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}
