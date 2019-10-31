using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttributes : MonoBehaviour
{
    EnemyBehaviour uniqueEnemyBehaviour;

    public float attRange;
    public int attDamage;

    public int maxHealthPoints;
    public int healthPoints;
    public float reactionRange;
    public float movementSpeed;
    public float distanceToPlayer;

    public bool attacking;
    public bool fleeing;
    public bool roaming;
    public bool guarding;
    public bool chasing;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void Update()
    {

        if (this.gameObject != null)
        {
            Fleeing();
            Attacking();
            Chasing();
        }         
        Fleeing();
        Attacking();
        Chasing();        
    }

    public void Fleeing()
    {
        if (Vector2.Distance(player.transform.position, this.transform.position) < reactionRange)
        {
            fleeing = true;
        }
        else
        {
            fleeing = false;
        }
    }

    public void Attacking()
    {
        if (Vector2.Distance(player.transform.position, this.transform.position) < attRange)
        {
            attacking = true;
        }
        else
        {
            attacking = false;
        }
    }

    public void Chasing()
    {
        if (Vector2.Distance(player.transform.position, this.transform.position) < reactionRange || healthPoints < maxHealthPoints) //Skal nok ændres (lige nu jagtes man hvis man er indenfor
            //en radius eller hvis man skader mobs//
        {
            chasing = true;
        }
        else
        {
            chasing = false;
        }
    }

    //public void Roaming()
    //{

    //}

    //public void Guarding()
    //{
    //    //Kode fra box orders i hookGame//
    //}

    //public void Relaxing()
    //{

    //}

    public void DealDamage(int damage)
    {
        healthPoints -= damage;

        if (healthPoints == 0)
        {
            healthPoints = maxHealthPoints;
            chasing = false;
            fleeing = false;
            this.gameObject.SetActive(false);
        }
    }

}
