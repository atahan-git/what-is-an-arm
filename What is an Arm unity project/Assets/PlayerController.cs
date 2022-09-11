using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody rg;
    // Start is called before the first frame update
    public Transform viewAxis;
    public float force = 100;
    void Start() {
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (ScoreController.isGameInProgress) {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var input = new Vector3(vertical, 0, -horizontal);
            input = viewAxis.TransformDirection(input);

            rg.AddTorque(input * force);

            viewAxis.position = transform.position;
        }
    }
}
