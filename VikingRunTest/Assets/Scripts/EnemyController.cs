using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    bool isjump = false;
    bool run = true;
    bool endgame = false;

    [SerializeField] float movingSpeed = 9.0f;
    Animator animator;
    public Transform viking;
    private float dist = 10.0f;

    void Awake()
    {

    }

    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = viking.position - dist * viking.forward;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "viking")
        {
            endgame = true;
            run = false;
            isjump = false;
            animator.SetBool("Punch", endgame);
        }
    }

    void Update()
    {
        if (!endgame)
        {
            transform.position += movingSpeed * Time.deltaTime * transform.forward;
            dist = (viking.position - transform.position).magnitude;
            transform.position = viking.position - dist * viking.forward;

            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.Rotate(new Vector3(0f, -90f, 0f));
                transform.position = viking.position - dist * viking.forward;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.Rotate(new Vector3(0f, 90f, 0f));
                transform.position = viking.position - dist * viking.forward;
            }


            if (!isjump)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    {
                        isjump = true;
                        run = false;
                    }
                }
            }

            if (transform.position.y < 1.03)
            {
                isjump = false;
                run = true;
            }

            animator.SetBool("Run", run);
            animator.SetBool("Jump", isjump);
        }
    }       
}
