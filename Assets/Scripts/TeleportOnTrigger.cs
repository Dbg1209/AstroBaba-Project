using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{
    private Portal portalCard;
    public Vector3 newPosition;

    private void Start()
    {
        portalCard = GameObject.Find("PortalGoToLevel2").GetComponent<Portal>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Portal") && !portalCard.CardIsInLevel) 
        {
            Debug.Log(portalCard.CardIsInLevel);
            transform.position = newPosition;
        }
    }
}
