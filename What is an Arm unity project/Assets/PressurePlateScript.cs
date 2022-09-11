using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateScript : MonoBehaviour {

    public DoorScript myDoor;
    public DoorScript myPlate;

    public float zHeight = 0.01f;

    public Material noMaterial;
    public Material yesMaterial;

    private LineRenderer ln;

    public bool yes = false;
    void Start() {
        ln = GetComponentInChildren<LineRenderer>();
        var pos = new Vector3[2];
        pos[0] = transform.position;
        pos[1] = myDoor.transform.position;
        pos[0].y = zHeight;
        pos[1].y = zHeight;
        
        ln.SetPositions(pos);
        
        CheckIfActive();
    }


    public List<Collider> colliders = new List<Collider>();
    private void OnTriggerEnter (Collider other) {
        if (!colliders.Contains(other)) { colliders.Add(other); }
        CheckIfActive();
    }
 
    private void OnTriggerExit (Collider other) {
        colliders.Remove(other);
        CheckIfActive();
    }

    void CheckIfActive() {
        yes = false;
        for (int i = 0; i < colliders.Count; i++) {
            if (colliders[i].attachedRigidbody.gameObject.CompareTag("Arm") || colliders[i].attachedRigidbody.gameObject.CompareTag("Player")) {
                yes = true;
                break;
            }
        }

        if (yes) {
            ln.material = yesMaterial;
        } else {
            ln.material = noMaterial;
        }

        myDoor.isDoorOpen = yes;
        myPlate.isDoorOpen = yes;
    }
}
