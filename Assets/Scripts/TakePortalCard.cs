using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePortalCard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Model2") || other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

   
}
