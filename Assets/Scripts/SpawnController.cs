using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject m_enemyPrefab;
    public List<Transform> m_spawns;
    public Transform m_enemyContainer;
    public Transform m_player;

    private float m_spawnTimer = 2f;
    private float m_decreaseTimer = 0.01f;

    private float m_timer = 0f;

    private List<EnemyBehaviour> m_activeEnemies;
    private List<EnemyBehaviour> m_inactiveEnemies;
    private int m_enemyAmount = 20;

    public Transform Target => m_player;
    private bool m_gameOver = false;

    private void Update()
    {
        if (m_gameOver) 
        {
            return;
        }
        m_timer += Time.deltaTime;
        if(m_timer > m_spawnTimer)
        {
            SpawnEnemy();
            m_timer = 0f;
            m_spawnTimer -= m_decreaseTimer;
            if(m_spawnTimer < 0f) 
            {
                m_spawnTimer = 0f;
            }
        }
    }

    public void InitEnemies() 
    {
        m_activeEnemies = new List<EnemyBehaviour>();
        m_inactiveEnemies = new List<EnemyBehaviour>();

        for (int i = 0; i < m_enemyAmount; i++)
        {
            CreateEnemy();
        }
    }

    private void SpawnEnemy() 
    {
        if (m_inactiveEnemies.Count == 0) 
        {
            CreateEnemy();
        }
        EnemyBehaviour enemy = m_inactiveEnemies[0];
        m_inactiveEnemies.RemoveAt(0);
        m_activeEnemies.Add(enemy);
        int index = Random.Range(0, m_spawns.Count);
        Transform sp = m_spawns[index];
        Vector3 position = sp.position;
        enemy.SetEnemyForAttack(position);
    }

    private void CreateEnemy() 
    {
        GameObject obj = Instantiate(m_enemyPrefab);
        obj.transform.parent = m_enemyContainer;
        EnemyBehaviour enemyBehaviour = obj.GetComponent<EnemyBehaviour>();
        enemyBehaviour.InitEnemy(this);
        m_inactiveEnemies.Add(enemyBehaviour);
    }

    public void ResetDeadEnemy(EnemyBehaviour deadEnemy) 
    {
        m_activeEnemies.Remove(deadEnemy);
        m_inactiveEnemies.Add(deadEnemy);
    }

    public void StopSpawn()
    {
        m_gameOver = true;
        foreach(EnemyBehaviour activeEnemy in m_activeEnemies) 
        {    
            activeEnemy.Stop();
        }
    }
}
