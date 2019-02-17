using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    public GameObject trackedObj;
    public float upVelo;
    public float lerpAmount;
    public float groundedCheckRadius;
    public float groundedCheckOffsetDown;
    public LayerMask groundedCheckLM;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isGivingInput = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
        bool isGrounded = Physics.OverlapSphere(
                transform.position + Vector3.down * groundedCheckOffsetDown,
                groundedCheckRadius,
                groundedCheckLM).Length > 0;

        if (isGivingInput && isGrounded)
        {
            //cause a hop upwards
            rb.velocity = Vector3.up * upVelo;
        }
        //lerp x and z position of this model to the player, leave y alone so that way hops can do everything to that
        transform.position = new Vector3(
            Mathf.LerpUnclamped(trackedObj.transform.position.x, transform.position.x, lerpAmount),
            transform.position.y,
            Mathf.LerpUnclamped(trackedObj.transform.position.z, transform.position.z, lerpAmount));
    }
}
