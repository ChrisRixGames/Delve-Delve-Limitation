using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 velocity;
    private bool player = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVelocity(Vector2 v)
    {
        velocity = v;
        GetComponent<Rigidbody2D>().AddForce(v ,ForceMode2D.Impulse);
    }

    public void SetPlayer(bool p)
    {
        player = p;

        GetComponent<SpriteRenderer>().color = p ? Color.green : Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player)
            {
                return;
            }
            else
            {
                collision.gameObject.GetComponent<Health>().Hurt(1);
            }

        }
        else if (collision.CompareTag("Enemy"))
        {
            if (player)
            {
                collision.gameObject.GetComponent<Health>().Hurt(1);
            }
            else
            {
                return;
            }
        }
        Destroy(this.gameObject);
    }
}
