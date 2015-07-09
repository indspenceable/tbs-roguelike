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
		if (unit == null) {
			currentStage.InputManager.OnTileClicked(this);
		} else {
			currentStage.InputManager.OnUnitClicked(unit);
		}
	}
}
