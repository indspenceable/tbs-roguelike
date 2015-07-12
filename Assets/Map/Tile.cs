using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	public Terrain terrain;
	public string terrainName;
	public Point p;
	public Unit unit;

	private StageManager currentStage;

	void Start() {
		currentStage = CampaignManager.Instance.CurrentStage();
	}

	public void OnMouseDown() {
		InputManager.MouseButton button;
		if (Input.GetMouseButtonDown(0)) {
			button = InputManager.MouseButton.LEFT;
		} else if (Input.GetMouseButtonDown(1)) {
			button = InputManager.MouseButton.RIGHT;
		} else {
			button = InputManager.MouseButton.MIDDLE;
		}

		if (unit == null) {
			currentStage.InputManager.OnTileClicked(this, button);
		} else {
			currentStage.InputManager.OnUnitClicked(unit, button);
		}
	}
}
