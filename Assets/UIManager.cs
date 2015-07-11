using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	[SerializeField]
	private Text UnitInfoText;
	[SerializeField]
	private Image CurrentTerrainImage;

	private SpriteRenderer spriteRenderer;
	void Start() {
	}

	public void CurrentTile(Tile t) {
		CurrentTerrainImage.sprite = t.GetComponent<SpriteRenderer>().sprite;
	}

	public void CurrentUnit(Unit u) {
	}

	public void Show(bool value) {
		gameObject.SetActive(value);
	}
}
