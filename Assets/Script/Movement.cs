using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{

    [SerializeField] public float thrustPower = 0;
    [SerializeField] float rotatePower = 0;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainRocketParticle;
    [SerializeField] ParticleSystem rightSideParticle;
    [SerializeField] ParticleSystem leftSideParticle;
    [SerializeField] ParticleSystem hoverParticle;

    [SerializeField] CollisionSyntax collisionSyntax;
    [SerializeField] Fuel fuelScript;
    [SerializeField] DataHandler dataHandler;

    [SerializeField] Vector3 rocketTransform;

    [SerializeField] TMP_Text speedText;
    //Vector3 force;
    Rigidbody rb;
    AudioSource audioSource;
    Collider colliderRocket;
    ConstantForce hover;

    public Canvas pauseCanvas;
    public bool isPause = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        colliderRocket = GetComponent<Collider>();
        //dataHandler.Save();
        //pauseCanvas = pause.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        //force = GetComponent<Transform>().position;
        ProcessThrusting();
        ProcessRotate();
        DebugCheat();
        Pause();
        HoverPlayer();
        Speed();

        //JoyStickInput();
        float inputV = SimpleInput.GetAxisRaw("Vertical");
        float inputH = SimpleInput.GetAxisRaw("Horizontal");

        if (inputV > 0)
        {
            rb.freezeRotation = true; //Freeze rotation before manually take control (override physic)
            transform.eulerAngles = new Vector3(0, 0, -inputH * 90 )* Time.deltaTime;
            rb.freezeRotation = false; //let physic system (rb) take control again
            leftSideParticle.Play();
            rightSideParticle.Stop();
        }
         if (inputV < 0)
        {
            rb.freezeRotation = false; //let physic system (rb) take control again
            transform.eulerAngles = new Vector3(0, 0, (inputH + 2) * 90)* Time.deltaTime;
            rb.freezeRotation = false; //let physic system (rb) take control again
            leftSideParticle.Stop();
            rightSideParticle.Play();
        }
    }
    bool thrustBoll;
    public void Thrust(bool value)
    {
        thrustBoll = value;
    }

    void ProcessThrusting()
    {
        if (Input.GetKey(KeyCode.Space) || thrustBoll)
        {
            StartThrusting();
            fuelScript.UseFuel(fuelScript.thrustFuelUse);
        }
        else
        {
            StopThrusting();
        }
    }

    bool hoverBoll;
    public void Hover(bool value)
    {
        hoverBoll = value;
    }

    public void HoverPlayer()
    {
        hover = GetComponent<ConstantForce>();
        if (Input.GetKey(KeyCode.X) || hoverBoll)
        {
            print("Move");
            hover.enabled = true;
            fuelScript.UseFuel(fuelScript.thrustFuelUse);
            

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }

            if (!hoverParticle.isPlaying)
            {
                hoverParticle.Play();
            }
        }
        else
        {
            hover.enabled = false;
            hoverParticle.Stop();
        }
    }

    bool leftBoll;
    public void Left(bool value)
    {
        leftBoll = value;
    }

    bool rightBoll;
    public void Right(bool value)
    {
        rightBoll = value;
    }
    void ProcessRotate()
    {
        if (leftBoll)
        {
            LeftRotate();
        }

        if (rightBoll)
        {
            RightRotate();
        }

        else
        {
            StopRotate();
        }
    }

    bool pauseBoll;
    public void Pause(bool value)
    {
        pauseBoll = value;
    }
    void Pause()
    {
        GameObject tempObject = GameObject.FindGameObjectWithTag("Pause");
        if (tempObject != null)
        {
            pauseCanvas = tempObject.GetComponent<Canvas>();
            pauseCanvas.enabled = isPause;
            if (pauseBoll)
            {
                isPause = !isPause;
            }
        }
    }

    bool skipBoll;
    public void Skip(bool value)
    {
        skipBoll = value;
    }

    bool invulBoll;
    public void Invul(bool value)
    {
        invulBoll = value;
    }
    void DebugCheat()
    {
        if (Input.GetKeyDown(KeyCode.L) || skipBoll)
        {
            collisionSyntax.NextProcess();
        }

        else if (Input.GetKeyDown(KeyCode.C) || invulBoll)
        {
            Debug.Log("Debug Mode : No Collision");
            collisionSyntax.collisionDisabled = !collisionSyntax.collisionDisabled;
            
        }
    }

    void ApplyRotation(float rotationAxis)
    {
        rb.freezeRotation = true; //Freeze rotation before manually take control (override physic)
        transform.Rotate(Vector3.forward * rotationAxis * Time.deltaTime);

        rb.freezeRotation = false; //let physic system (rb) take control again
    }

    void JoyStickInput()
    {
        //rb.freezeRotation = true; //Freeze rotation before manually take control (override physic)

        //transform.eulerAngles = new Vector3(0, 0, -value * (360 + 90));
        ////if (SimpleInput.GetAxis("Vertical") > 0)
        ////{
        ////    transform.eulerAngles = new Vector3(0, 0, -value * 90);
        ////}
        ////else if (SimpleInput.GetAxis("Vertical") < 0)
        ////{

        ////}

        //rb.freezeRotation = false; //let physic system (rb) take control again

        //transform.Rotate(0, 0, -SimpleInput.GetAxis("Horizontal"));
        transform.Translate(SimpleInput.GetAxis("Horizontal"), 0, SimpleInput.GetAxis("Horizontal"));
    }

    public FixedJoystick fixedJoystick;
    private void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical;
        rb.AddForce(direction * rotatePower * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

   

    public void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
        
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainRocketParticle.isPlaying)
        {
            mainRocketParticle.Play();
        }
        //Debug.Log("Pressed Space");
    }

    public void StopThrusting()
    {
        audioSource.Stop();
        mainRocketParticle.Stop();
    }

    private void RightRotate()
    {
        //Debug.Log("Pressed D");
        ApplyRotation(-rotatePower);
        if (!rightSideParticle.isPlaying)
        {
            rightSideParticle.Play();
        }
    }

    private void LeftRotate()
    {

        //Debug.Log("Pressed A");
        ApplyRotation(rotatePower);
        if (!leftSideParticle.isPlaying)
        {
            leftSideParticle.Play();
        }
    }

    private void StopRotate()
    {
        leftSideParticle.Stop();
        rightSideParticle.Stop();
    }

    void Speed()
    {
        speedText.text = string.Format("{0} {1}", "Speed : ", thrustPower/10, "km/h");
    }
}
