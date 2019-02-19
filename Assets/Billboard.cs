using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	private float degrees;
	private Vector3 toCamera;

    void LateUpdate()
    {
		// toCamera = transform.position - Camera.main.transform.position;
		// toCamera.Set(toCamera.x, 0, toCamera.z);
		// degrees = Mathf.Cos(Vector3.Dot(toCamera.normalized , Vector3.forward));
		// Debug.Log(degrees);
		// transform.rotation = Quaternion.AngleAxis(degrees * Mathf.Rad2Deg, Vector3.up);
		
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
				Camera.main.transform.rotation * Vector3.up);
    }
}
