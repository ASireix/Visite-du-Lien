using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    public static InfoManager instance;
    Dictionary<int, GameObject> infoDico = new Dictionary<int, GameObject>();

    [SerializeField] Sprite unlockSprite;
    [SerializeField] Sprite pinSprite;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void ShowInfo(int id, MapButton mapButton = null)
    {
        if (infoDico.TryGetValue(id, out GameObject info))
        {
            info.SetActive(true);
        }
        else
        {
            Debug.Log("Could not find ID");
        }

        if (mapButton) mapButton.SetSprite(unlockSprite);
    }

    public void AddInfo(int id, GameObject info)
    {
        GameObject obj = Instantiate(info);
        obj.transform.SetParent(transform,false);
        obj.SetActive(false);

        infoDico.TryAdd(id, obj);
    }
}
