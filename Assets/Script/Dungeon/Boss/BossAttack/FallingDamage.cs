using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            Player.instance.HealthDown();
            Debug.Log("Preso");
            Boss.instance.DestroyAllRocks();
        }
    }
}
