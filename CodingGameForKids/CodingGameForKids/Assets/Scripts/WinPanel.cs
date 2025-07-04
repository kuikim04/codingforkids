using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }
}
