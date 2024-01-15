using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCTimerScript : MonoBehaviour
{
    public float countdownTime = 30f; // Total countdown time in seconds
    public TMP_Text timerText; // Assign the TMP_Text component that will display the countdown
    public Transform canvasTransform; // Transform of the World Space Canvas that contains the TMP_Text
    public Image inGameTimerFillImage; // The in-game UI Image component
    public Image mapTimerFillImage; // The map UI Image component
    public Rigidbody npcRigidbody; // Reference to the NPC's Rigidbody

    private NPCController npcController; // Reference to the NPCController script
    private float currentTime;
    private bool timerActive = true;
    private Transform mainCameraTransform;

    void Awake()
    {
        // Get the NPCController component from the same GameObject
        npcController = GetComponent<NPCController>();
    }

    void Start()
    {
        currentTime = countdownTime;
        mainCameraTransform = Camera.main.transform;
        UpdateTimerFill(1f); // Initialize the fill images to full
    }

    void Update()
    {
        if (timerActive && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
            UpdateTimerFill(currentTime / countdownTime);
            FaceCamera();
        }
        else if (currentTime <= 0 && timerActive)
        {
            OnTimerEnd();
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(currentTime).ToString();
        }
    }

    void UpdateTimerFill(float fillAmount)
    {
        if (inGameTimerFillImage != null)
        {
            inGameTimerFillImage.fillAmount = fillAmount;
        }

        if (mapTimerFillImage != null)
        {
            mapTimerFillImage.fillAmount = fillAmount;
        }
    }

    public void StopTimer()
    {
        timerActive = false;
        if (timerText != null)
        {
            timerText.enabled = false;
        }
    }

    private void OnTimerEnd()
    {
        timerActive = false;
        Debug.Log("Timer ended for " + gameObject.name);
        npcRigidbody.isKinematic = true; // Make the Rigidbody kinematic
        npcController.enabled = false; // Disable the NPCController script to stop following player
        if (inGameTimerFillImage != null)
            inGameTimerFillImage.transform.parent.gameObject.SetActive(false); // Disable the in-game UI canvas
        if (mapTimerFillImage != null)
            mapTimerFillImage.transform.parent.gameObject.SetActive(false); // Disable the map UI canvas
    }

    private void FaceCamera()
    {
        if (mainCameraTransform != null && canvasTransform != null)
        {
            canvasTransform.LookAt(canvasTransform.position + mainCameraTransform.rotation * Vector3.forward,
                                   mainCameraTransform.rotation * Vector3.up);
        }
    }
}
