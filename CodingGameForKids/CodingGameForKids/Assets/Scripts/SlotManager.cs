using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public static SlotManager instance;

    public Slot[] slots;
    public Slot[] slotAll;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Placed()
    {
        int count = 0;

        foreach (Slot slot in slotAll)
        {
            if (slot.transform.childCount > 0)
            {
                count++;
            }
        }

        if (count == slots.Length) 
        {
            Debug.Log("ครบทุกช่องที่ต้องวาง ลองเช็คคำตอบ...");
            CheckWinCondition();
        }
    }


    public void CheckWinCondition()
    {
        foreach (Slot slot in slots)
        {
            if (slot.transform.childCount == 0)
            {
                GameMainMenu.instance.GameLose();
                return;
            }

            Draggable drag = slot.transform.GetChild(0).GetComponent<Draggable>();

            if (drag == null || drag.id != slot.id || drag.order != slot.order)
            {
                GameMainMenu.instance.GameLose();
                return;
            }
        }

        GameMainMenu.instance.GameWin();
    }
}
