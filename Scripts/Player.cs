using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce;

    [SerializeField]
    private float maxHealth;
    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    private float currentHealth;
    public float CurrentHealth
    {
        set { currentHealth = value; }
        get { return currentHealth; }
    }

    [SerializeField]
    private float damage;
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float criticalChance;
    
    private float movementX;
    private float movementY;

    private const int MAX_WEAPONS = 6;
    public Weapon[] weapons = new Weapon[MAX_WEAPONS];
    [SerializeField]
    private int weaponNumber;

    private SpriteRenderer sr;

    private Animator anim;
    private const string RUN_ANIMATION = "Running";

    private readonly float[,] weaponPositions = new float[6,2]
    {
        {-0.1f, 0.05f}, {0.04f, 0.15f}, {0.1f, 0.06f}, {0.1f, -0.14f}, {0.04f, -0.24f}, {-0.01f, 0.16f}
    };

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        //weaponNumber = 2;
        InitWeapons();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        AnimatePlayer();
    }

    private void InitWeapons()
    {
        for (int i = 0; i < weaponNumber; i++)
        {
            //var weapon = Instantiate(weapons[i]);
            var weapon = weapons[i];
            weapon.player = gameObject;
            weapon.followX = weaponPositions[i, 0];
            weapon.followY = weaponPositions[i, 1];
            Instantiate(weapon);
        }
    }

    void PlayerMovement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");

        transform.position += moveForce * Time.deltaTime * new Vector3(movementX, movementY, 0);
    }

    void AnimatePlayer()
    {
        if(movementX != 0 || movementY != 0)
        {
            anim.SetBool(RUN_ANIMATION, true);
            sr.flipX = movementX < 0;
        } else
        {
            anim.SetBool(RUN_ANIMATION, false);
        }
    }

    public void TakeDamage(float value)
    {
        currentHealth -= value;
        Debug.Log(currentHealth);
        if(currentHealth <= 0)
        {
            //Destroy(gameObject);
        }
    }

    public void AddWeapon()
    {
        weaponNumber++;
    }
}
