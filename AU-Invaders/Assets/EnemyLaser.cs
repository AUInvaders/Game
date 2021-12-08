using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    public FireRate _fireRate;
    public Fire _fire;
    public EnemyLaser()
    {
        _fireRate = new FireRate();
        _fire = new Fire(this);
    }

    public Transform FirePoint;
    public GameObject EnemyBulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    { 
        Shoot();
    }

    // Update is called once per frame
    void Shoot()
    {
        if (_fireRate.canShoot1 == true)
        {            
            _fireRate.canShoot1 = false;
            StartCoroutine(_fire.ShootDelay1());

            Instantiate(EnemyBulletPrefab, FirePoint.position, FirePoint.rotation);
        }
    }
}

public class Fire
{
    private EnemyLaser _enemyLaser;
    public Fire(EnemyLaser enemyLaser)
    {
        _enemyLaser = enemyLaser;
    }
    public IEnumerator ShootDelay1()
    {
        yield return new WaitForSeconds(_enemyLaser._fireRate.delayInSeconds1);
        _enemyLaser._fireRate.canShoot1 = true;
        
    }
}

public class FireRate
{
    public FireRate()
    {
        canShoot1 = true;
        delayInSeconds1 = 2;
    }
    public bool canShoot1;
    public float delayInSeconds1;
}
