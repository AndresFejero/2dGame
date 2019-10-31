using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMapEnter : MonoBehaviour
{
    ObjectSpawning newMapSpawns;
    Transform cameraYo;
    Transform player;

    private void Start()
    {
        newMapSpawns = GameObject.FindGameObjectWithTag("Tile").GetComponent<ObjectSpawning>();
        cameraYo = GameObject.FindGameObjectWithTag("MainCamera").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (this.transform.name == "ExitTop")
            {
                newMapSpawns.ObjSpawn();
                collision.transform.position = new Vector3(0, -250, 0);
            }

            if (this.transform.name == "ExitBot")
            {
                newMapSpawns.ObjSpawn();
                collision.transform.position = new Vector3(250, 0, 0);
            }

            if (this.transform.name == "ExitLeft")
            {
                newMapSpawns.ObjSpawn();
                collision.transform.position = new Vector3(250, 0, 0);
            }

            if (this.transform.name == "ExitRight")
            {
                newMapSpawns.ObjSpawn();
                collision.transform.position = new Vector3(-250, 0, 0);
            }

            cameraYo.position = player.position;
        }
    }
}
