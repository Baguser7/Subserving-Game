using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionSyntax : MonoBehaviour
{
    public float delayDuration = 1f;
    
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip finishAudio;

    [SerializeField] ParticleSystem finishParticle;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem invulParticle;
    [SerializeField] ParticleSystem powerParticle;

    [SerializeField] Movement movementScript;
    [SerializeField] Portal portalScript;

    [SerializeField] float addThrust = 200f;

    [SerializeField] HealthScript health;
    [SerializeField] Fuel fuelScript;
    [SerializeField] Shoot shootScript;

    AudioSource audioSource;

    [HideInInspector] public GameObject locked;

    [HideInInspector] public Canvas winCanvas;
    [HideInInspector] public Canvas loseCanvas;

    public bool isTransitioning = false;
    public bool collisionDisabled = false;
    public bool isInvulnerable = false;
    public bool isDestroyed = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        locked = GameObject.FindWithTag("Locked");
    }

     void Update()
    {
        FuelHave();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled)
        { return;  }

            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Hit Friendly");
                    break;

                case "Obstacle":
                    Debug.Log("Hit Obstacle");
                    Crash();
                    break;

                case "Finish":
                    Debug.Log("Congrats");
                    NextProcess();
                    break;

                default:
                    Debug.Log("Hit Something");
                    break;
            }
    }

     void OnTriggerEnter(Collider trigger)
    {
        if (isTransitioning || collisionDisabled)
        { return; }

        switch (trigger.gameObject.tag)
        {
           
            case "PowerUp":
                Destroy(trigger.gameObject);
                Debug.Log("Hit PowerUp");
                powerParticle.Play();

                movementScript.thrustPower += addThrust;
                break;

            case "Fuel":
                Destroy(trigger.gameObject);
                Debug.Log("Hit Fuel");
                powerParticle.Play();

                fuelScript.fuelTotal += 100;
                break;

            case "Ammo":
                Destroy(trigger.gameObject);
                Debug.Log("Hit Ammo");
                powerParticle.Play();

                shootScript.bulletHave += 3;
                break;

            case "Key":
                Destroy(trigger.gameObject);
                powerParticle.Play();
                Destroy(locked);
                break;

            case "portalIN":
                isTransitioning = true;
                this.transform.position = portalScript.calcuPos;
                break;

            case "Bullet":
                Debug.Log("Being Shoot");
                isTransitioning = true;
                audioSource.Stop();
                audioSource.PlayOneShot(crashAudio);

                crashParticle.Play();
                health.healthPlayer--;
                break;
        }
    }

    void Crash()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);
    
        crashParticle.Play();

        health.healthPlayer--;
        //add particle
    }

    public void FuelHave()
    {
        if (!fuelScript.hasFuel && isDestroyed == false)
        {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(crashAudio);

            crashParticle.Play();
            GetComponent<Movement>().enabled = false;
            isDestroyed = true;
            Invoke("LoseCanvas", delayDuration);
            Debug.Log("Lost Fuel");
        }
    }

    public void Invulnerable()
    {

        if (health.healthPlayer < 1 && isDestroyed == false)
        {
            lose();
        }
        else if (isTransitioning && health.healthPlayer > 0 && isDestroyed  == false)
        {
           
            isInvulnerable = true;
                if (isInvulnerable)
                {
                    //Debug.Log(health.tempInvulnerableDuration);
                    health.tempInvulnerableDuration -= Time.deltaTime;
                invulParticle.Play();
                    if (health.tempInvulnerableDuration < 0)
                    {
                        isTransitioning = false;
                        isInvulnerable = false;
                    }

                if (isInvulnerable == false)
                {
                    health.tempInvulnerableDuration = health.invulnerableDuration;
                    Debug.Log("invulnerability Over");
                    invulParticle.Stop();
                }
            }
                
        }
    }

    public void lose()
    {
        isDestroyed = true;
        GetComponent<Movement>().enabled = false;
        Invoke("LoseCanvas", delayDuration);
    }
   public void LoseCanvas()
    {
        GameObject tempObject = GameObject.FindGameObjectWithTag("Lose");
        if (tempObject != null)
        {
            loseCanvas = tempObject.GetComponent<Canvas>();
            loseCanvas.enabled = true;
        }
    }

    void WinCanvas()
    {
        GameObject tempObject = GameObject.FindGameObjectWithTag("Win");
        if (tempObject != null)
        {
            winCanvas = tempObject.GetComponent<Canvas>();
            winCanvas.enabled = true;
        }
    }

  public void NextProcess()
    {
        //add sfx
        //add particle
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(finishAudio);

        finishParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("WinCanvas", delayDuration);
    }
}
