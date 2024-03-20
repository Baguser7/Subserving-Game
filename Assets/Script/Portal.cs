using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject portalOut;
    [SerializeField] GameObject player;

    Vector3 portalPos;
    Vector3 playerPos;
   public Vector3 calcuPos;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        portalPos = portalOut.transform.position;
        calcuPos = portalPos;
    }
}
