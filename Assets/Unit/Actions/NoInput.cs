using UnityEngine;
using System.Collections;

public class NoInput : InputAction {
	public void OnUnitClicked (Unit u, InputManager.MouseButton button) {}
	public void OnTileClicked (Tile t, InputManager.MouseButton button) {}
	public void OnTileHovered (Tile t) {}
}
