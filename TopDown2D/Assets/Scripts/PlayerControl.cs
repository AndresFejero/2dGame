using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float speed;

    Rigidbody2D player;

    public LayerMask layerMask;

    void Start()
    {
        player = this.gameObject.GetComponent<Rigidbody2D>();
        speed = player.GetComponent<PlayerAttributes>().movementSpeed;
    }

    void FixedUpdate()
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));

        RaycastHit2D hitL = Physics2D.Raycast(player.transform.position, Vector2.left, 0.5f, layerMask);
        RaycastHit2D hitR = Physics2D.Raycast(player.transform.position, Vector2.right, 0.5f, layerMask);
        RaycastHit2D hitUp = Physics2D.Raycast(player.transform.position, Vector2.up, 0.5f, layerMask);
        RaycastHit2D hitDown = Physics2D.Raycast(player.transform.position, Vector2.down, 0.5f, layerMask);

        if (Input.GetKey(KeyCode.W) && player.velocity.y > -speed && hitUp.collider == null)
        {
            player.AddForce(Vector2.up * speed);
            this.SendMessage("objectOrder");
        }
        if (!Input.GetKey(KeyCode.W) && player.velocity.y < 0 || hitUp.collider != null)
        {
            Vector3 vel = player.velocity;
            vel.y = 0f;
            player.velocity = vel;
        }


        if (Input.GetKey(KeyCode.S) && player.velocity.y < speed && hitDown.collider == null)
        {
            player.AddForce(Vector2.down * speed);
            this.SendMessage("objectOrder");
        }
        if (!Input.GetKey(KeyCode.S) && player.velocity.y > 0 || hitDown.collider != null)
        {
            Vector3 vel = player.velocity;
            vel.y = 0f;
            player.velocity = vel;
        }


        if (Input.GetKey(KeyCode.A) && player.velocity.x < speed && hitL.collider == null)
        {
            player.AddForce(Vector2.left * speed);
            this.SendMessage("objectOrder");
        }
        if (!Input.GetKey(KeyCode.A) && player.velocity.x > 0 || hitL.collider != null)
        {
            Vector3 vel = player.velocity;
            vel.x = 0f;
            player.velocity = vel;
        }

        if (Input.GetKey(KeyCode.D) && player.velocity.x > -speed && hitR.collider == null)
        {
            player.AddForce(Vector2.right * speed);
            this.SendMessage("objectOrder");
        }
        if (!Input.GetKey(KeyCode.D) && player.velocity.x < 0 || hitR.collider != null)
        {
            Vector3 vel = player.velocity;
            vel.x = 0f;
            player.velocity = vel;
        }

    }

}
