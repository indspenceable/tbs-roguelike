using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	UIManager UI;

	private Tile currentHoveredTile;
	public InputAction currentAction { set; private get; }

	public void Start() {
		UI = GameObject.FindGameObjectWithTag("levelUI").GetComponent<UIManager>();
	}

	public enum MouseButton {
		LEFT,
		RIGHT,
		MIDDLE,
	}

	public void TileHovered(Tile t) {
		if (currentHoveredTile == t) {
			return;
		}
		currentHoveredTile = t;

		if (currentAction != null) {
			currentAction.OnTileHovered(t);
		} else {
			FixUIForTileHovered(t);
		}
	}

	public void FixUIForTileHovered(Tile t) {
		if (t.unit != null) {
			Unit u = t.unit;
			UI.CurrentUnit(u);
			UI.Show (true);
		} else {
			if (UI != null) {
				UI.Show(false);
			}
		}
	}

	public void StopTileHovered(Tile t) {
	}

	public void OnTileClicked(Tile t, MouseButton button) {
		if (currentAction != null) {
			currentAction.OnTileClicked(t, button);
		}
	}



	public void OnUnitClicked (Unit unit, MouseButton button)
	{
		if (currentAction == null) {
		    if (unit.team == Unit.Team.PLAYER && !unit.usedThisTurn) {
				Movement m = new Movement();
				m.Setup(unit);
				currentAction = m;
			}
		} else {
			currentAction.OnUnitClicked(unit, button);
		}
	}
}