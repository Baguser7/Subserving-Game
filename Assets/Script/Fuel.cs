using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fuel : MonoBehaviour
{
    public float fuelTotal = 100;
    public float thrustFuelUse = 1;
    public float hoverFuelUse = 1;
    [SerializeField] TMP_Text fuelText;
    [SerializeField] Movement movementScript;
    [SerializeField] CollisionSyntax collisionSyntax;

   public bool hasFuel = true; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FuelUI();
        CheckFuel();
    }

    public void UseFuel(float fuelUse)
    {
        fuelTotal -= fuelUse * Time.deltaTime;
    }

    void FuelUI()
    {
        fuelText.text = string.Format("{0} {1}", "Fuel :", fuelTotal);
    }

    public void CheckFuel()
    {
        if (fuelTotal < 0)
        {
            hasFuel = false;
        }
        else if (fuelTotal > 0)
        {
            hasFuel = true;
        }
    }

    
}
