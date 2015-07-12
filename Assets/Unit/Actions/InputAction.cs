using UnityEngine;
using System.Collections;

public interface InputAction {
	void OnUnitClicked(Unit u, InputManager.MouseButton button);
	void OnTileClicked(Tile t, InputManager.MouseButton button);
	void OnTileHovered(Tile t);
}
