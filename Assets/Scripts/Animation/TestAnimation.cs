using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    public GameObject trackedObj;
    public float upVelo;
    public float hopTime;
    public float lerpAmount;
    public float minHopHeight;

    private Vector2 changeDueToLerp = new Vector2();
    private Rigidbody rb;
    private Coroutine runningHop;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isGivingInput = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
        Vector2 lerpThisFrame = new Vector2(
            Mathf.LerpUnclamped(trackedObj.transform.position.x, transform.position.x, lerpAmount),
            Mathf.LerpUnclamped(trackedObj.transform.position.z, transform.position.z, lerpAmount)
            );
        if(isGivingInput && timer <= 0 && minHopHeight > transform.position.y)
        {
            rb.velocity = Vector3.up * upVelo;
            timer = hopTime;
        }
        changeDueToLerp = lerpThisFrame;
        transform.position = new Vector3(
            lerpThisFrame[0],
            transform.position.y,
            lerpThisFrame[1]);
        timer -= Time.deltaTime;
        if (timer < 0)
            timer = 0;
    }
}
