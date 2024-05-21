using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionLibre : TextBoxContainer
{
    [TextArea]
    [SerializeField] string questionText;
    [SerializeField] string answer;
    [SerializeField] int differenceTolerance;

    ReponseLibre answerField;

    protected override void OnStart()
    {
        base.OnStart();
        answerField = GetComponentInChildren<ReponseLibre>();
        textBox.WriteText(questionText);
    }

    public void CompareAnswers(){
        int dist = DamerauLevenshtein.
        DamerauLevenshteinDistance(answerField.GetAnswer().Trim().ToLower(),answer.Trim().ToLower());
        if (dist<=differenceTolerance){
            Debug.Log("GOOD, diff is "+dist);
            answerField.SetAnswer(answer);
        }else{
            Debug.Log("BAD, diff is "+dist);
        }
    }
}
