using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    [SerializeField] GameObject LockOne;
    [SerializeField] GameObject LockTwo;
    [SerializeField] GameObject LockThree;

    [SerializeField] DataHandler dataHandler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        dataHandler.Load();

    }

    // Update is called once per frame
    void Update()
    {
        LockOne.SetActive(true);
        LockTwo.SetActive(true);
        LockThree.SetActive(true);
        levelManager();
    }

    public void levelManager()
    {
        switch (dataHandler.unlockedScene)
        {
            case 1:
                Debug.Log("Level 1 Unlocked");
                LockOne.SetActive(false);
                break;
            case 2:
                Debug.Log("Level 1 Unlocked");
                LockOne.SetActive(false);
                break;
            case 3:
                Debug.Log("Level 2 Unlocked");
                LockOne.SetActive(false);
                LockTwo.SetActive(false);
                break;
            case 4:
                Debug.Log("Level 2 Unlocked");
                LockOne.SetActive(false);
                LockTwo.SetActive(false);
                LockThree.SetActive(false);
                break;
            default:
                Debug.Log("Level 0");
                LockOne.SetActive(false);
                break;
        }
    }
}
