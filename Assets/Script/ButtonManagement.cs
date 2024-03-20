using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonManagement : MonoBehaviour
{
    public Canvas settingCanvas;
    public Canvas credCanvas;
    public GameObject pause;
    bool isSetting = false;
    private GameObject tempObject;
    private GameObject credObject;

    [SerializeField] DataHandler dataHandler;


    [HideInInspector] public int currentScene;
   [HideInInspector] public int nextScene;

     void Update()
    {
         tempObject = GameObject.FindGameObjectWithTag("Setting");
        credObject = GameObject.FindGameObjectWithTag("Credit");
    }
    public void playButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    public void homeButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(0);
    }

    public void selectLevel_One()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(2);
    }

    public void selectLevel_Two()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(3);
    }

    public void selectLevel_Three()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(4);
    }

    public void restartButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void Save()
    {
        dataHandler.Save();
        Debug.Log("Data Saved" + dataHandler.unlockedScene);
    }
    public void nextButton()
    {
        
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        pause.SetActive(false);
        
    }

    public void Pause()
    {
        pause.SetActive(true);

    }
    public void setting()
    {
        if (tempObject != null)
        {
            settingCanvas = tempObject.GetComponent<Canvas>();
            settingCanvas.enabled = isSetting;
            
            isSetting = !isSetting;
        }
    }

    public void Credit()
    {
        if (credObject != null)
        {
            credCanvas = credObject.GetComponent<Canvas>();
            credCanvas.enabled = isSetting;

            isSetting = !isSetting;
        }
    }


}
