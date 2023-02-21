using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public float damage;

    public float lifeTime;

    public Transform enemy;

    private Vector3 difference;

    //private float directionX, directionY;

    public Projectile(int damage)
    {
        this.damage = damage;
    }

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        CalculateMovementDirection();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += speed * Time.deltaTime * new Vector3(directionX, directionY, 0);
        transform.position += speed * Time.deltaTime * difference;
    }

    void CalculateMovementDirection()
    {
        if (enemy != null)
        {
            /*if (transform.position.x > enemy.position.x)
            {
                directionX = -1;
            }
            else if (transform.position.x < enemy.position.x)
            {
                directionX = 1;
            }
            if (transform.position.y > enemy.position.y)
            {
                directionY = -1;
            }
            else if (transform.position.y < enemy.position.y)
            {
                directionY = 1;
            }*/
            difference = enemy.position - transform.position;
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
