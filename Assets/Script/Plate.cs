using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] CollisionSyntax collisionScript;
    public GameObject gate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider trigger)
    {
        if (collisionScript.isTransitioning ||collisionScript.collisionDisabled)
        { return; }

        switch (trigger.gameObject.tag)
        {

            case "Pickable":
                Destroy(gate);
                Debug.Log("Pickable Placed");
                break;
        }
    }
}
