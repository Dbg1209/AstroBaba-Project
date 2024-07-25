using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform player;

    Vector3 offset = new Vector3(0, 3.90f, -3.94f);

   

    private void LateUpdate()
    {
        Vector3 desiredPosition = player.position + Quaternion.Euler(0, player.eulerAngles.y, 0) * offset;

        transform.position = desiredPosition;

        float originalXRotation = transform.eulerAngles.x;
        float originalZRotation = transform.eulerAngles.z;

        transform.LookAt(player);

        transform.rotation = Quaternion.Euler(originalXRotation, transform.eulerAngles.y, originalZRotation);
    }
}
