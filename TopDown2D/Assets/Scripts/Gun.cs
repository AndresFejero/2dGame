using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    public Transform player;

    public int damage;
    public float coolDown;
    public int range;
    public float projectileSpeed;
    public float spread;
    public int bulletsPerShot;

    bool isOnCD;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnCD && this.transform.parent == player.transform)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Shoot();
                StartCoroutine(FireCapMethod(coolDown));
            }
        }
    }

    void Shoot()
    {
        for (int i = 1; i <= bulletsPerShot; i++)
        {
            float rotation = Random.Range(-spread, spread);
            Quaternion bulletRotation = player.rotation;
            bulletRotation *= Quaternion.Euler(0, 0, rotation);
            GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
        }
    }

    IEnumerator FireCapMethod(float cooldown)
    {
        isOnCD = true;
        yield return new WaitForSecondsRealtime(cooldown);
        isOnCD = false;
    }
}
