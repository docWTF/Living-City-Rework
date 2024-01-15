using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnSchedule
    {
        public GameObject npc; // The NPC GameObject to activate
        public float spawnTime; // The time after which this NPC should be activated
    }

    public List<SpawnSchedule> spawnSchedules; // List of NPCs and their spawn times
    private float elapsedTime = 0f; // Tracks the elapsed game time

    void Update()
    {
        elapsedTime += Time.deltaTime; // Increment the game time

        // Check the spawn schedules to see if it's time to activate an NPC
        foreach (var schedule in spawnSchedules.ToArray()) // ToArray() to avoid concurrent modification
        {
            if (elapsedTime >= schedule.spawnTime && schedule.npc != null && !schedule.npc.activeSelf)
            {
                // Activate the NPC
                schedule.npc.SetActive(true);
                // Remove the schedule to avoid reactivating the same NPC
                spawnSchedules.Remove(schedule);
            }
        }
    }
}
