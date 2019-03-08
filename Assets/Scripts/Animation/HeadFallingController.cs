using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFallingController : MonoBehaviour
{
    public float gravity;
    public GameObject body;

    private float initOffset;

    void Start()
    {
        initOffset = transform.position.y - body.transform.position.y;
    }

    void Update()
    {
        Vector3 nextPosition = transform.position + Vector3.down * gravity * Time.deltaTime;
        if (nextPosition.y - initOffset < body.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, body.transform.position.y + initOffset, transform.position.z);
        }
        else
        {
            transform.position = nextPosition;
        }
    }
}
