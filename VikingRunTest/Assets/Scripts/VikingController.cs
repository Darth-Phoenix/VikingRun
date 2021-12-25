using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
//[RequireComponent(typeof(NavMeshAgent))]

public class VikingController : MonoBehaviour
{
    //public Vector3 MovingDirection;
    public float JumpingForce = 100;
    bool isjump = false;
    bool run = false;
        
    //MeshRenderer mr;
    [SerializeField]float movingSpeed = 9.0f;
    Rigidbody rb;
    Animator animator;
    NavMeshAgent agent;

    RaycastHit raycastHit;
 
    //Vector2 velocity = Vector2.zero;


    /*private void OnAnimatorMove()
    {
        transform.position = agent.nextPosition;
    }*/
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
        //agent = GetComponent<NavMeshAgent>();
        //agent.updatePosition = false;
        transform.position = Vector3.one;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Contains("module"))
        {
            isjump = false;
        }
        if (collision.transform.name.Contains("rock"))
        {
            rb.AddForce(transform.forward * -10);
            movingSpeed = 0;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.transform.name.Contains("module"))
        {
            isjump = false;
        }
        if (collision.transform.name.Contains("rock"))
        {
            rb.AddForce(transform.forward * -10);
            movingSpeed = 0;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        movingSpeed = 9;
    }

    void Update()
    {
        run = true;
        transform.localPosition += movingSpeed * Time.deltaTime * transform.forward;

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(new Vector3(0f, -90f, 0f));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(new Vector3(0f, 90f, 0f));
        }


        if (!isjump)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                {
                    isjump = true;
                    rb.AddForce(150 * Vector3.up);
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

        /*if (Input.GetMouseButtonDown(1))
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
        run = velocity.magnitude > MovingThreshold && agent.remainingDistance > agent.radius;*/

        animator.SetBool("Run", run);
        animator.SetBool("Jump", isjump);
    }
}
