using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIPopupWobble : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(1, 1, 1);
    [SerializeField] Transform targetTransform;
    public float animationDuration = 0.1f;

    private void Awake()
    {
        targetTransform.localScale = Vector3.zero;
    }

    public void ShowPopup()
    {
        targetTransform.localScale = Vector3.zero;
        targetTransform.DOScale(targetScale, animationDuration);
    }

    public void HidePopup()
    {
        targetTransform
             .DOScale(Vector3.zero, animationDuration).OnComplete(()
             =>
             gameObject.SetActive(false)); 
    }
}
