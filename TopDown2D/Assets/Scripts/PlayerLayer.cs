using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerLayer : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Update()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y) * -1 + 1;
    }
}
