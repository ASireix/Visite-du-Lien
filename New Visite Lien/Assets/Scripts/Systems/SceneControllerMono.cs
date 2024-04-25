using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerMono : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string mainMenuSceneName;
    [SerializeField] string mainGameSceneName;

    public IEnumerator LoadGame(){
        AsyncOperation operation = SceneManager.LoadSceneAsync(mainGameSceneName,LoadSceneMode.Additive);
        while(!operation.isDone){
            yield return new WaitForEndOfFrame();
        }
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(mainGameSceneName));
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
