using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
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

    public void Update()
    {
        if (attributes.attacking == true)
        {
            this.gameObject.SetActive(false);
            playerAttributes.DamageTaken(attributes.attDamage);
        }

        if (attributes.chasing == true)
        {
            this.transform.position += (player.transform.position - this.transform.position).normalized * attributes.movementSpeed * Time.deltaTime;
        }
    }

}
