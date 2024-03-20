using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataHandler : MonoBehaviour
{
    public int unlockedScene;
    public int currentScene;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        

        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveSystem.SavePlayer(this);
            Debug.Log("Data Saved");
            Debug.Log(unlockedScene);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerData dataPlayer = SaveSystem.LoadSave();
            unlockedScene = dataPlayer.unlockedScene;
            Debug.Log("Load Game");

            Debug.Log(unlockedScene);
        }
        if (currentScene > unlockedScene)
        {
            unlockedScene++;

        }
    }

   public void Save()
    {
        SaveSystem.SavePlayer(this);
        Debug.Log("Data Saved");
        Debug.Log(unlockedScene);

    }

    public void Load()
    {
        PlayerData dataPlayer = SaveSystem.LoadSave();
        unlockedScene = dataPlayer.unlockedScene;
        Debug.Log("Load Game");

        Debug.Log(unlockedScene);
    }
}
