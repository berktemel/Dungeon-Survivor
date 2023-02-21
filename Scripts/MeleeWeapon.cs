using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private float speed;

    [SerializeField] private float distance;

    private bool attacking;
    private Vector3 diff;
    private float distanceTraveled;

    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
        distanceTraveled = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void LateUpdate()
    {
        if (!attacking)
        {
            //FollowPlayer(-0.1f, 0.05f);
            FollowPlayer(followX, followY);
        }
    }

    public override void Attack()
    {
        if (!attacking)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, range, enemies);
            bool enemyExists = enemiesToDamage.Length > 0;
            if (enemyExists)
            {
                if (currentCooldown <= 0)
                {
                    attacking = true;
                    Vector3 difference = enemiesToDamage[0].transform.position - transform.position;
                    float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90;
                    transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
                    diff = difference;
                    currentCooldown = cooldown;
                }
                else
                {
                    currentCooldown -= Time.deltaTime;
                }
            }
        }
        else
        {
            if (distanceTraveled < distance)
            {
                PierceEnemy(diff);
            }
            else
            {
                ReturnToCharacter();
                if (Vector3.Distance(transform.position, attackPos.position) <= 0.15f)
                {
                    attacking = false;
                    distanceTraveled = 0f;
                }
            }
        }
    }

    private void PierceEnemy(Vector3 difference)
    {
        Vector3 pre = transform.position;
        transform.position += speed * Time.deltaTime * difference;
        distanceTraveled += Vector3.Distance(pre, transform.position);
    }

    private void ReturnToCharacter()
    {
        Vector3 charDiff = attackPos.position - transform.position;
        transform.position += speed * Time.deltaTime * charDiff;
    }

}