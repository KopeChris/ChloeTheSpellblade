using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

	private void Awake()
	{
		transform.position = target.position;

	}
    void FixedUpdate()
	{
		//transform.position = target.position + offset;

		Vector3 desiredPosition = target.position + offset;

		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;


	}

}