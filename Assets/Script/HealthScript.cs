using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
   public int healthPlayer;
   [HideInInspector] public float tempInvulnerableDuration = 3f;
   [HideInInspector] public float invulnerableDuration = 3f;

    [SerializeField] CollisionSyntax collision;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void healthInvul ()
    {
        healthPlayer = 99;
    }
    // Update is called once per frame
    void Update()
    {
        collision.Invulnerable();
    }    
}
