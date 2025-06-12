using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private int score = 0;
    private int time = 150;
    private int enemies = 20;
    private int escapedEnemy = 0;

    [SerializeField]
    private int points = 50;

    private bool _isGameEnded = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(TimeRoutine());
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame && _isGameEnded)
        {
            RestartGame();
        }
    }

    public void AddScore()
    {
        score += points;
        UpdateEnemyCount();
        UiManager.Instance.UpdateScore(score);
    }
    private void UpdateEnemyCount()
    {
        escapedEnemy++;
        if (escapedEnemy >= 10)
        {
            _isGameEnded = true;
            UiManager.Instance.ShowTryAgain();
        }
    }

    public void EnemyReachedEnd()
    {
        enemies--;
        if (enemies <= 0)
        {
            _isGameEnded = true;
            UiManager.Instance.ShowYouWon();
        }
        else
        {
            UiManager.Instance.UpdateEnemiesRemaining(enemies);
        }
    }
    void RestartGame()
    {
        score = 0;
        time = 150;
        enemies = 20;
        UiManager.Instance.UpdateScore(score);
        UiManager.Instance.UpdateEnemiesRemaining(enemies);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator TimeRoutine ()
    {
        while (time > 0 && !_isGameEnded)
        {
            yield return new WaitForSeconds(1f);
            time--;
        }
        if (time <= 0 && !_isGameEnded && enemies > 10)
        {
            _isGameEnded = true;
            UiManager.Instance.ShowTryAgain();
        }
        else if (enemies <= 10 && !_isGameEnded)
        {
            _isGameEnded = true;
            UiManager.Instance.ShowYouWon();
        }
    }

}
