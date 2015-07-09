using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
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
		    if (unit.team == Unit.Team.PLAYER && !unit.usedThisTurn) {
				Movement m = new Movement();
				m.Setup(unit);
				currentAction = m;
			}
		} else {
			currentAction.OnUnitClicked(unit);
		}
	}
}