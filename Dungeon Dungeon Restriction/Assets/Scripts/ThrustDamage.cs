using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustDamage : MonoBehaviour
{
    private bool player = true;

    private void Awake()
    {
        if (this.gameObject.CompareTag("Enemy"))
        {
            player = false;
        }
        else
        {
            player = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player)
            {
                return;
            }
            else
            {
                collision.gameObject.GetComponent<Health>().Hurt(2);
            }

        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if (player)
            {
                collision.gameObject.GetComponent<Health>().Hurt(5);
            }
            else
            {
                return;
            }
        }
    }
}
