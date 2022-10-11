using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int hp; //gemme da dover raccogliere
    private Animator animator;

    public float timerNormal = 10f;
    public float timerHeavy = 30f;

    public int numberObjectFallen;

    private float lastNormal = 0.0f;
    private float lastHeavy= 0.0f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //Debug.Log(Time.time);
        if (Time.time > lastHeavy)
        {
            //fa attacco pesante
            animator.SetTrigger("Heavy");
            lastHeavy = Time.time + timerHeavy;
            lastNormal = Time.time + timerNormal;
            Debug.Log(Time.time);
            Debug.Log(lastNormal);

        } else if(Time.time > lastNormal)
        {

            //fa attacco normale
            animator.SetTrigger("Normal");
            lastNormal = Time.time + timerNormal;
        }
    }

    
}
