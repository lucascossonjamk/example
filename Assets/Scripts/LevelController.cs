using UnityEngine;

public class LevelController : MonoBehaviour
{
    public PlayerController m_playerController;
    public SpawnController m_spawnController;

    private void Start()
    {
        m_playerController.InitPlayer(this);
        m_spawnController.InitEnemies();
    }
    public void GameOver() 
    {
        m_spawnController.StopSpawn();
    }
}
