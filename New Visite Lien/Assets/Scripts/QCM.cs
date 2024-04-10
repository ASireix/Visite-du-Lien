using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QCM : MonoBehaviour
{
    [SerializeField] string question;

    [SerializeField] string[] reponses;
    [SerializeField] int reponseID;

    [System.NonSerialized]
    public UnityEvent onAnswerWrong = new UnityEvent();

    [System.NonSerialized]
    public UnityEvent onAnswerRight = new UnityEvent();

    public void AskQuestion(){
        
    }

    public void AnswerQuestion(int id){
        if (id == reponseID){
            onAnswerRight?.Invoke();
        }else{
            onAnswerWrong?.Invoke();
        }
    }
}
