using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float shootingInterval = 1f;
    private float shootingTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        shootingTimer += Time.deltaTime;

        if (shootingTimer >= shootingInterval && Input.GetButton("Fire1"))
        {
            Shoot();
            shootingTimer = 0f;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}