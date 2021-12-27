using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    bool isjump = false;
    bool run = true;
    bool endgame = false;

    private float movingSpeed = 8.9f;
    Animator animator;
    public Transform viking;
    private float dist = 10.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = viking.position - new Vector3(0, 0, dist);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "viking")
        {
            movingSpeed = 0;
            if (!endgame)
            {
                endgame = true;
                run = false;
                isjump = false;
                animator.SetBool("Punch", endgame);
            }
        }
    }

    void Update()
    {
        if ((transform.position - viking.position).magnitude > 10)
            transform.position = viking.position - viking.forward * 10;
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
        }
        animator.SetBool("Run", run);
        animator.SetBool("Jump", isjump);
    }       
}
