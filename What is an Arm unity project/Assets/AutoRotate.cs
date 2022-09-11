using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {

    public AnimationCurve bob;
    public float bobSpeed = 0.2f;
    public float baseY = 0.5f;
    public Vector3 rotation;

    public float curBobOffset = 0;
    
    // Start is called before the first frame update
    void Start() {
        curBobOffset = Random.Range(0f, 1f);
        transform.Rotate(rotation * Random.Range(0, 100f), Space.World);
    }

    // Update is called once per frame
    void Update() {
        transform.localPosition = new Vector3(0, bob.Evaluate(curBobOffset) + baseY, 0);
        curBobOffset += bobSpeed * Time.deltaTime;
        if (curBobOffset > 1)
            curBobOffset -= 1;
        transform.Rotate(rotation * Time.deltaTime, Space.World);
    }
}
