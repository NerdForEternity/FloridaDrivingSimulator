using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NavMeshPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] Lwaypoints;
    [SerializeField] private Transform[] Rwaypoints;
    [SerializeField] private float waypointThreshold = 0.5f;

    private NavMeshAgent agent;
    private Transform[] waypoints;
    private int currentIndex;
    private Queue<int> recentIndices = new Queue<int>();

    private const int historyLimit = 3;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        bool useLeft = Random.Range(0, 2) == 1;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 2f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
        else
        {
            Debug.LogWarning("No NavMesh found near agent. Agent might not move.");
        }

        waypoints = useLeft ? Lwaypoints : Rwaypoints;

        Debug.Log($"Patrol route: {(useLeft ? "Left" : "Right")} ({waypoints.Length} points)");

        currentIndex = FindClosestWaypointIndex();
        if (currentIndex >= 0)
        {
            agent.SetDestination(waypoints[currentIndex].position);
            AddToHistory(currentIndex);
        }
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0 || agent.pathPending)
            return;

        if (agent.remainingDistance <= waypointThreshold)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;

            // Avoid repeating recent waypoints
            int attempts = 0;
            while (recentIndices.Contains(currentIndex) && attempts < waypoints.Length)
            {
                currentIndex = (currentIndex + 1) % waypoints.Length;
                attempts++;
            }

            agent.SetDestination(waypoints[currentIndex].position);
            AddToHistory(currentIndex);
        }
    }

    private void AddToHistory(int index)
    {
        recentIndices.Enqueue(index);
        if (recentIndices.Count > historyLimit)
            recentIndices.Dequeue();
    }

    private int FindClosestWaypointIndex()
    {
        int closestIndex = -1;
        float minDist = float.MaxValue;

        for (int i = 0; i < waypoints.Length; i++)
        {
            if (recentIndices.Contains(i))
                continue;

            float dist = Vector3.Distance(transform.position, waypoints[i].position);
            if (dist < minDist)
            {
                minDist = dist;
                closestIndex = i;
            }
        }

        return closestIndex;
    }
}