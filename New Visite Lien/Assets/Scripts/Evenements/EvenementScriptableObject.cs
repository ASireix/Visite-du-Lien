using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Evenement", menuName = "Evenement")]
public class EvenementScriptableObject : ScriptableObject
{
    public bool useFlatEvent;
    public Evenement arEvent;
    public Evenement flatEvent;
    public Sprite backgroundImageForFlatEvent;
}