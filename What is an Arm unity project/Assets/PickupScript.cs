using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    private void Start() {
        GameMaster.s.RegisterPickup();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            GameMaster.s.PickupCollected(gameObject);
        }
    }
}
