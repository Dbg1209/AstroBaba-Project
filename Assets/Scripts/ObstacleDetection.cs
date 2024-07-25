using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetection : MonoBehaviour
{
    public int barrilCount = 0;
    private HashSet<GameObject> registerElements = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") && !registerElements.Contains(other.gameObject)) 
        {
            registerElements.Add(other.gameObject);
            barrilCount++;
            Debug.Log(barrilCount);
        }
    }
}
