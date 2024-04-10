using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueBox : MonoBehaviour, IPointerClickHandler
{
    Dialogue _dialogue;
    [SerializeField] BoucingSize skipDot; // appear when you can advance the dialogue
    public void InitDialogueBox(Dialogue dial)
    {
        if (skipDot) skipDot.gameObject.SetActive(false);
        
        _dialogue = dial;
        _dialogue.onSentenceTyped.AddListener(() => {skipDot.gameObject.SetActive(true);});
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (skipDot){
            if (skipDot.gameObject.activeSelf) skipDot.gameObject.SetActive(false); 
        }
        _dialogue.AdvanceDialogue(); 
    }

}
