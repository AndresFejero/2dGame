using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCollision : MonoBehaviour
{
    public float move;


    // Start is called before the first frame update
    void Start()
    {
        move = 500  ;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {        
        if (this.transform.tag == "BorderTop")
        {
            if (col.transform.CompareTag("Tile"))
            {
                col.transform.position = new Vector3(col.transform.position.x, col.transform.position.y - move, 0);

                col.transform.SendMessage("ObjSpawn");
            }
        }


        if (this.transform.tag == "BorderLeft")
        {
            if (col.transform.CompareTag("Tile"))
            {
                col.transform.position = new Vector3(col.transform.position.x + move, col.transform.position.y, 0);

                col.transform.SendMessage("ObjSpawn");
            }
        }


        if (this.transform.tag == "BorderRight")
        {

            if (col.transform.CompareTag("Tile"))
            {
                col.transform.position = new Vector3(col.transform.position.x - move, col.transform.position.y, 0);

                col.transform.SendMessage("ObjSpawn");
            }
        }


        if (this.transform.tag == "BorderBottom")
        {
            if (col.transform.CompareTag("Tile"))
            {
                col.transform.position = new Vector3(col.transform.position.x, col.transform.position.y + move, 0);

                col.transform.SendMessage("ObjSpawn");
            }
        }

    }



}
