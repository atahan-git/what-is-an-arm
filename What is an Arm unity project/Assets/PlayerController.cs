using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Transform viewAxis;
    public Transform guy;
    public float rotationMatchSpeed = 20;
    
    [Tooltip("Maximum slope the character can jump on")]
    [Range(5f, 60f)]
    public float slopeLimit = 45f;
    [Tooltip("Move speed in meters/second")]
    public float moveSpeed = 5f;
    [Tooltip("Turn speed in degrees/second, left (+) or right (-)")]     
    public float turnSpeed = 300;
    [Tooltip("Whether the character can jump")]
    public bool allowJump = false;
    [Tooltip("Upward speed to apply when jumping in meters/second")]
    public float jumpSpeed = 4f;

    public bool IsGrounded;
    public Vector3 MovementInput;
    public bool JumpInput;
    
    
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        ProcessActions();
    }
    
    /// <summary>
    /// Checks whether the character is on the ground and updates <see cref="IsGrounded"/>
    /// </summary>
    private void CheckGrounded()
    {
        IsGrounded = false;
        float capsuleHeight = Mathf.Max(capsuleCollider.radius * 2f, capsuleCollider.height);
        Vector3 capsuleBottom = transform.TransformPoint(capsuleCollider.center - Vector3.up * capsuleHeight / 2f);
        float radius = transform.TransformVector(capsuleCollider.radius, 0f, 0f).magnitude;
        Ray ray = new Ray(capsuleBottom + transform.up * .01f, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, radius * 5f))
        {
            float normalAngle = Vector3.Angle(hit.normal, transform.up);
            if (normalAngle < slopeLimit)
            {
                float maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;
                if (hit.distance < maxDist)
                    IsGrounded = true;
            }
        }
    }
    
    /// <summary>
    /// Processes input actions and converts them into movement
    /// </summary>
    private void ProcessActions() {
        MovementInput.y = 0;
        
        // Process Movement/Jumping
        if (IsGrounded)
        {
            // Reset the velocity
            rigidbody.velocity = Vector3.zero;
            // Check if trying to jump
            if (JumpInput && allowJump)
            {
                // Apply an upward velocity to jump
                rigidbody.velocity += Vector3.up * jumpSpeed;
            }

            // Apply a forward or backward velocity based on player input
            rigidbody.velocity += MovementInput * moveSpeed;
        }
        else
        {
            // Check if player is trying to change forward/backward movement while jumping/falling
            if (!Mathf.Approximately(MovementInput.magnitude, 0f))
            {
                // Override just the forward velocity with player input at half speed
                Vector3 verticalVelocity = Vector3.Project(rigidbody.velocity, Vector3.up);
                rigidbody.velocity = verticalVelocity + MovementInput * moveSpeed / 2f;
            }
        }
    }
    
    void Update() {
        if (GameMaster.isGameInProgress) {
            GetInputs();
            MatchRotation();
        } else {
            MovementInput = Vector3.zero;
        }
    }

    void MatchRotation() {
        if (MovementInput.sqrMagnitude > 0.1f) {
            guy.rotation = Quaternion.Lerp(guy.rotation, Quaternion.LookRotation(MovementInput), rotationMatchSpeed * Time.deltaTime);
        }
    }

    void GetInputs() {
        /*// Get input values
        int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        bool jump = Input.GetKey(KeyCode.Space);
        ForwardInput = vertical;
        TurnInput = horizontal;
        JumpInput = jump;*/
        
        
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var input = new Vector3(horizontal, 0, vertical);
        input = viewAxis.TransformDirection(input);

        MovementInput = input;
        
        bool jump = Input.GetKey(KeyCode.Space);
        JumpInput = jump;


        viewAxis.position = transform.position;
    }
}
