using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING};
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    public int nextWave = 0;
    
    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCD = 1f;

    public SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {

                WaveCompleted();
                return;
            }
            else
            {
                return;
            }    
        }
        if (waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                Debug.Log("Spawning next wave!");
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }
    void WaveCompleted()
    {
        Debug.Log("wave completed!");
        state = SpawnState.COUNTING;
        
        if(nextWave+1>waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping...");
        }
        else
            nextWave++;
    }
    bool EnemyIsAlive()//gennemgår scenens objekter for om de har tagget enemy
    {
        searchCD -= Time.deltaTime;
        if (searchCD <= 0f)
        {
            searchCD = 1f;

            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Wpawning wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy1(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy1(Transform _enemy)
    {
        Debug.Log("spawning Enemy: " + _enemy.name);

        Instantiate(_enemy, transform.position, transform.rotation);
    }

}
