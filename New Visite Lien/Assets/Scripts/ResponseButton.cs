using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResponseButton : MonoBehaviour, IPointerClickHandler
{
    QCM qcm;
    int iD;
    public void OnPointerClick(PointerEventData eventData)
    {
        qcm.AnswerQuestion(iD);
    }
}
