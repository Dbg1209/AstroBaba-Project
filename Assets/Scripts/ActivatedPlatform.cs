using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedPlatform : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    private float platformSpeed = 1;
    private int waypointsIndex = 0;
    private bool playerIsHere = false;
    [SerializeField]
    private GameObject roomBarril;
    private ObstacleDetection roomBarrilScript;
    void Start()
    {
        roomBarrilScript = roomBarril.GetComponent<ObstacleDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (roomBarrilScript.barrilCount == 3)
        {
            MovePlatform();
        }
    }

    public void MovePlatform()
    {
        if (Vector3.Distance(transform.position, waypoints[waypointsIndex].transform.position) < 0.1f)
        {
            waypointsIndex++;

            if (waypointsIndex >= waypoints.Length)
            {
                waypointsIndex = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointsIndex].transform.position, platformSpeed * Time.deltaTime);
    }
}
