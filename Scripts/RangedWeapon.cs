using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public GameObject projectile;
    public Transform shotPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void LateUpdate()
    {
        FollowPlayer(followX, followY);
    }

    public override void Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, range, enemies);
        bool enemyExists = enemiesToDamage.Length > 0;
        if (enemyExists)
        {
            Vector3 difference = enemiesToDamage[0].transform.position - attackPos.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            
            if (currentCooldown <= 0)
            {
                Quaternion projectileRotation = Quaternion.Euler(0f, 0f, rotZ - 90);
                Projectile proj = projectile.GetComponent<Projectile>();
                proj.enemy = enemiesToDamage[0].transform;
                
                proj.damage = damage;
                Instantiate(proj, shotPoint.position, projectileRotation);
                
                currentCooldown = cooldown;
            }
            else
            {
                currentCooldown -= Time.deltaTime;
            }
        }        
    }

}
