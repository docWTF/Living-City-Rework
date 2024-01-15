using UnityEngine;

public class ChildColliderScript : MonoBehaviour
{
    private NPCController npcController;

    void Start()
    {
        // Make sure you're getting the NPCController component correctly
        npcController = GetComponentInParent<NPCController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Call the public method on the npcController
            npcController.StartFollowingPlayer();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Call the public method on the npcController
            npcController.StopFollowingPlayer();
        }
    }
}
