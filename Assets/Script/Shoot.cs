using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject rocket;
    [SerializeField] Transform transformShoot;
    [SerializeField] float bulletSpeed;
    [SerializeField] public int bulletHave;
    [SerializeField] TMP_Text ammoText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ammo();
    }

   public void ShootPlayer()
    {
        if (bulletHave > 0)
        {
                Bullet();
                bulletHave--;
        }
    }

    void Bullet()
    { 
        GameObject bulletObject = Instantiate(bullet, transformShoot.position, transformShoot.rotation);
        bulletObject.GetComponent<Rigidbody>().AddForce(bulletObject.transform.up * bulletSpeed, ForceMode.Impulse);
    }
    
    void Ammo()
    {
        ammoText.text = string.Format("{0} {1}", "Ammo :",  bulletHave);
    }
}
