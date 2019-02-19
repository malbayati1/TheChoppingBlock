using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public GameObject toFollow;
	public float cameraDistance;
	public float cameraAngle;
	[Range(0, 1)]
	public float lerpFactor;
	public float rotationDegreesPerSecond;

	public Vector3 forwardDirection = Vector3.forward;
	public Vector3 rightDirection = Vector3.right;

	private Vector3 offset;
	private float radius;
	private float height;
	private float currentRotationDegrees;

    void OnValidate()
    {
		offset = new Vector3(cameraDistance * Mathf.Cos(cameraAngle * Mathf.Deg2Rad), cameraDistance * Mathf.Sin(cameraAngle * Mathf.Deg2Rad), 0);	
		height = offset[1];
		radius = Mathf.Sqrt(offset[0] * offset[0] + offset[2] * offset[2]);
		UpdateRotationAndOffset();
    }

	void LateUpdate()
	{
		UpdatePosition(true);
		float cameraRotation = Input.GetAxisRaw("CameraRotation");
		if(cameraRotation != 0)
		{
			currentRotationDegrees +=  cameraRotation * Time.deltaTime * rotationDegreesPerSecond;
			UpdateRotationAndOffset();
		}
	}

	public void UpdatePosition(bool lerp)
	{
		transform.position = Vector3.Lerp(transform.position, toFollow.transform.position + offset, (lerp) ? lerpFactor : 1);
	}

	public void UpdateRotationAndOffset()
	{
		Quaternion temp = new Quaternion();
		temp.eulerAngles = new Vector3(cameraAngle, 270 - currentRotationDegrees, 0);
		transform.rotation = temp;
		offset = new Vector3(radius * Mathf.Cos(currentRotationDegrees * Mathf.Deg2Rad), height, radius * Mathf.Sin(currentRotationDegrees * Mathf.Deg2Rad));	
		forwardDirection.Set(offset.x, 0, offset.z);
		forwardDirection *= -1;
		rightDirection = Vector3.Cross(Vector3.up, forwardDirection);
	}
}
