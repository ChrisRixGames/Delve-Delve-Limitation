using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(collision.gameObject.name);
            if (collision.gameObject.GetComponent<DisableAfterTime>() != null)
            {
                health.Hurt(2);
            }
            else
            {
                health.Hurt(1);
            }
        }
    }
}
