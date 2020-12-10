using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject soldierToFollow;
    public GameObject objectToFollow;
    public Transform target;
    public float distanceFromSoldier;
    public float speed;
    public bool shouldOffset;
    
	void Start () {
        distanceFromSoldier = 4;
        speed = 3;
        shouldOffset = false;
	}
	
	void Update () {
		
	}

    private void LateUpdate()
    {
        if (soldierToFollow != null)
        {
            float step = speed * Time.deltaTime;
            float x = soldierToFollow.transform.position.x;
            float y = transform.position.y;
            float z = 0;
            string name = this.name;
            if (name.Contains("player1"))
            {
                z = soldierToFollow.transform.position.z;
                if(shouldOffset)
                {
                    z = z + distanceFromSoldier;
                }
            }
            else
            {
                z = soldierToFollow.transform.position.z;
                if(shouldOffset)
                {
                    z = z - distanceFromSoldier;
                }
            }
            target.position = new Vector3(x, y, z);
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }

    public void setSoldierToFollow(GameObject soldierToFollow)
    {
        this.soldierToFollow = soldierToFollow;
        if (target == null)
        {
            target = new GameObject().transform;
        }
        float x = soldierToFollow.transform.position.x;
        float y = soldierToFollow.transform.position.y;
        float z = soldierToFollow.transform.position.z;
        Vector3 targetPosition = new Vector3(x, y, z);
        target.position = targetPosition;
    }

    private void LateUpdate_reworked()
    {
        if(objectToFollow != null)
        {
            float step = speed * Time.deltaTime;
            float x = objectToFollow.transform.position.x;
            float y = objectToFollow.transform.position.y;
            float z = objectToFollow.transform.position.z;

            target.position = objectToFollow.transform.position;
            target.rotation = objectToFollow.transform.rotation;
        }
    }
}
