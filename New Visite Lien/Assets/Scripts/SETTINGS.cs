using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SETTINGS
{
    public static bool isTutorialCompleted
    {
        get
        {
            return PlayerPrefs.GetInt("isTutorialCompleted").Equals(1);
        }
        set
        {
            PlayerPrefs.SetInt("isTutorialCompleted", value ? 1 : 0);
        }
    }
}
