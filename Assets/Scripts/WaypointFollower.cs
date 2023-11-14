using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    [SerializeField] private GameObject chainPrefab;
    private List<GameObject> chainLinks = new List<GameObject>();


    private void Start()
    {
        InitializeChains();
    }

    private void Update()
    {
        MoveToNextWaypoint();
    }

    private void InitializeChains()
    {
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            float distance = Vector3.Distance(waypoints[i].transform.position, waypoints[i + 1].transform.position);
            int numberOfChains = Mathf.RoundToInt(distance) + 2;

            for (int j = 0; j < numberOfChains; j++)
            {
                float t = (float)(j + 1) / (numberOfChains + 1);
                Vector3 chainPosition = Vector3.Lerp(waypoints[i].transform.position, waypoints[i + 1].transform.position, t);
                GameObject chainLink = Instantiate(chainPrefab, chainPosition, Quaternion.identity);
                chainLinks.Add(chainLink);
            }
        }
    }

    private void MoveToNextWaypoint()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
