using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    public Transform door;
    public Transform openPos;
    public Transform closedPos;

    public bool isDoorOpen = false;
    public float doorOpenSpeed = 0.5f;
    public float doorCloseSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
	    if (isDoorOpen) {
		    door.position = Vector3.MoveTowards(door.position, openPos.position, doorOpenSpeed * Time.deltaTime);
	    } else {
		    door.position = Vector3.MoveTowards(door.position, closedPos.position, doorCloseSpeed * Time.deltaTime);
	    }
    }
}
