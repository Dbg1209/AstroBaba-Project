using UnityEngine;

public class BabaController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,5,0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        { 
            DisableBaba();
        }
    }

    void DisableBaba()
    {
        gameObject.SetActive(false);

    }
}
