using UnityEngine;
using UnityEngine.UI;

public class Enemy2Controller : MonoBehaviour
{
    [SerializeField] private Collider enemyLegColl;

    private void Start()
    {
        enemyLegColl.enabled = false;
    }

}
