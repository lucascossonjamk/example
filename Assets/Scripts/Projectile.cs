using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody m_rigidbody;
    private float m_movementSpeed = 10f;
    private int m_damage = 5;
    private PlayerController m_playerController;
    private float m_lifecycle = 5f;
    private float m_timer = 0f;

    void FixedUpdate()
    {
        m_timer += Time.deltaTime;
        if(m_timer > m_lifecycle) 
        {
            m_playerController.ResetProjectile(this);
            return;
        }
        m_rigidbody.MovePosition(transform.position + transform.forward * m_movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().ApplyDamage(m_damage);
            m_playerController.ResetProjectile(this);
        }
    }
    public void Init(PlayerController playerController)
    {
        m_playerController = playerController;
    }

    public void SetForAttack(Transform m_shootPosition)
    {
        m_timer = 0f;
        gameObject.SetActive(true);
        transform.position = m_playerController.m_shootPosition.position;
        transform.rotation = m_shootPosition.rotation;
    }
}
