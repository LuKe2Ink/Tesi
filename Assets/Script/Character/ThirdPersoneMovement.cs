using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class ThirdPersoneMovement : MonoBehaviour
{
    public static ThirdPersoneMovement instance;
    public CharacterController controller;
    public Camera camera;
    public CinemachineFreeLook cameraPlayer;
    public NavMeshAgent agent;
    public NavMeshSurface surface;
    public GameObject inventory;
    private Animator animator;


    public float speed = 6f;
    public float rotationspeed = 6f;
    public float gravity = -9.81f;
    public float cameraAxis = 0.5f;

    public bool cameraMouse = false;

    public Terrain terrain;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        terrain.GetComponent<TerrainCollider>().enabled = false;

        terrain.GetComponent<TerrainCollider>().enabled = true;

        cameraAxis = 1f;
        cameraMouse = true;
        cameraPlayer.m_YAxis.Value = cameraAxis;

        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //per schivare il boss
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                inventory.SetActive(!inventory.active);
            }
            if (Boss.instance)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                    Boss.instance.DodgeAttack(KeyCode.Q);

                if (Input.GetKeyDown(KeyCode.W))
                    Boss.instance.DodgeAttack(KeyCode.W);

                if (Input.GetKeyDown(KeyCode.A))
                    Boss.instance.DodgeAttack(KeyCode.A);
            }
        }

        //movimento con mouse

        if (!inventory.active)
        {
            if (Input.GetMouseButtonDown(0) && cameraMouse)
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);
                }

            }
        }
        
        if(agent.remainingDistance > agent.stoppingDistance)
        {
            animator.SetBool("Walking", true);
        }else
        {
            animator.SetBool("Walking", false);
        }
    }
}



        /*if (Input.GetKeyDown(KeyCode.R))
        {
            surface.BuildNavMesh();
        }
            Eventualmente pi� in l�
            if (Input.GetKeyDown(KeyCode.F2))
            {
                if (cameraAxis >= 1f)
                {
                    cameraAxis = 0.5f;
                    cameraMouse = false;
                } else
                {
                    cameraAxis = 1f;
                    cameraMouse = true;
                }
                cameraPlayer.m_YAxis.Value = cameraAxis;
            }
            //per movimento con tasti
            float horizontal = Input.GetAxisRaw("Horizontal"); //-1 se premo A o <-, +1 se premo D o ->
            float vertical = Input.GetAxisRaw("Vertical"); //+1 se premo W o freccetta su, -1 se premo S o freccetta giu


            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            //Debug.Log(direction);

            if(direction.magnitude >= 0.1f && !cameraMouse)
            {
                controller.Move(speed * Time.deltaTime * direction);
                //Debug.Log(controller.transform.position);
                Vector3 vettore = new Vector3();
                vettore.y += gravity * (Time.deltaTime);
                //controller.Move(vettore * Time.deltaTime);
                Vector3 moveDestination = transform.position + direction;
                if (direction != Vector3.zero)
                {
                    transform.forward = direction; 
                }
            }*/

        //perch� il character controller non applica la gravit�


