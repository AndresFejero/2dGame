using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyScript : MonoBehaviour
{
    GameObject player;

    EnemyAttributes attributes;
    PlayerAttributes playerAttributes;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        attributes = this.gameObject.GetComponent<EnemyAttributes>();
        playerAttributes = player.GetComponent<PlayerAttributes>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attributes.fleeing == true)
        {
            this.transform.position += (this.transform.position - player.transform.position).normalized * attributes.movementSpeed * Time.deltaTime;
        }
    }
}
