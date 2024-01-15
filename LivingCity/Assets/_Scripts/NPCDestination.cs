using UnityEngine;

public class NPCDestination : MonoBehaviour
{
    private NPCController npcController;

    void Start()
    {
        // Get the NPCController component from the parent NPC GameObject
        npcController = GetComponentInParent<NPCController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destination"))
        {
            // Notify the UIManager to increment the saved count
            UIManager.Instance.IncrementSavedCount();
            // Optional: Stop the NPC from following the player
            npcController.StopFollowingPlayer();
            // Deactivate the NPC GameObject
            gameObject.SetActive(false);
        }
    }
}
