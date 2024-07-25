using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePortalCard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Model2") || other.gameObject.CompareTag("Model1") || other.gameObject.CompareTag("Model3") || other.gameObject.CompareTag("Model4"))
        {
            Destroy(gameObject);
        }
    }

    //private void OnColliderEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player") /*|| other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Model1") || other.gameObject.CompareTag("Model3") || other.gameObject.CompareTag("Model4")*/)
    //    {
    //        Destroy(gameObject);
    //    }
    //}


}
