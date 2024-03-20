using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

   
    [SerializeField] float bulletSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] ParticleSystem crashParticle;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider trigger)
    {
        Debug.Log("Bullet Hit");
        crashParticle.Play();
        Destroy(gameObject, 0.15f);
        
        if (trigger.CompareTag("Turret"))
        {
            Destroy(trigger.gameObject);
        }
    }
}
