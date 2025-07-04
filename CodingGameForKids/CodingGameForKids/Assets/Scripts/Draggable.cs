using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int id;
    public int order;

    private Vector3 startPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SoundManager.instance.PlayDragSound();

        startPosition = transform.position;
        originalParent = transform.parent;

        transform.SetParent(transform.root);
        transform.SetAsLastSibling(); 
        canvasGroup.blocksRaycasts = false; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (eventData.pointerEnter != null)
        {
            Slot targetSlot = eventData.pointerEnter.GetComponent<Slot>();

            if (targetSlot != null)
            {
                if (targetSlot.transform.childCount > 0)
                {
                    ReturnToStart();
                    return;
                }

                if (targetSlot.slotStart)
                    SoundManager.instance.PlayPlacedSound();
                else if(this.id != targetSlot.id || this.order != targetSlot.order)
                    SoundManager.instance.PlayWrongSound();
                else
                    SoundManager.instance.PlayPlacedSound();

                transform.SetParent(targetSlot.transform);
                transform.position = targetSlot.transform.position;
                SlotManager.instance.Placed();
                return;

            }
        }

        ReturnToStart();
    }

    void ReturnToStart()
    {
        transform.SetParent(originalParent);
        transform.position = startPosition;
    }

}
