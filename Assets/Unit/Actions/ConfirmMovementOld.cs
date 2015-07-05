using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfirmMovementOld : InputAction {
	private Unit actor;
	private Path path;
	private GameManager manager;
	private Highlighter highlighter;
	private List<Unit> unitsInRange;

	public void Setup (Unit act, Path path)
	{
		this.actor = act;
		this.path = path;
		this.manager = GameManager.instance;
		this.highlighter = manager.GetComponent<Highlighter>();
		unitsInRange = new List<Unit>();

		foreach(Unit u in this.manager.units) {
			if (actor.canHitAtRange(u.tile.p.distance(actor.tile.p))) {
				unitsInRange.Add(u);
			}
		}

		Debug.Log (actor);
		RecalculateHighlights();
	}

	public void RecalculateHighlights() {
		highlighter.removeAllHighlights();
		foreach (Unit u in unitsInRange) {
			highlighter.highlight(u.tile.p, Highlight.Style.RED);
		}
		highlighter.highlight(actor.tile.p, Highlight.Style.BLUE);
	}

	public void OnUnitClicked (Unit u)
	{
		Debug.Log (this.actor);
		if (u == actor) {
			// Stay put!
			u.usedThisTurn = true;
			highlighter.removeAllHighlights();
			InputManager.Instance.currentAction = null;
		} else {
			if (unitsInRange.Contains(u)) {
				// Attack!
			}
		}
	}
	public void OnTileClicked (Tile t)
	{
	}
	public void OnTileHovered (Tile t)
	{
	}
}