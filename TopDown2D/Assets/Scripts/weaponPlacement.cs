using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPlacement : MonoBehaviour
{
    public float x;
    public float y;
    public float rotation;

    Transform player;

    bool rotationComplete;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void PlaceWeapon()
    {

        Vector3 placementAdj = new Vector3(x, y, 0);
        transform.position = player.position + placementAdj;

        Quaternion weaponRotation = player.rotation;
        weaponRotation *= Quaternion.Euler(0, 0, 180-rotation);
        transform.rotation = weaponRotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !rotationComplete)
        {
            PlaceWeapon();
            rotationComplete = true;
        }
    }
}
