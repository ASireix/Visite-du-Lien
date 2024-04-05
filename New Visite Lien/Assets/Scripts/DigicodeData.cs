using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Code",menuName ="Digicode/Code")]
public class DigicodeData : ScriptableObject
{
    public string code;

    public virtual void OnCodeCracked() { }
}
