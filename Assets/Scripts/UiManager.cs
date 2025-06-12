using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }

    [SerializeField]
    private Text scorePoints;
    [SerializeField]
    private Text enemiesRemaining;
    [SerializeField]
    private Text timeRemaining;
    [SerializeField]
    private GameObject wonText;
    [SerializeField]
    private GameObject tryAgainText;
    [SerializeField]
    private GameObject restartText;


    private int time = 150;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        StartCoroutine(timeRemainingRoutine());
    }

    public void UpdateScore(int newScore) {
        scorePoints.text = newScore.ToString();
    }

    public void UpdateEnemiesRemaining(int remaining) {
        enemiesRemaining.text = remaining.ToString();
    }
    
    public void ShowYouWon() {
        wonText.SetActive(true);
        restartText.SetActive(true);
    }

    public void ShowTryAgain() {
        tryAgainText.SetActive(true);
        restartText.SetActive(true);
    }

    IEnumerator timeRemainingRoutine ()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1f);
            time--;
            timeRemaining.text = time.ToString();
        }
                        
    }
}
