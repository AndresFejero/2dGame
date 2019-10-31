using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Optimization : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<SpriteRenderer>())
        {
            collision.transform.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<SpriteRenderer>())
        {
            collision.transform.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
