using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]

public class VikingController : MonoBehaviour
{
    public Vector3 MovingDirection;
    public float JumpingForce = 200, MovingThreshold;
    bool isjump = false;
    bool run = false;
        
    //MeshRenderer mr;
    [SerializeField]float movingSpeed = 10f;
    Rigidbody rb;
    Animator animator;
    NavMeshAgent agent;

    RaycastHit raycastHit;
    Vector2 velocity = Vector2.zero;


    private void OnAnimatorMove()
    {
        transform.position = agent.nextPosition;
    }
    void Awake()
    {
        //Debug.Log("awake");
    }

    void Start()
    {
        //Transform t = GetComponent<Transform>();
        //mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        transform.position = Vector3.one;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "big_module_01_floor")
        {
            isjump = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.transform.name == "big_module_01_floor")
        {
            isjump = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {

    }

    void Update()
    {

        /*run = false;

        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += movingSpeed * Time.deltaTime * Vector3.forward;
            run = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += movingSpeed * Time.deltaTime * Vector3.back;
            run = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += movingSpeed * Time.deltaTime * Vector3.left;
            run = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += movingSpeed * Time.deltaTime * Vector3.right;
            run = true;
        }*/


        if (!isjump)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                {
                    isjump = true;
                    rb.AddForce(JumpingForce * Vector3.up);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(r, out raycastHit))
            {
                if (raycastHit.collider.transform.name == "Viking_Shield(Clone)")
                {
                    Destroy(raycastHit.collider.gameObject);
                }              
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(r, out raycastHit))
            {
                agent.SetDestination(raycastHit.point);
            }
        }

        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dz = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPostition = new Vector2(dx, dz);
        velocity = deltaPostition / Time.deltaTime;
        run = velocity.magnitude > MovingThreshold && agent.remainingDistance > agent.radius;

        animator.SetBool("Run", run);
    }
}
