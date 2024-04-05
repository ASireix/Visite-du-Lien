using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapCode", menuName = "Digicode/Map Code")]
public class MapInfoDigiData : DigicodeData
{
    public GameObject objectToShow;
    public int iD;
    public override void OnCodeCracked()
    {
        base.OnCodeCracked();
        InfoManager.instance.AddInfo(iD,objectToShow);
    }
}
