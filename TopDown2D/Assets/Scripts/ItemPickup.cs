using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    Transform player;

    Vector3 mousePos;

    List<GameObject> inventory = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Weapon")
        {
            collision.transform.SetParent(this.transform);
            inventory.Add(collision.transform.GetComponent<GameObject>());

        }
    }
}



