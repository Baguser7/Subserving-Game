using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] HealthScript healthScript;
    [SerializeField] TMP_Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Health();
    }

    void Health()
    {
        healthText.text = string.Format("{0} {1}", "Health :", healthScript.healthPlayer);
    }
}
