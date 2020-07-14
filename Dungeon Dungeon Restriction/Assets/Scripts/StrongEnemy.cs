using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemy : MonoBehaviour
{
    private Vector2 direction;
    public LayerMask terrainAndEnemyLayer;
    private float speed = 1.8f;
    private Rigidbody2D rb2d;
    private GameObject player;
    private Transform playerPos;
    private Transform currentPos;

    public GameObject thrustAttack;

    private bool changeDirection = true;
    private bool closeToPlayer = false;
    private bool attacking;

    private float attackTimer = 1.8f;
    private float restTimer = .8f;

    public DirectionSpriteController dsc;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform;
        currentPos = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        if (restTimer > 0)
        {
            restTimer -= Time.deltaTime;
            return;
        }
        if (!closeToPlayer && !attacking)
        {
            MoveTowardsPlayer();
        }
        else
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                Attack();
            }

        }
    }

    private void MoveTowardsPlayer()
    {
        float horizontalDistance = playerPos.position.x - currentPos.position.x;
        float verticalDistance = playerPos.position.y - currentPos.position.y;

        if (Mathf.Abs(horizontalDistance) < 1.2f && Mathf.Abs(verticalDistance) < 1.2f)
        {
            closeToPlayer = true;
            rb2d.velocity = Vector2.zero;
            return;
        }
        else
        {
            closeToPlayer = false;
            rb2d.velocity = direction * speed;
        }

        if (direction.x != 0 && (Mathf.Abs(horizontalDistance) < .3f || Mathf.Sign(direction.x) != Mathf.Sign(horizontalDistance)))
        {
            changeDirection = true;
            //return;
        }

        if (direction.y != 0 && (Mathf.Abs(verticalDistance) < .3f || Mathf.Sign(direction.y) != Mathf.Sign(verticalDistance)))
        {
            changeDirection = true;
            //return;
        }

        if (PathBlocked())
        {
            changeDirection = true;
        }

        if (changeDirection)
        {
            int horizontal = Random.Range(0, 2);
            changeDirection = false;
            
            if ((horizontal == 1 || Mathf.Abs(verticalDistance) < 1.2f))
            {
                
                direction = new Vector2(horizontalDistance / Mathf.Abs(horizontalDistance), 0);
            }
            else
            {
                direction = new Vector2(0, verticalDistance / Mathf.Abs(verticalDistance));
            }

            rb2d.velocity = direction * speed;
        }

        dsc.SetDirection(direction);
    }

    private void Attack()
    {
        thrustAttack.transform.position = (Vector2)this.transform.position + (direction * .6f);
        if (direction.x != 0)
        {
            thrustAttack.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            thrustAttack.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        //thrustAttack.transform.Rotate(transform.position, Mathf.Acos(Vector2.Dot(thrustAttack.transform.up, direction)/(thrustAttack.transform.up.magnitude * direction.magnitude)));
        thrustAttack.SetActive(true);

        attackTimer = 1.8f;
        attacking = false;
        closeToPlayer = false;
        restTimer = .8f;
    }

    private bool PathBlocked()
    {
        RaycastHit2D[] hits = new RaycastHit2D[3];
        int num = Physics2D.RaycastNonAlloc((Vector2)transform.position + direction * .65f, direction, hits, 0.05f, terrainAndEnemyLayer);
        return num > 0;
    }
}
