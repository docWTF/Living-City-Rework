using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float totalTime = 300f; // Total time in seconds for the game
    public Image timerBar; // The UI Image that represents the timer progress

    private float timeLeft;

    void Start()
    {
        timeLeft = totalTime;
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerBar();
        }
        else
        {
            // Optional: Call any functions you need when the timer runs out
            OnTimerEnd();
        }
    }

    void UpdateTimerBar()
    {
        if (timerBar != null)
        {
            timerBar.fillAmount = timeLeft / totalTime;
        }
    }

    private void OnTimerEnd()
    {
        // Disable player controls or trigger the end of the game
        // You can also enable the end level UI here if needed
        Debug.Log("Time's up!");
    }
}
