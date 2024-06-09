using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int World { get; private set; }
    public int Stage { get; private set; }
    public int Lives { get; private set; }
    public int Coins { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }

    // Functionality for other levels is not implemented only base logic
    public void NextLevel()
    {
        LoadLevel(World, Stage + 1);
    }

    public void PlayerDeath(float delay)
    {
        Invoke(nameof(PlayerDeath), delay);
    }

    public void PlayerDeath()
    {
        Lives--;

        if (Lives > 0)
        {
            LoadLevel(World, Stage);
        }
        else
        {
            GameOver();
        }
    }

    public void AddCoin()
    {
        Coins++;

        if (Coins == 100)
        {
            AddLife();
            Coins = 0;
        }
    }

    public void AddLife()
    {
        Lives++;
    }

    private void NewGame()
    {
        Lives = 3;
        Coins = 0;

        LoadLevel(1, 1);
    }

    private void GameOver()
    {
        Invoke(nameof(NewGame), 3f);
    }

    private void LoadLevel(int world, int stage)
    {
        World = world;
        Stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }
}
