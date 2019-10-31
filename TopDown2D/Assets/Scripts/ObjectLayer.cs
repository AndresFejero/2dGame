using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLayer : MonoBehaviour
{
    SpriteRenderer sprite;
    public Transform myTrans;
    SpriteRenderer objPos;

    private void Start()
    {     
        sprite = GetComponent<SpriteRenderer>();
        myTrans = null;
        foreach (Transform item in GetComponentsInChildren<Transform>())
        {
            if (item.name == "objectPosition")
            {
                myTrans = item;
            }
        }

        objPos = transform.GetChild(0).GetComponent<SpriteRenderer>();
        objPos.enabled = false;

        objectOrder();

    }


    public void objectOrder()
    {
        if (myTrans.position.y < 0)
        {
            sprite.sortingOrder = Mathf.RoundToInt(myTrans.position.y * 100f) * -1;
        }

        if (myTrans.position.y > 0)
        {
            sprite.sortingOrder = Mathf.RoundToInt(myTrans.position.y * 100f) * -1;
        }
    }

}
