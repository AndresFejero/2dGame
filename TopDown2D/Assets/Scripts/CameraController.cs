using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    CameraLock cameraLockScript;

    // Start is called before the first frame update
    void Start()
    {       
        player = GameObject.FindGameObjectWithTag("Player");

        cameraLockScript = player.transform.GetComponent<CameraLock>();
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (!cameraLockScript.topHit && !cameraLockScript.botHit && !cameraLockScript.rightHit && !cameraLockScript.leftHit)
        {
            transform.position = player.transform.position + offset;
        }

        if (cameraLockScript.topHit && !cameraLockScript.multipleBordersHit || cameraLockScript.botHit && !cameraLockScript.multipleBordersHit)
        {
            Vector3 pos = new Vector3(player.transform.position.x, transform.position.y, 0);
            transform.position = pos + offset;
        }

        if (cameraLockScript.leftHit && !cameraLockScript.multipleBordersHit || cameraLockScript.rightHit && !cameraLockScript.multipleBordersHit)
        {
            Vector3 pos = new Vector3(transform.position.x, player.transform.position.y, 0);
            transform.position = pos + offset;
        }

        if (cameraLockScript.multipleBordersHit)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            transform.position = pos + offset;
        }
    }
}
