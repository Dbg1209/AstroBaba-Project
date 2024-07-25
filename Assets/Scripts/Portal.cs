using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject portalCard;

    public bool CardIsInLevel = true;


    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Model2") || other.gameObject.CompareTag("Model1") || other.gameObject.CompareTag("Model3") || other.gameObject.CompareTag("Model4"))
        {

            if (portalCard != null)
            {
                CardIsInLevel = true;
            }
            else
            {
                CardIsInLevel = false;
            }
        }
    }
}
