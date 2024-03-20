using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeScript : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;
    [SerializeField] float timeLimit = 0;
    float timeFloat;
    
    [SerializeField] bool isTimeLimit = false;
    [SerializeField] bool isTimeStart = true;
    [SerializeField] CollisionSyntax collisionSyntax;
    // Start is called before the first frame update
    void Start()
    {
        isTimeStart = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        TimerStart();

    }

    private void TimerStart()
    {
        Timer(timeFloat);

        if (isTimeStart == false)
        { 
            timeFloat = 0f;
            timeLimit = 0f;
        }

        if (isTimeStart == true)
        {
            timeFloat = Time.time;

            if (isTimeLimit)
            {
                if (timeLimit > 0)
                {
                    timeLimit -= Time.deltaTime;
                    Timer(timeLimit);
                }
                else
                {
                    isTimeLimit = false;
                    isTimeStart = false;
                    collisionSyntax.lose();
                    Debug.Log("Time Run Out");
                }
            }
        }  
    }

    void Timer(float displayTime)
    {
        float minutes = Mathf.FloorToInt(displayTime / 60);
        float seconds = Mathf.FloorToInt(displayTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
