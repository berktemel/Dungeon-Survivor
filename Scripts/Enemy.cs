using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float health;

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    [SerializeField]
    private float damage;

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    [SerializeField]
    private float moveForce;

    private GameObject player;
    private SpriteRenderer sr;

    public GameObject bloodEffect;

    public GameObject damageNumber;
    private TextMeshPro textMesh;
    private void Awake()
    {
        textMesh = damageNumber.GetComponent<TextMeshPro>();
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        DamagePopup.damageNumber = damageNumber;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardPlayer();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
        else
        {
            float critChance = player.GetComponent<Player>().criticalChance;
            float random = 100 * Random.value;
            bool isCriticalHit = random < critChance;
            if (collision.CompareTag("Projectile"))
            {
                if (isCriticalHit)
                {
                    TakeDamage(2 * collision.GetComponent<Projectile>().damage);
                }
                else
                {
                    TakeDamage(collision.GetComponent<Projectile>().damage);
                }
                
            } else if (collision.CompareTag("MeleeWeapon"))
            {
                if (isCriticalHit)
                {
                    TakeDamage(2 * collision.GetComponent<MeleeWeapon>().damage);
                }
                else
                {
                    TakeDamage(collision.GetComponent<MeleeWeapon>().damage);
                }
                
            }
            
        }
        
    }

    private void MoveTowardPlayer()
    {
        int movementX = 0;
        int movementY = 0;
        Transform playerPosition = player.transform;
        if(playerPosition.position.x < transform.position.x)
        {
            movementX = -1;
            sr.flipX = true;
        } else if(playerPosition.position.x > transform.position.x)
        {
            movementX = 1;
            sr.flipX = false;
        }
        if(playerPosition.position.y < transform.position.y)
        {
            movementY = -1;
        } else if(playerPosition.position.y > transform.position.y)
        {
            movementY = 1;
        }
        transform.position += moveForce * Time.deltaTime * new Vector3(movementX, movementY, 0);
    }

    public void TakeDamage(float amount)
    {
        Vector3 pos = transform.position;
        GameObject bloodCopy = Instantiate(bloodEffect, pos, Quaternion.identity);
        textMesh.SetText(amount.ToString());
        pos += new Vector3(0, 0.2f, 0);
        GameObject tempNumber = Instantiate(damageNumber, pos, Quaternion.identity);
        Destroy(tempNumber, 1f);
        //DamagePopup.Create(pos, amount);
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log(health);
        Destroy(bloodCopy, 0.5f);
    }
}
