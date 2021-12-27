using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]

public class VikingController : MonoBehaviour
{
    public GameOver EndScreen;
    public HideIcon Icon;

    bool isjump = false;
    bool run = true;
    bool endgame = false;
    private int coins = 0;
    float StartTime, EndTime;

    private float movingSpeed = 9.0f;
    Rigidbody rb;
    Animator animator;
    NavMeshAgent agent;

    RaycastHit raycastHit;

    private void End()
    {
        EndScreen.Setup(EndTime - StartTime, coins);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        transform.position = new Vector3(0, 1, 1);

        StartTime = Time.time;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Monster")
        {
            endgame = true;
            run = false;
            isjump = false;
            movingSpeed = 0;
            transform.Rotate(new Vector3(0f, 90f, 0f));
            animator.SetBool("Defeated", endgame);
            EndTime = Time.time;
            Invoke("End", 2.0f);
        }
        else
        {
            if (collision.transform.name.Contains("module"))
            {
                if (transform.position.y < 1)
                {
                    rb.AddForce(transform.forward * -10);
                    movingSpeed = 0;
                }
                else
                {
                    isjump = false;
                    run = true;
                }
            }
            if (collision.transform.name.Contains("rock"))
            {
                rb.AddForce(transform.forward * -10);
                movingSpeed = 0;
            }
            if (collision.transform.name.Contains("Viking_Shield"))
            {
                coins++;
                Destroy(collision.gameObject);
            }
        }      
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.transform.name.Contains("module"))
        {
            if (transform.position.y < 1)
            {
                rb.AddForce(transform.forward * -10);
                movingSpeed = 0;
            }
            else
            {
                isjump = false;
                run = true;
            }
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
        transform.localPosition += movingSpeed * Time.deltaTime * transform.forward;
        if (!endgame)
        {

            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.Rotate(new Vector3(0f, -90f, 0f));
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.Rotate(new Vector3(0f, 90f, 0f));
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (!isjump)
                {
                    isjump = true;
                    run = false;
                    rb.AddForce(145 * Vector3.up);
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

            if (transform.position.y < 0)
            {
                endgame = true;
                run = false;
                isjump = true;
                EndTime = Time.time;
                Invoke("End", 2.0f);
            }

            animator.SetBool("Run", run);
            animator.SetBool("Jump", isjump);
        }
        else
        {
            Icon.Hide();
        }
    }
}
