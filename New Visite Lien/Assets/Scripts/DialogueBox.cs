using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueBox : MonoBehaviour, IPointerClickHandler
{
    Dialogue _dialogue;
    public void InitDialogueBox(Dialogue dial)
    {
        _dialogue = dial;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _dialogue.AdvanceDialogue();
    }

}
