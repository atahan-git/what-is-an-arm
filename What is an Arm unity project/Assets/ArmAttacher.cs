using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAttacher : MonoBehaviour {
    public GameObject currentArm;
    public bool isArmAttached = false;

    private void OnTriggerEnter(Collider other) {
        if (isArmAttached) {
            return;
        }
        if (other.transform.root.gameObject.CompareTag("Arm")) {
            if (currentArm != null) {
                currentArm.GetComponent<Outline>().enabled = false;
            }

            currentArm = other.transform.root.gameObject;
            currentArm.GetComponent<Outline>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (isArmAttached) {
            return;
        }
        if (other.transform.root.gameObject == currentArm) {
            currentArm.GetComponent<Outline>().enabled = false;
            currentArm = null;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (isArmAttached) {
                isArmAttached = false;
                currentArm.transform.SetParent(null);
                currentArm.AddComponent<Rigidbody>();
                var rg = gameObject.AddComponent<Rigidbody>();
                rg.isKinematic = true;
                rg.useGravity = false;
                currentArm.GetComponent<Outline>().enabled = true;

            } else {
                if (currentArm != null) {
                    isArmAttached = true;
                    Destroy(currentArm.GetComponent<Rigidbody>());
                    Destroy(GetComponent<Rigidbody>());
                    currentArm.transform.SetParent(transform);
                    currentArm.transform.localPosition = Vector3.zero;
                    currentArm.transform.localRotation = Quaternion.identity;
                    currentArm.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
}
