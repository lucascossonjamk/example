using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private float m_speed = 1f;
    private int m_health;
    private int m_startHealth = 10;
    private int m_damageToPlayer = 1;

    private SpawnController m_spawnController;
    private Transform m_target;

    private bool m_gameOver;

    void Update()
    {
        if (m_gameOver) {  return; }

        transform.position = Vector3.MoveTowards(transform.position, m_target.position, m_speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, m_target.position) < 0.001f)
        {
            m_target.GetComponent<PlayerController>().ApplyDamage(m_damageToPlayer);
            m_spawnController.ResetDeadEnemy(this);
        }
    }

    public void InitEnemy(SpawnController spawnController) 
    {
        m_spawnController = spawnController;
        m_target = spawnController.Target;
        gameObject.SetActive(false);
    }

    public void SetEnemyForAttack(Vector3 position) 
    {
        m_health = m_startHealth;
        gameObject.SetActive(true);
        transform.position = position;
    }

    public void ApplyDamage(int damage) 
    {
        m_health -= damage;
        if(m_health <= 0) 
        {
            m_spawnController.ResetDeadEnemy(this);
            gameObject.SetActive(false);
        }
    }

    public void Stop() => m_gameOver = true;
}
