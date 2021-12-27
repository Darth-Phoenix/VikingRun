using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private float trailDistance = 2f;
    private float heightOffset = 3f;
    private float cameraDelay = 0.02f;
   
    void Update()
    {
        Vector3 followPos = target.position - target.forward * trailDistance;

        followPos.y += heightOffset;
        transform.position += (followPos - transform.position) * cameraDelay;

        transform.LookAt(target.transform);
    }
}
