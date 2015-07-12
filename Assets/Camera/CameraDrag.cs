using UnityEngine;
using System.Collections;

public class CameraDrag : MonoBehaviour
{
	private Vector3 oldPos;
	private Plane collisionPlane = new Plane(new Vector3(0,0,1), Vector3.zero);
	private Camera cam;

	void Start() {
		cam = GetComponent<Camera>();
	}

	void LateUpdate()
	{
		if(Input.GetMouseButtonDown(1))
		{
			oldPos = worldPointForCurrentMouse();
			return;
		}
		
		if(Input.GetMouseButton(1))
		{
			Vector3 newPos = worldPointForCurrentMouse();
			transform.Translate(oldPos-newPos);
		}

		if (Input.GetKeyDown(KeyCode.W)) {
			transform.Translate(Vector2.up);
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			transform.Translate(Vector2.left);
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			transform.Translate(Vector2.down);
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			transform.Translate(Vector2.right);
		}
	}
	
	Vector3 worldPointForCurrentMouse() {
		Vector3 viewportPoint = cam.ScreenToViewportPoint(Input.mousePosition);
		Ray ray = cam.ViewportPointToRay(viewportPoint);
		float d;
		collisionPlane.Raycast(ray, out d);
		return ray.GetPoint(d);
	}
}