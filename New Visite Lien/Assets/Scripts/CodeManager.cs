using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeManager : MonoBehaviour
{
    [SerializeField] string masterCode;
    [SerializeField] List<DigicodeData> digicodeDatas;
    Dictionary<string, DigicodeData> codeDico = new Dictionary<string, DigicodeData>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in digicodeDatas)
        {
            codeDico.TryAdd(item.code, item);
        }
        Digicode.onValidate.AddListener(CheckCodeFromManager);    
    }


    void CheckCodeFromManager(string code, Digicode digicode)
    {
        if (code.Equals(masterCode))
        {
            UnlockAll();
            return;
        }
        if(codeDico.TryGetValue(code, out DigicodeData digicodeData))
        {
            digicodeData.OnCodeCracked();
        }
        else
        {
            Debug.Log("Incorrect code");
        }
        
    }

    void UnlockAll()
    {
        foreach (var item in digicodeDatas)
        {
            item.OnCodeCracked();
        }
    }
}
