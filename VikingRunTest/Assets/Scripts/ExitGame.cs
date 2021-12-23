using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExitSwitcher : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData e)
    {
        Application.Quit();
    }
}
