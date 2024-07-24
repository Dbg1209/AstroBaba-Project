using UnityEngine;
using UnityEngine.UI;

public class Enemy3Controller : MonoBehaviour
{
    [SerializeField] private Collider enemyLegColl;

    private void Start()
    {
        enemyLegColl.enabled = false;
    }
}
