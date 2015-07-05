using UnityEngine;
using System.Collections;

public interface InputAction {
	void OnUnitClicked(Unit u);
	void OnTileClicked(Tile t);
	void OnTileHovered(Tile t);
}
