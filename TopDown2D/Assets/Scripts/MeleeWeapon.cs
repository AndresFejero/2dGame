using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public Transform player;

    public int damage;
    public float coolDown;
    bool isOnCd;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnCd && this.transform.parent == player.transform)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                StartCoroutine(SwingCapMethod(coolDown));
            }
        }
    }

    IEnumerator SwingCapMethod(float coolDown)
    {
        isOnCd = true;
        yield return new WaitForSecondsRealtime(0.1f);
        isOnCd = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponent<EnemyAttributes>().DealDamage(damage);
        }
    }
}
