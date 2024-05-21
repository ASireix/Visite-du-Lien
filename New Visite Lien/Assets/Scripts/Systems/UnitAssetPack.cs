using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitAssetPack",menuName = "Data/Unit asset pack")]
public class UnitAssetPack : ScriptableObject
{
    [SerializeField]
    public DialogueData[] TextFiles;

    [SerializeField]
    public EventData[] EventDatas;
}
