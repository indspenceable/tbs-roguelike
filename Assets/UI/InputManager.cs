using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	// Static singleton instance
	private static InputManager instance;
	
	// Static singleton property
	public static InputManager Instance
	{
		// Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
		// otherwise we assign instance to a new component and return that
		get { return instance ?? (instance = new GameObject("InputManager").AddComponent<InputManager>()); }
	}

	public void OnGUI() {
	}

	private Tile currentHoveredTile;
	public InputAction currentAction { set; private get; }


	public void TileHovered(Tile t) {
		if (currentHoveredTile == t) {
			return;
		}
		currentHoveredTile = t;
		if (currentAction != null) {
			currentAction.OnTileHovered(t);
		}
	}

	public void StopTileHovered(Tile t) {
	}

	public void OnTileClicked(Tile t) {
		if (currentAction != null) {
			currentAction.OnTileClicked(t);
		}
	}

	public void OnUnitClicked (Unit unit)
	{
		if (currentAction == null) {
			Movement m = new Movement();
			m.Setup(unit);
			currentAction = m;
		} else {
			currentAction.OnUnitClicked(unit);
		}
	}
}