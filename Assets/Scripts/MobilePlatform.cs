using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject[] waypoints;
    private float platformSpeed = 1;
    private int waypointsIndex = 0;
    private bool playerIsHere = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsHere)
        {
            MovePlatform(); 
        }
    }

    public void MovePlatform()
    {
        if(Vector3.Distance(transform.position, waypoints[waypointsIndex].transform.position) < 0.1f)
        {
            waypointsIndex++;

            if (waypointsIndex >= waypoints.Length)
            {
                waypointsIndex = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointsIndex].transform.position, platformSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsHere = true;
        }
    }

   
}
