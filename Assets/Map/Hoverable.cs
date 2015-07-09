using UnityEngine;
using System.Collections;

public class Hoverable : MonoBehaviour {
	bool hover;
	Camera cam;
	Plane p;
	StageManager currentStage;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		currentStage = CampaignManager.Instance.CurrentStage();
	}
	
	// Update is called once per frame
	void Update () {
		bool newHover = false;
		Plane p = new Plane(new Vector3(0,0,1), transform.position);
		Ray r = cam.ScreenPointToRay(Input.mousePosition);
		float dist;
		if (!p.Raycast(r, out dist)) {
			Debug.LogError ("This should never happen.... Camera isn't set right for 2d mode.");
			return;
		}
		Vector3 pt = r.GetPoint(dist);
		newHover = GetComponent<Collider2D>().OverlapPoint(new Vector2(pt.x, pt.y));

		if (newHover != hover) {
			if (newHover) {
				currentStage.InputManager.TileHovered(GetComponent<Tile>());
			} else {
				currentStage.InputManager.StopTileHovered(GetComponent<Tile>());
			}
		}
	}
}
