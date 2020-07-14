using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float horizontalMovement;
    private float verticalMovement;
    private Vector2 direction;

    public float speed;
    public ControlLock controlLock;
    public GameObject shield;
    public GameObject attack;
    public GameObject thrustAttack;
    public GameObject bullet;

    private bool attacking;
    private bool shooting;
    private bool shielding;

    private float attackCharge = 0;
    public float chargeSpeed;
    public float fullyCharged;

    private float shootCooldown = .3f;
    private float shootTimer = 0;

    public GameObject gameoverPanel;

    public DirectionSpriteController dsc;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void GameOver()
    {
        gameoverPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!shooting && ! shielding)
            {
                attacking = true;
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (!attacking && !shielding)
            {
                shooting = true;
            }
        }

        if (Input.GetButtonDown("Fire3"))
        {
            if (!shooting && !attacking)
            {
                shielding = true;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            attacking = false;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            shooting = false;
        }

        if (Input.GetButtonUp("Fire3"))
        {
            shielding = false;
        }

        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        int nextLock = 0;
        do
        {
            LockControls(controlLock.GetNextLocks(ref nextLock));
        } while (nextLock != 0);
        LockControls(controlLock.GetDefaultLocks());

        if (verticalMovement != 0)
        {
            direction = new Vector2(0, verticalMovement).normalized;
        }
        else if(horizontalMovement != 0)
        { 
            direction = new Vector2(horizontalMovement, 0).normalized;
        }
        dsc.SetDirection(direction);

        Attack();

        Shield();

        Shoot();
    }

    private void Attack()
    {
        if (attacking)
        {
            attackCharge += Time.deltaTime * chargeSpeed;
            if (attackCharge >= fullyCharged)
            {
                attacking = false;
            }
        }
        if (!attacking)
        {
            if (attackCharge >= fullyCharged)
            {
                rb2D.AddForce(direction * 10f, ForceMode2D.Impulse);
                thrustAttack.transform.position = (Vector2)this.transform.position + (direction * .75f);
                if (direction.x != 0)
                {
                    thrustAttack.transform.rotation = Quaternion.Euler(0,0,90);
                }
                else
                {
                    thrustAttack.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                //thrustAttack.transform.Rotate(transform.position, Mathf.Acos(Vector2.Dot(thrustAttack.transform.up, direction)/(thrustAttack.transform.up.magnitude * direction.magnitude)));
                thrustAttack.SetActive(true);
                attackCharge = 0;
            }
            else if (attackCharge != 0)
            {
                attack.transform.position = (Vector2)this.transform.position + (direction * .45f);
                attack.SetActive(true);
                attackCharge = 0;
            }
        }
    }

    private void Shield()
    {
        if (shielding && !shield.activeSelf)
        {
            shield.SetActive(true);
        }
        else if(!shielding && shield.activeSelf)
        {
            shield.SetActive(false);
        }
    }

    public bool ShieldUp()
    {
        return shield.activeSelf;
    }

    private void Shoot()
    {
        if (shooting)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                Bullet b = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>();
                b.SetPlayer(true);
                b.SetVelocity(direction * 8f);
                shootTimer += shootCooldown;
            }
        }
        if (!shooting)
        {
            shootTimer = 0;
        }
    }

    private void FixedUpdate()
    {
        //rb2D.velocity = Vector2.zero;

        


        rb2D.AddForce( new Vector2(horizontalMovement * speed, verticalMovement * speed));
    }

    private void LockControls(ControlLock.Locks locks)
    {
        switch (locks.control)
        {
            case ControlLock.LockedControl.None:
                break;
            case ControlLock.LockedControl.Up:
                LockUp(locks.locked);
                break;
            case ControlLock.LockedControl.Left:
                LockLeft(locks.locked);
                break;
            case ControlLock.LockedControl.Right:
                LockRight(locks.locked);
                break;
            case ControlLock.LockedControl.Down:
                LockDown(locks.locked);
                break;
            case ControlLock.LockedControl.Melee:
                LockAttack(locks.locked);
                break;
            case ControlLock.LockedControl.Ranged:
                LockShoot(locks.locked);
                break;
            case ControlLock.LockedControl.Shield:
                LockShield(locks.locked);
                break;
            default:
                break;
        }
    }

    private void LockLeft(bool locked)
    {
        if (horizontalMovement < -Vector2.kEpsilon)
        {
            if (locked)
            {
                horizontalMovement = 0;
            }
            
        }
        else if (!locked)
        {
            horizontalMovement = -1;
        }
    }

    private void LockRight(bool locked)
    {
        if (horizontalMovement > Vector2.kEpsilon)
        {
            if (locked)
            {
                horizontalMovement = 0;
            }

        }
        else if (!locked)
        {
            horizontalMovement = 1;
        }
    }

    private void LockUp(bool locked)
    {
        if (verticalMovement > Vector2.kEpsilon)
        {
            if (locked)
            {
                verticalMovement = 0;
            }

        }
        else if (!locked)
        {
            verticalMovement = 1;
        }
    }

    private void LockDown(bool locked)
    {
        if (verticalMovement < -Vector2.kEpsilon)
        {
            if (locked)
            {
                verticalMovement = 0;
            }

        }
        else if (!locked)
        {
            verticalMovement = -1;
        }
    }

    private void LockAttack(bool locked)
    {
        if (attacking)
        {
            if (locked)
            {
                attacking = false;
            }

        }
        else if (!locked)
        {
            attacking = true;
        }
    }

    private void LockShoot(bool locked)
    {
        if (shooting)
        {
            if (locked)
            {
                shooting = false;
            }

        }
        else if (!locked)
        {
            shooting = true;
        }
    }

    private void LockShield(bool locked)
    {
        if (shielding)
        {
            if (locked)
            {
                shielding = false;
            }

        }
        else if (!locked)
        {
            shielding = true;
        }
    }

}
