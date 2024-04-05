using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Digicode : MonoBehaviour
{
    [SerializeField] int maxNumberOfLetters;
    [SerializeField] TextMeshProUGUI uiVisualisation;
    int codeIndex;
    string[] code;

    public static UnityEvent<string,Digicode> onValidate = new UnityEvent<string,Digicode>();

    private void Start()
    {
        InitButtons();
        ClearCode();
    }

    void InitButtons()
    {
        DigicodeButton[]buttons = GetComponentsInChildren<DigicodeButton>();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onButtonPressed.AddListener(AddCode);
        }
    }

    void AddCode(string letter, bool reset, bool validate)
    {
        if (validate) CheckCode(string.Join("", code));
        if (reset)
        {
            ClearCode();
            return;
        }
        if(codeIndex > maxNumberOfLetters - 1)
        {
            codeIndex = 0;
            for(int i = code.Length-1; i > 0; i--)
            {
                code[i] = code[i-1];
            }
            code[0] = letter;
        }

        code[codeIndex] = letter;
        codeIndex++;

        uiVisualisation.text = string.Join(" ", code);
    }

    void CheckCode(string code)
    {
        Debug.Log("Trying to find the code " + code);
        onValidate?.Invoke(code,this);
    }

    void ClearCode()
    {
        code = new string[maxNumberOfLetters];
        codeIndex = 0;
        for(int i = 0; i<maxNumberOfLetters; i++)
        {
            code[i] = "_";
        }

        uiVisualisation.text = string.Join(" ", code);
    }
}
