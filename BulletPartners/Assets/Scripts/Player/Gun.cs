using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform attackPoint;

    [SerializeField] private float spread;
    [SerializeField] private float cooldown;
    [SerializeField] private float reloadTime;
    [SerializeField] private float shootForce;
    [SerializeField] private float damage;
    [SerializeField] private bool reloading;
    [SerializeField] private bool canShoot;
    [SerializeField] private int bulletsPerShot;
    public int bulletsPerMag;
    public int currentBullets;

    private void Awake()
    {
        currentBullets = bulletsPerMag;
    }

    public void Shoot()
    {
        if(canShoot && !reloading && currentBullets > 0)
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                Vector3 dir = transform.right;

                float zOffset = Random.Range(spread, -spread);
                float xOffset = Random.Range(spread, -spread);

                Vector3 dirWithSpread = dir + new Vector3(xOffset, 0, zOffset);

                GameObject firedBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

                firedBullet.transform.forward = dirWithSpread.normalized;

                firedBullet.GetComponent<Rigidbody>().AddForce(dirWithSpread.normalized * shootForce, ForceMode.Impulse);
                firedBullet.GetComponent<Bullet>().bulletDamage = damage;

                currentBullets--;
            }

            Invoke(nameof(ResetShot), cooldown);

            canShoot = false;
        }else if(canShoot && !reloading && currentBullets == 0)
        {
            Invoke(nameof(Reload), reloadTime);
            reloading = true;
        }

    }

    private void ResetShot()
    {
        canShoot = true;
    }

    private void Reload()
    {
        currentBullets = bulletsPerMag;
        reloading = false;
    }
}
