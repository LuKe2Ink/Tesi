using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Presa gemma");
            Boss.instance.ReduceHealth();
            Destroy(this.gameObject);
        }
    }
}
