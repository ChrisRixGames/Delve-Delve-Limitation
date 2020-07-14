using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    private Vector2 direction;
    public GameObject bullet;
    public LayerMask terrainAndEnemyLayer;
    private float speed = 1.8f;
    private Rigidbody2D rb2d;
    private float moveTimer = 0;
    private float delayTimer = 1f;
    private float shootTimer = 1.5f;

    public DirectionSpriteController dsc;
    // Start is called before the first frame update
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer -= Time.deltaTime;
        shootTimer -= Time.deltaTime;

        if (moveTimer <= 0 || PathBlocked())
        {
            rb2d.velocity = Vector2.zero;
            delayTimer -= Time.deltaTime;
            if (delayTimer <= 0)
            {
                NewDirection();
            }
        }

        if (shootTimer <= 0)
        {
            Shoot();
        }
    }

    private void NewDirection()
    {
        int horizontal = Random.Range(0, 2);
        int hm=0;
        int vm=0;
        if (horizontal == 1)
        {
            hm = Random.Range(0, 2);
            hm = hm == 0 ? -1 : 1;
        }
        else
        {
            vm = Random.Range(0, 2);
            vm = vm == 0 ? -1 : 1;
        }
        direction = new Vector2(hm, vm);

        rb2d.velocity = direction * speed;

        moveTimer = Random.Range(.5f, 3f);
        delayTimer = Random.Range(.5f, 1.2f);

        dsc.SetDirection(direction);
    }

    private bool PathBlocked()
    {
        RaycastHit2D[] hits = new RaycastHit2D[3];
        int num = Physics2D.RaycastNonAlloc((Vector2)transform.position + direction*.65f, direction, hits, 0.05f, terrainAndEnemyLayer);
        return num > 0;
    }

    private void Shoot()
    {
        Bullet b = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>();
        b.SetPlayer(false);
        b.SetVelocity(direction * 5f);
        shootTimer = Random.Range(.8f, 2.8f);
    }
}
