using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	public Terrain terrain;
	public string terrainName;
	public Point p;
	public Unit unit;

	public void OnMouseDown() {
		if (unit == null) {
			InputManager.Instance.OnTileClicked(this);
		} else {
			InputManager.Instance.OnUnitClicked(unit);
		}
	}
}
