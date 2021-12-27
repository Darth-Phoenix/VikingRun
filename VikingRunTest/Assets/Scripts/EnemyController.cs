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
    private float dist = 3.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = new Vector3(0, 1, -2);
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
        transform.position += movingSpeed * Time.deltaTime * transform.forward;
        transform.position = new Vector3(transform.position.x, viking.position.y, transform.position.z);
        if (!endgame)
        {
            if (transform.forward == new Vector3(1, 0, 0))
            {
                if (transform.position.x <= viking.position.x)
                {
                    transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                }
                else if (transform.position.z <= viking.position.z)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
                else if (transform.position.z > viking.position.z)
                {
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
                else if (transform.position.x > viking.position.x)
                {
                    transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                }
            }
            else if (transform.forward == new Vector3(0, 0, 1))
            {
                if (transform.position.z <= viking.position.z)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
                else if (transform.position.x <= viking.position.x)
                {
                    transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                }
                else if (transform.position.x > viking.position.x)
                {
                    transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                }
                else if ((transform.position.z > viking.position.z))
                {
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
            }
            else if (transform.forward == new Vector3(0, 0, -1))
            {
                if (transform.position.z >= viking.position.z)
                {
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
                else if (transform.position.x >= viking.position.x)
                {
                    transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                }
                else if (transform.position.x < viking.position.x)
                {
                    transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                }
                else if (transform.position.z < viking.position.z)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
            }
            else if (transform.forward == new Vector3(-1, 0, 0))
            {
                if (transform.position.x >= viking.position.x)
                {
                    transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                }
                else if (transform.position.z >= viking.position.z)
                {
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
                else if (transform.position.z < viking.position.z)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }
                else if (transform.position.x < viking.position.x)
                {
                    transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                }
            }

            if (transform.position.y < 1.05)
            {
                isjump = false;
                run = true;
            }

            if (transform.position.y < 0)
            {
                endgame = true;
                run = false;
                isjump = true;
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

            animator.SetBool("Run", run);
            animator.SetBool("Jump", isjump);
        }
    }       
}
