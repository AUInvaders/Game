using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBullet : MonoBehaviour
{


    public static float speed = 300f;
    public int damage = 40;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitinfo)
    {
        PlayerShip player = hitinfo.GetComponent<PlayerShip>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        if (hitinfo.gameObject.name != "BulletEnemy Variant(Clone)"&& hitinfo.gameObject.name != "Enemy_1")
            Destroy(gameObject);
    }
    
}
