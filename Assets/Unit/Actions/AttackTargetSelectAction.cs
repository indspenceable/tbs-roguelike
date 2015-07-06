using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackTargetSelectAction : InputAction {
	public Unit actor;
	public Path path;
	public List<Unit> targetableUnits;

	private StageManager manager;
	private Highlighter highlighter;
	
	public void Setup(Unit actor, Path path, List<Unit> targetableUnits) {
		this.actor = actor;
		this.manager = StageManager.current;
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

	public IEnumerator onTriggerAttack(Unit u) {
		// Hey! We can attack this unit! Let's do it!
		highlighter.removeAllHighlights();
		InputManager.Instance.currentAction = new NoInput();
		yield return manager.StartCoroutine(manager.spawnBattleExecutor().doCombat(actor, u));
		manager.RemoveDeadUnits();


		if (manager.PlayerVictory()) {
			// If the player won, deal with it.
			yield return manager.DoPlayerVictory();
		} else if (manager.PlayerDefeat()) {
		} else {
			// Else, go back to input entry mode.
			InputManager.Instance.currentAction = null;
		}
	}

	public void OnUnitClicked(Unit u) {
		if (targetableUnits.Contains(u)) {
			manager.StartCoroutine(onTriggerAttack(u));
		}
	}
	public void OnTileClicked(Tile t) {
		// Clicking on a tile does nothing.
	}
	public void OnTileHovered(Tile t) {
		// When we hover a
	}
}
