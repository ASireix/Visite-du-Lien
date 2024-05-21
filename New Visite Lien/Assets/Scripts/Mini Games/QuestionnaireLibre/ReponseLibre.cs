using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReponseLibre : TextBoxContainer
{
    protected override void OnStart()
    {
        textBox.onClickEvent.AddListener(SelectBox);
    }

    void SelectBox()
    {
        textBox.TrySelectInputField();
    }

    public string GetAnswer()
    {
        return textBox.GetInputText();
    }
    
    public void SetAnswer(string to)
    {
        textBox.TMP_inputField.text = to;
    }
}
