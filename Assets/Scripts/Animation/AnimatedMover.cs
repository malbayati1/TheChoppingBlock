using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedMover : MonoBehaviour
{
    [HideInInspector]
    public GameObject trackedObj;
    public float upVelo;
    public float lerpAmount;
    public float maxSpeed;
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
    public void Move(float xInput, float zInput)
    {
        bool isGivingInput = xInput != 0 || zInput != 0;
        bool isGrounded = IsGrounded();

        if (isGivingInput && isGrounded)
        {
            //cause a hop upwards
            rb.velocity = Vector3.up * upVelo;
        }
        Vector2 newXZPos = new Vector2(Mathf.LerpUnclamped(trackedObj.transform.position.x, transform.position.x,
            lerpAmount), Mathf.LerpUnclamped(trackedObj.transform.position.z, transform.position.z, lerpAmount));
        Vector2 oldXZPos = new Vector2(transform.position.x, transform.position.z);
        float maxPositionChangeAllowedThisFrame = maxSpeed * Time.deltaTime;
        Vector2 positionChange = Vector2.ClampMagnitude(newXZPos - oldXZPos, maxPositionChangeAllowedThisFrame);
        //lerp x and z position of this model to the player, leave y alone so that way hops can do everything to that
        transform.position = new Vector3(
            oldXZPos[0] + positionChange[0],
            transform.position.y,
            oldXZPos[1] + positionChange[1]
            );
        //update rotation to face towards the focus at all times
        Quaternion newRot = new Quaternion();
        Vector2 diffFromTracked = new Vector2(
            transform.position.x - trackedObj.transform.position.x,
            transform.position.z - trackedObj.transform.position.z);
        newRot.eulerAngles = new Vector3(0, Mathf.Rad2Deg * Mathf.Atan2(diffFromTracked.x, diffFromTracked.y) + 180, 0);
        transform.rotation = newRot;
    }

    public bool IsGrounded()
    {
        return Physics.OverlapSphere(
                transform.position + Vector3.down * groundedCheckOffsetDown,
                groundedCheckRadius,
                groundedCheckLM).Length > 0;
    }
}
