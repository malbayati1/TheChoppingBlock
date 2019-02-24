using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedMover : MonoBehaviour
{
    public float upVelo;
    public float groundedCheckRadius;
    public float groundedCheckOffsetDown;
    public LayerMask groundedCheckLM;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Move(float xInput, float zInput, float yOverride = 0f)
    {
        bool isGivingInput = xInput != 0 || zInput != 0;

        if (yOverride != 0f)
        {
            rb.velocity = Vector3.up * yOverride * 10f;
        }
        else if (IsGrounded())
        {
            //Debug.Log("hmm");
            //cause a hop upwards
            rb.velocity = Vector3.up * upVelo;
        }  
    }

    public bool IsGrounded()
    {
        return Physics.OverlapSphere(
                transform.position + Vector3.down * groundedCheckOffsetDown,
                groundedCheckRadius,
                groundedCheckLM).Length > 0;
    }
}
