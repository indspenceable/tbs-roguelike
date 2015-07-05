using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackTargetSelectAction : InputAction {
	public Unit actor;
	public Path path;
	public List<Unit> targetableUnits;

	private GameManager manager;
	private Highlighter highlighter;
	
	public void Setup(Unit actor, Path path, List<Unit> targetableUnits) {
		this.actor = actor;
		this.manager = GameManager.instance;
		this.highlighter = manager.GetComponent<Highlighter>();
		this.targetableUnits = targetableUnits;

		RecalculateHighlights();
	}
	
	public void RecalculateHighlights() {
		highlighter.removeAllHighlights();
		foreach(Unit u in targetableUnits) {
			highlighter.highlight(u.tile.p, Highlight.Style.RED);
		}
	}
	
	public void OnUnitClicked(Unit u) {
		if (targetableUnits.Contains(u)) {
			// Hey! We can attack this unit! Let's do it!
		}
	}
	public void OnTileClicked(Tile t) {
		// Clicking on a tile does nothing.
	}
	public void OnTileHovered(Tile t) {
		// When we hover a
	}
}
