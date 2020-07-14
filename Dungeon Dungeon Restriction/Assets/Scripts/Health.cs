using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 6;
    private int currentHealth;
    private bool player = false;

    private void Awake()
    {
        if (GetComponent<PlayerController>() != null)
        {
            player = true;
        }
        currentHealth = maxHealth;
    }

    public void Hurt(int damage)
    {
        if (player)
        {
            if (GetComponent<PlayerController>().ShieldUp())
            {
                return;
            }
        }
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            if (player)
            {
                GetComponent<PlayerController>().GameOver();
            }
            Destroy(this.gameObject);
        }
    }
}
