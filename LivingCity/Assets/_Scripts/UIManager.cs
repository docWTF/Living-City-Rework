using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TMP_Text savedCountText; // Assign this via inspector in the Canvas GameObject
    private int savedCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: if you want the UIManager to persist across scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementSavedCount()
    {
        savedCount++;
        savedCountText.text = $"People you have take across the street: {savedCount}";
    }
}
