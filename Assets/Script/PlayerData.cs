using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int unlockedScene = 1;

    public PlayerData (DataHandler data)
    {
        unlockedScene = data.unlockedScene;
    }
}