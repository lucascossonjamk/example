using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject m_player;
    public Projectile m_projectile;
    public Transform m_projectileContainer;
    public Transform m_shootPosition;

    private int m_health;
    private LevelController m_levelController;
    private bool m_gameOver = false;
    private int m_projectileAmount = 30;
    private Stack<Projectile> m_inactiveProjectiles;
    private List<Projectile> m_activeProjectiles;

    public void InitPlayer(LevelController gameOver)
    {
        m_levelController = gameOver;
        InitProjectiles();
    }

    public void ResetProjectile(Projectile projectile)
    {
        m_activeProjectiles.Remove(projectile);
        m_inactiveProjectiles.Push(projectile);
        projectile.gameObject.SetActive(false);
    }

    private void InitProjectiles()
    {
        m_inactiveProjectiles = new Stack<Projectile>();
        m_activeProjectiles = new List<Projectile>();
        for (int i = 0; i < m_projectileAmount; i++) 
        {
            Projectile proj = Instantiate<Projectile>(m_projectile);
            proj.Init(this);
            proj.transform.parent = m_projectileContainer;
            proj.gameObject.SetActive(false);
            m_inactiveProjectiles.Push(proj);
        }
    }

    private void Update()
    {
        if (m_gameOver) 
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 50f))
        {
            Vector3 rotation = Quaternion.LookRotation(hit.point).eulerAngles;
            rotation.x = 0f;
            transform.rotation = Quaternion.Euler(rotation);
        }
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if(m_inactiveProjectiles.Count == 0) 
            {
                return;
            }
            Projectile proj = m_inactiveProjectiles.Pop();
            m_activeProjectiles.Add(proj);
            proj.SetForAttack(m_shootPosition);
        }
    }

    public void ApplyDamage(int damage)
    {
        m_health -= damage;
        if (m_health <= 0) 
        {
            m_player.SetActive(false);
            m_gameOver = true;
            m_levelController.GameOver();
        }
    }
}
