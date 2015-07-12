using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackTargetSelectAction : InputAction {
	public Unit actor;
	public Path path;
	public List<Unit> targetableUnits;

	private StageManager currentStage;
	private Highlighter highlighter;
	
	public void Setup(Unit actor, Path path, List<Unit> targetableUnits) {
		this.actor = actor;
		this.currentStage = CampaignManager.Instance.CurrentStage();
		this.highlighter = currentStage.GetComponent<Highlighter>();
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
		currentStage.InputManager.currentAction = new NoInput();
		yield return currentStage.StartCoroutine(currentStage.spawnBattleExecutor().doCombat(actor, u));
		actor.usedThisTurn = true;
		currentStage.RemoveDeadUnits();


		if (currentStage.PlayerVictory()) {
			// If the player won, deal with it.
			CampaignManager campaign = CampaignManager.Instance;
			yield return campaign.StartCoroutine(campaign.DoPlayerVictory());
		} else if (currentStage.PlayerDefeat()) {
			CampaignManager campaign = CampaignManager.Instance;
			yield return campaign.StartCoroutine(campaign.DoPlayerDefeat());
		} else if (currentStage.NoMoreUnitsToMove(Unit.Team.PLAYER)) {
			yield return currentStage.StartCoroutine(currentStage.TakeEnemyTurn());
		} else {
			// Else, go back to input entry mode.
			currentStage.InputManager.currentAction = null;
		}
	}

	public void OnUnitClicked(Unit u, InputManager.MouseButton button) {
		if (targetableUnits.Contains(u)) {
			currentStage.StartCoroutine(onTriggerAttack(u));
		}
	}
	public void OnTileClicked(Tile t, InputManager.MouseButton button) {
		// Clicking on a tile does nothing.
	}
	public void OnTileHovered(Tile t) {
		// When we hover a
	}
}
