using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HowToPlay : MonoBehaviour, IPointerClickHandler
{
    public Instruction instruction;
    public void OnPointerClick(PointerEventData e)
    {
        instruction.Show();
    }
}
