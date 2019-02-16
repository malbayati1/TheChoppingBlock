using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject toFollow;
	public Vector3 offset;

    void Start()
    {
		offset = transform.position - toFollow.transform.position;
    }

    void Update()
    {
		transform.position = toFollow.transform.position + offset;        
    }
}
