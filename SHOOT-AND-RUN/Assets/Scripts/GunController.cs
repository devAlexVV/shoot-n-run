using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    public Animator animator;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform firePoint;

    [SerializeField]
    Vector3 bulletRotation;

    [SerializeField]
    float bulletSpeed = 50.0F;

    [SerializeField]
    float lifeTime = 5.0F;

    [SerializeField]
    int maximumAmmunition = 16;

    [SerializeField]
    float realoadTime = 2.0F;

    [SerializeField]
    public int fireRate = 4;

    [SerializeField]
    StarterAssetsInputs _starterAssetsInputs;

    

    float _currentAmmunition = -1;

    public float _nextFireTime = 0.0F;

    bool _isReloading = false;

    void Start()
    {
        if (_currentAmmunition == -1)
        {
            _currentAmmunition = maximumAmmunition;
        }
    }

    void Update()
    {
        if (_isReloading)
        {
            return;
        }

        if (_currentAmmunition <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time > _nextFireTime)
        {
            _nextFireTime = Time.time + 1.0F / fireRate;
            animator.SetTrigger("shoot");
           // Shoot();
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(bulletRotation));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (_starterAssetsInputs != null && _starterAssetsInputs.aim)
        {
            Vector3 mouseWorldPosition = Vector3.zero;
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2.0F, Screen.height / 2.0F);
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 1000.0F))
            {
                mouseWorldPosition = raycastHit.point;
            }

            Vector3 aimDirection = (mouseWorldPosition - firePoint.position).normalized;

            rb.velocity = aimDirection * bulletSpeed;
        }
        else
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }
       
        Destroy(bullet, lifeTime);

        _currentAmmunition--;
    }

    IEnumerator Reload()
    {
        _isReloading = true;

       // animator.SetBool("reload", true);

        yield return new WaitForSeconds(realoadTime);

        _currentAmmunition = maximumAmmunition;

        //animator.SetBool("reload", false);
        _isReloading = false;
    }

   
}
