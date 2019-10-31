using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int damage;
    float speed;
    float range;
    Vector3 spawnLocation;
    public Rigidbody2D rb;

    Gun gunScript;


    // Start is called before the first frame update
    void Start()
    {
        //Gun script findes i scenen//
        gunScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Gun>();

        //Variablerne fra gun scriptet hentes//
        speed = gunScript.GetComponent<Gun>().projectileSpeed;
        damage = gunScript.GetComponent<Gun>().damage;
        range = gunScript.GetComponent<Gun>().range;

        //Rigidbody komponentet findes, og der tilføjes force til det//
        rb = gameObject.GetComponent<Rigidbody2D>();
        spawnLocation = transform.position;

        rb.AddForce(transform.up * -1 * speed * Time.deltaTime, ForceMode2D.Impulse);
    }


    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, spawnLocation) > range)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponent<EnemyAttributes>().DealDamage(damage);
            Destroy(this.gameObject);
        }
    }

}
