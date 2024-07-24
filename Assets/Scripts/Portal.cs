using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private GameObject obj;

    public bool CardIsInLevel = true;

    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("PortalCard1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Model2") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Model1"))
        {
            if (obj != null)
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
