using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TEMP CLASS THAT WILL BE UPDATED WHEN WE DECIDE ON WHAT KIND OF CAMERA WE WANT

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
