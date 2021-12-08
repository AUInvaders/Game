using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health = 100;

    private bool dead = false;
    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (dead == false)
        {
            ScoreManager.Instance.AddPoint(5);
            //Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(transform.parent.gameObject);
        }
        dead = true;
        
    }

}
