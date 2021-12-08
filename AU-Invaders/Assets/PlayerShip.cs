using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShip : MonoBehaviour
{
    public static int Health = 100;

    private bool dead = false;
    public void TakeDamage(int Damage)
    {
        PlayerHealthManager.Instance.UpdateHealth(Damage);
        Health -= Damage;
        if (Health <= 0)
        {
            ScoreManager.Instance.New_Highscore();
            Die();
        }
    }

    public GameOverMenu GameOverMenu;
   

    void Die()
    {
        if (dead == false)
        {
            Destroy(gameObject);
        }
        dead = true;
        ScoreManager.Instance.SendHighscore();
        GameOverMenu.Setup();
    }
}

