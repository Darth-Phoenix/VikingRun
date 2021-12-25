using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject tileA, tileB, cornerAB, cornerBA, endA, startA, endB, startB, FenceA, FenceB;
    private GameObject tileToSpawn, obstacle;
    public GameObject referenceObject;
    private GameObject child;
    public float timeOffset = 0.4f;
    private bool ObstacleUsed = false;
    private float r;
    private Vector3 previousTilePosition;
    private Vector3 spawnPos;
    private Vector3 direction, mainDirection = new Vector3(0, 0, 1), otherDirection = new Vector3(1, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        previousTilePosition = referenceObject.transform.position;
        spawnPos = previousTilePosition;
        tileToSpawn = tileA;
        direction = mainDirection;
        child = Instantiate(startA, spawnPos, Quaternion.Euler(0, 0, 0));
        child.transform.parent = referenceObject.transform;
        spawnPos -= 1.42f * direction;
        previousTilePosition = spawnPos;
        for (int i=0; i<5; i++)
        {
            spawnPos = previousTilePosition + 6.0f * direction;
            child = Instantiate(tileA, spawnPos, Quaternion.Euler(0, 0, 0));
            child.transform.parent = referenceObject.transform;
            previousTilePosition = spawnPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((spawnPos - transform.position).magnitude < 100)
        {
            r = Random.value;
            spawnPos = previousTilePosition + 6.0f * direction;
            if (r < 0.6)
            {
                direction = mainDirection;
                if (r < 0.3 && ObstacleUsed == false)
                {
                    if (tileToSpawn == tileA) obstacle = FenceA;
                    else obstacle = FenceB;
                    child = Instantiate(obstacle, spawnPos + 0.5f * Vector3.up, Quaternion.Euler(0, 0, 0));
                    child.transform.parent = referenceObject.transform;
                    ObstacleUsed = true;
                }
                else
                {
                    ObstacleUsed = false;
                }
            }
            else if (r < 0.8)
            {
                ObstacleUsed = false;
                direction = otherDirection;
                otherDirection = mainDirection;
                mainDirection = direction;
                if (tileToSpawn == tileA) tileToSpawn = cornerAB;
                else if (tileToSpawn == tileB) tileToSpawn = cornerBA;
                spawnPos += 0.44f * direction + 1.57f * otherDirection;
            }
            else
            {
                direction = mainDirection;
                ObstacleUsed = false;
                if (tileToSpawn == tileA)
                {
                    spawnPos -= 1.42f * direction;
                    child = Instantiate(endA, spawnPos, Quaternion.Euler(0, 0, 0));
                    child.transform.parent = referenceObject.transform;
                    spawnPos += 10.0f * direction;
                    tileToSpawn = startA;
                }
                else if (tileToSpawn == tileB)
                {
                    spawnPos -= 1.42f * direction;
                    child = Instantiate(endB, spawnPos, Quaternion.Euler(0, 0, 0));
                    child.transform.parent = referenceObject.transform;
                    spawnPos += 10.0f * direction;
                    tileToSpawn = startB;
                }
            }
            child = Instantiate(tileToSpawn, spawnPos, Quaternion.Euler(0, 0, 0));
            child.transform.parent = referenceObject.transform;
            if (tileToSpawn == cornerAB)
            {
                tileToSpawn = tileB;
                spawnPos += 1.57f * direction + 0.44f * otherDirection;
            }
            else if (tileToSpawn == cornerBA)
            {
                tileToSpawn = tileA;
                spawnPos += 1.57f * direction + 0.44f * otherDirection;
            }
            else if (tileToSpawn == startA)
            {
                tileToSpawn = tileA;
                spawnPos -= 1.42f * direction;
            }
            else if (tileToSpawn == startB)
            {
                tileToSpawn = tileB;
                spawnPos -= 1.42f * direction;
            }
            previousTilePosition = spawnPos;
        }

        if ((referenceObject.transform.GetChild(0).position - transform.position).magnitude > 30)
            Destroy(referenceObject.transform.GetChild(0).gameObject);
    }
}