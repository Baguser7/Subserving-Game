using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [HideInInspector] public Light yLight;
    [HideInInspector] public Light gLight;

    [HideInInspector] public Transform objTransform, rocTransform;
    [HideInInspector] public Rigidbody objRigid;
    [HideInInspector] public ConstantForce objConstForce;
    [SerializeField] ParticleSystem dragParticle;

    [SerializeField] GameObject pickButton;
    [SerializeField] GameObject dropButton;
    bool isInteractable = false;
    bool isPicking = false;
    void OnTriggerStay(Collider trigger)
    {
        if (trigger.CompareTag("Pickable"))
        {
            GameObject tempObject = GameObject.FindGameObjectWithTag("yLight");
            if (tempObject != null)
            {
                yLight = tempObject.GetComponent<Light>();

                yLight.enabled = true;
                isInteractable = true;
                dragParticle.Play();
            }
        }
    }

    void OnTriggerExit(Collider trigger)
    {
        if (trigger.CompareTag("Pickable"))
        {
            GameObject tempObject = GameObject.FindGameObjectWithTag("yLight");
            if (tempObject != null)
            {
                yLight = tempObject.GetComponent<Light>();

                yLight.enabled = false;
                isInteractable = false;
                dragParticle.Stop();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        dragAndDropButton();
        dragAndDrop();
    }

    bool pickBoll;
    public void Pick(bool value)
    {
        pickBoll = value;
    }

    bool dropBoll;
    public void Drop(bool value)
    {
        dropBoll = value;
    }

    void dragAndDrop()
    {
        if (isInteractable == true)
        {
            GameObject Pickable = GameObject.FindGameObjectWithTag("Pickable");
            objTransform = Pickable.GetComponent<Transform>();
            objRigid = Pickable.GetComponent<Rigidbody>();
            objConstForce = Pickable.GetComponent<ConstantForce>();
            rocTransform = GetComponent<Transform>();
            if (Pickable != null)
            {
                if (pickBoll)
                {

                    objTransform.parent = rocTransform;
                    objRigid.constraints = RigidbodyConstraints.FreezePosition;
                    objRigid.useGravity = false;
                    objConstForce.enabled = true;
                    isPicking = true;
                    if (!dragParticle.isPlaying)
                    {
                        dragParticle.Play();
                    }


                }
                if (dropBoll)
                {
                    objTransform.parent = null;
                    objRigid.constraints = RigidbodyConstraints.None;

                    objRigid.useGravity = true;
                    isPicking = false;
                    dragParticle.Stop();
                }
            }
        }
    }

    void dragAndDropButton()
    {
        if (isPicking == false)
        {
            pickButton.SetActive(true);
            dropButton.SetActive(false);
        }
        else
        {
            pickButton.SetActive(false);
            dropButton.SetActive(true);
        }
    }
}
