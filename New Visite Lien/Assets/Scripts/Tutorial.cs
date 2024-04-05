using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject gpsButton;
    [SerializeField] GameObject mapButton;
    [SerializeField] GameObject ARsession;

    [SerializeField] GameObject coworkingBackground;

    [SerializeField] Dialogue dialogueTutorial;

    [SerializeField] Patrick PatrickInScene;

    // Start is called before the first frame update
    void Start()
    {
        if (!SETTINGS.isTutorialCompleted)
        {
            gpsButton.SetActive(false);
            mapButton.SetActive(false);
            ARsession.SetActive(false);
            coworkingBackground.SetActive(true);
            PatrickInScene.gameObject.SetActive(true);

            dialogueTutorial.onDialogueComplete.AddListener(OnFirstDialogueComplete);

            StartCoroutine(DelayStart());
        }
    }


    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(3f);

        dialogueTutorial.TriggerDialogue();
        PatrickInScene.StopWave();
    }

    void OnFirstDialogueComplete()
    {
        gpsButton.SetActive(true);
        mapButton.SetActive(true);
        ARsession.SetActive(true);
        coworkingBackground.SetActive(false);
        PatrickInScene.gameObject.SetActive(false);
    }

    public void ShowMapButton()
    {

    }
}
