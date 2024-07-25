using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{
    [Header("Player Portal Cards")]

    [SerializeField] private GameObject portalCard1, portalCard2, portalCard3;
    private Portal portalCardScript1, portalCardScript2, portalCardScript3;
    public Vector3 newPosition1, newPosition2, newPosition3;

    private void Start()
    {
        portalCardScript1 = portalCard1.GetComponent<Portal>();
        portalCardScript2 = portalCard2.GetComponent<Portal>();
        portalCardScript3 = portalCard3.GetComponent<Portal>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Portal") && !portalCardScript1.CardIsInLevel) 
        {
            Debug.Log(portalCardScript1.CardIsInLevel);
            transform.position = newPosition1;
        }
        if (other.gameObject.CompareTag("Portal") && !portalCardScript2.CardIsInLevel)
        {
            Debug.Log(portalCardScript2.CardIsInLevel);
            transform.position = newPosition2;
        }
        if (other.gameObject.CompareTag("Portal") && !portalCardScript3.CardIsInLevel)
        {
            Debug.Log(portalCardScript3.CardIsInLevel);
            transform.position = newPosition3;
        }
    }
}
