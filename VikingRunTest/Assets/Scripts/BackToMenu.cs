using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackToMenu : MonoBehaviour, IPointerClickHandler
{
    public Instruction instruction;
    public void OnPointerClick(PointerEventData e)
    {
        instruction.Hide();
    }
}
