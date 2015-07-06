using UnityEngine;
using System.Collections;

public class BoundCameraToBoard : MonoBehaviour {
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;

	public GameObject mapManager;

	void Start() {

	}
	
	void LateUpdate() {
		Camera cam = GetComponent<Camera>();
		StageManager map = mapManager.GetComponent<StageManager>();
	
		float viewportHeight = 2f * cam.orthographicSize;
		float viewportWidth = viewportHeight * cam.aspect;

		float minX = 0f;
		float minY = 0f;
		float maxX = map.width;
		float maxY = map.height;


		float nx = transform.position.x;
		if (nx + viewportWidth/2 > maxX - 0.5){ 
			nx = maxX - 0.5f - viewportWidth/2;
		}
		if (nx - viewportWidth/2 < minX - 0.5){ 
			nx = minX - 0.5f + viewportWidth/2;
		}
		float ny = transform.position.y;
		if (ny + viewportHeight/2 > maxY - 0.5){ 
			ny = maxY - 0.5f - viewportHeight/2;
		}
		if (ny - viewportHeight/2 < minY - 0.5){ 
			ny = minY - 0.5f + viewportHeight/2;
		}

		transform.position = new Vector3(nx, ny, transform.position.z);
	}
}
