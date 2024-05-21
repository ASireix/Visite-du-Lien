using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapButtonManager : MonoBehaviour
{
    [SerializeField] List<GameObject> pins;
    [SerializeField] List<MapUpdateButton> mapUpdateButtons;
    [SerializeField] TextMeshProUGUI categorieTitle;
    [SerializeField] int firstSelectId;

    void Start(){
        foreach (var button in mapUpdateButtons)
        {
            button.callPinChange+=ChangePins;
        }
        ChangePins(mapUpdateButtons[firstSelectId].pinHolderID);
    }

    void ChangePins(int id){
        for (int i = 0; i<mapUpdateButtons.Count; i++){
            if (i==id){
                pins[mapUpdateButtons[i].pinHolderID].SetActive(true);
                mapUpdateButtons[i].SelectButton();
                categorieTitle.text = pins[i].name;
            }else{
                pins[mapUpdateButtons[i].pinHolderID].SetActive(false);
                mapUpdateButtons[i].DeselectButton();
            }
        }
    }
}
