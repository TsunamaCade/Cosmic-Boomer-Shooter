using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private LayerMask playerMask;

    [SerializeField] private bool isAuto;

    [SerializeField] private float damage;

    [SerializeField] private float bulletDistance;
    [SerializeField] private float bulletSpread;
    [SerializeField] private float amountToFire;

    [SerializeField] private float timeToShoot;
    [SerializeField] private bool canShoot = true;

    [SerializeField] private GameObject bulletHole;
    [SerializeField] private Material bulletParticleColor;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private ParticleSystem muzzleFlash;

    void OnEnable()
    {
        canShoot = true;
    }

    void Update()
    {
        if(isAuto && canShoot)
        {
            if(Input.GetButton("Fire1"))
            {
                muzzleFlash.Play();
                ShootWeaponAuto();
            }
        }
        else if(!isAuto && canShoot)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                muzzleFlash.Play();
                ShootWeaponSemi();
            }
        }
    }

    void ShootWeaponSemi()
    {
        for (int i = 0; i < amountToFire; i++)
        {
            Vector3 dir = cam.transform.forward + new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread));
            RaycastHit hit;
            if(Physics.Raycast(cam.position, dir, out hit, bulletDistance, playerMask))
            {
                canShoot = false;
                StartCoroutine(ShootAgain());
                if(hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<TakeDamage>().health -= damage;
                    GameObject obj = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }
                else
                {
                    bulletParticleColor.color = hit.transform.GetComponent<Renderer>().material.color;
                    GameObject obj = Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
                    obj.transform.position += obj.transform.forward / 1000;
                }
            }
        }
    }

    void ShootWeaponAuto()
    {
        Vector3 dir = cam.transform.forward + new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread));
        RaycastHit hit;
        if(Physics.Raycast(cam.position, dir, out hit, bulletDistance, playerMask))
        {
            canShoot = false;
            StartCoroutine(ShootAgain());
            if(hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<TakeDamage>().health -= damage;
                GameObject obj = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                bulletParticleColor.color = hit.transform.GetComponent<Renderer>().material.color;
                GameObject obj = Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
                obj.transform.position += obj.transform.forward / 1000;
            }
        }
    }

    IEnumerator ShootAgain()
    {
        yield return new WaitForSeconds(timeToShoot);
        canShoot = true;
    }
}