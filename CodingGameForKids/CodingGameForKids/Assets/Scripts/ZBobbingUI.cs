using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ZBobbingUI : MonoBehaviour
{
    public float angle = 5f;      
    public float duration = 1f;   
    public bool playOnStart = true;

    private void Start()
    {
        if (playOnStart)
        {
            StartWobble();
        }
    }

    public void StartWobble()
    {
        transform.DOLocalRotate(new Vector3(0, 0, angle), duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    public void StopWobble()
    {
        transform.DOKill();
        transform.localRotation = Quaternion.identity;
    }
}
