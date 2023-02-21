using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Weapon : MonoBehaviour
    {
        public float range;

        public float baseDamage;
        
        public float damage;

        public float cooldown;
        public float currentCooldown;

        public Transform attackPos;
        public LayerMask enemies;

        public GameObject player;
        
        public float followX, followY;
        private void Awake()
        {
            attackPos = player.transform;
            
        }

        // Use this for initialization
        void Start()
        {
            CalculateDamage();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void FollowPlayer(float x, float y)
        {
            Vector3 tempPos = new Vector3();
            var temp = attackPos.position;
            tempPos.x = temp.x + x;
            tempPos.y = temp.y + y;
            transform.position = tempPos;
        }

        abstract public void Attack();

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPos.position, range);
        }

        public int IndexOfClosestEnemy(Collider2D[] enemiesToDamage)
        {
            if (enemiesToDamage.Length <= 0)
            {
                return -1;
            }

            Enemy[] allEnemies = new Enemy[enemiesToDamage.Length];
            for(int i = 0; i < allEnemies.Length; i++)
            {
                allEnemies[i] = enemiesToDamage[i].GetComponent<Enemy>();
            }

            int index = 0;
            float diff = (attackPos.position - allEnemies[0].transform.position).magnitude;
            for(int i = 1; i < allEnemies.Length; i++)
            {
                float currentDiff = (attackPos.position - allEnemies[i].transform.position).magnitude;
                if ( currentDiff < diff)
                {
                    diff = currentDiff;
                    index = i;
                }
            }
            return index;
        }

        public void CalculateDamage()
        {
            damage = baseDamage + player.GetComponent<Player>().Damage;
        }
    }
}