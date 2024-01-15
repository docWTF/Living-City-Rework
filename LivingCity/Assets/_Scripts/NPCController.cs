using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour
{
    public Transform player;
    public float maxSpeed = 3.0f;
    public float stoppingDistance = 2.0f;
    public float rotationSpeed = 5.0f;
    private Rigidbody npcRigidbody;
    private NPCTimerScript npcTimerScript; // Reference to the NPCTimerScript
    private bool shouldFollowPlayer = false;
    private bool hasReachedDestination = false;
    public float disappearDelay = 5.0f;

    void Start()
    {
        npcRigidbody = GetComponent<Rigidbody>();
        npcTimerScript = GetComponent<NPCTimerScript>(); // Get the NPCTimerScript component
    }

    public void StartFollowingPlayer()
    {
        if (!hasReachedDestination)
        {
            shouldFollowPlayer = true;
            npcTimerScript.StopTimer(); // Stop the timer when NPC starts following the player
        }
    }

    public void StopFollowingPlayer()
    {
        shouldFollowPlayer = false;
    }

    void FixedUpdate()
    {
        if (shouldFollowPlayer)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        float distanceToPlayer = Vector3.Distance(npcRigidbody.position, player.position);

        if (distanceToPlayer > stoppingDistance)
        {
            Vector3 directionToPlayer = (player.position - npcRigidbody.position).normalized;
            float speed = maxSpeed * Mathf.Clamp01((distanceToPlayer - stoppingDistance) / stoppingDistance);
            Vector3 newPosition = npcRigidbody.position + directionToPlayer * speed * Time.fixedDeltaTime;
            npcRigidbody.MovePosition(newPosition);

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            npcRigidbody.MoveRotation(Quaternion.Slerp(npcRigidbody.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destination"))
        {
            Debug.Log("Thanks, bro");
            hasReachedDestination = true;
            StopFollowingPlayer();
            StartCoroutine(DisappearAfterDelay(disappearDelay));
        }
        else if (other.CompareTag("Player"))
        {
            StartFollowingPlayer(); // Call when the player enters the trigger collider
        }
    }

    private IEnumerator DisappearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false); // Deactivate the NPC GameObject
    }
}
