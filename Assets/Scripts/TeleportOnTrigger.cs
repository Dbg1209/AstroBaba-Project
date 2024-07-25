using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{
    [Header("Player Portal Cards")]

    [SerializeField] private GameObject portalCard1, portalCard2, portalCard3, conditionCanvas;
    private Portal portalCardScript1, portalCardScript2, portalCardScript3;
    public Vector3 newPosition1, newPosition2, newPosition3;

    private void Start()
    {   
        conditionCanvas.SetActive(false);
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
        else if(other.gameObject.CompareTag("Portal") && portalCardScript1.CardIsInLevel)
        {   
            conditionCanvas.SetActive(true);
            Invoke("HideCondition", 3f);
        }

        if (other.gameObject.CompareTag("Portal2") && !portalCardScript2.CardIsInLevel)
        {
            Debug.Log(portalCardScript2.CardIsInLevel);
            transform.position = newPosition2;
        }
        else if (other.gameObject.CompareTag("Portal2") && portalCardScript2.CardIsInLevel) 
        {
            conditionCanvas.SetActive(true);
            Invoke("HideCondition", 3f);
        }

        if (other.gameObject.CompareTag("Portal3") && !portalCardScript3.CardIsInLevel)
        {
            Debug.Log(portalCardScript3.CardIsInLevel);
            transform.position = newPosition3;
        }
        else if (other.gameObject.CompareTag("Portal3") && portalCardScript3.CardIsInLevel) 
        {
            conditionCanvas.SetActive(true);
            Invoke("HideCondition", 3f);
        }
    }

    void HideCondition()
    {
        conditionCanvas.SetActive(false);
    }
}
