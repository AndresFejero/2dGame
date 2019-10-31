using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    public bool leftHit;
    public bool rightHit;
    public bool topHit;
    public bool botHit;
    public bool multipleBordersHit;

    // Start is called before the first frame update
    void Start()
    {
        leftHit = false;
        rightHit = false;
        botHit = false;
        topHit = false;
    }
        
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "BorderLeft")
        {
            leftHit = true;   
            
            if (topHit || botHit)
            {
                multipleBordersHit = true;
            }
        }
        if (collision.transform.tag == "BorderRight")
        {
            rightHit = true;

            if (topHit || botHit)
            {
                multipleBordersHit = true;
            }
        }

        if (collision.transform.tag == "BorderTop")
        {
            topHit = true;

            if (rightHit ||leftHit)
            {
                multipleBordersHit = true;
            }
        }
        if (collision.transform.tag == "BorderBottom")
        {
            botHit = true;

            if (rightHit || leftHit)
            {
                multipleBordersHit = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "BorderLeft")
        {
            leftHit = false;
            multipleBordersHit = false;

        }
        if (collision.transform.tag == "BorderRight")
        {
            rightHit = false;
            multipleBordersHit = false;
        }

        if (collision.transform.tag == "BorderTop")
        {
            topHit = false;
            multipleBordersHit = false;
        }
        if (collision.transform.tag == "BorderBottom")
        {
            botHit = false;
            multipleBordersHit = false;

        }
    }
}
