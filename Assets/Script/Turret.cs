using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] Transform transformShoot;
    [SerializeField] Transform targetPlayer;
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] float timeShoot;
    float timer;
    [SerializeField] float intervalShoot;
    bool isShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        timer = intervalShoot;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        TargetPlayer(targetPlayer);
        Shoot();


    }

    void TargetPlayer(Transform target)
    {
      transform.LookAt(target, Vector3.forward);
    }

    void Timer()
    {
        timer = timer - Time.deltaTime;
        if (timer < 0)
        {
            timer = intervalShoot;
            isShoot = false;
            Debug.Log("Turret Shoot");
            
        }

        else if (timer > 0)
        {
            isShoot = true;
        }
    }
    void Shoot()
    {
        if (!isShoot)
        {
            GameObject bulletObject = Instantiate(bullet, transformShoot.position, transformShoot.rotation);
            bulletObject.GetComponent<Rigidbody>().AddForce(bulletObject.transform.forward * bulletSpeed, ForceMode.Impulse);
            Destroy(bulletObject, 3f);
        }
    }

}
