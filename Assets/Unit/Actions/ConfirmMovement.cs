using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ConfirmMovement : MonoBehaviour, MenuInput<ConfirmMovement.UnitAction>{
	// Prefabs
	[SerializeField]
	private GameObject MenuSpritePrefab;
	[SerializeField]
	private Sprite moveOnly;
	[SerializeField]
	private Sprite attackSprite;

	// Fields
	private Unit actor;
	private Path path;
	private List<Unit> unitsInRange;
	private List<GameObject> menuOptions;

	// No Map Inputs
	public void OnUnitClicked (Unit u) {}
	public void OnTileClicked (Tile t) {}
	public void OnTileHovered (Tile t) {}

	public enum UnitAction {
		ATTACK,
		WAIT
	}
	
	public void SetupAndInstall (Unit act, Path path)
	{
		this.actor = act;
		this.path = path;
		GameManager manager = GameManager.instance;
		unitsInRange = new List<Unit>();
		foreach(Unit otherUnit in manager.units) {
			int distance = otherUnit.tile.p.distance(actor.tile.p);
			if (actor.canHitAtRange(distance) && (actor.team != otherUnit.team) && (actor !=otherUnit)) {
				unitsInRange.Add(otherUnit);
			}
		}

		// Build a list of MenuOptions.
		menuOptions = new List<GameObject>();
		menuOptions.Add(buildSpriteOption(moveOnly, UnitAction.WAIT));


		// Always can just stop
		if (unitsInRange.Count > 0) {
			menuOptions.Add(buildSpriteOption(attackSprite, UnitAction.ATTACK));
		}

		StartCoroutine(AnimateAndInstall(0.1f, menuOptions));
	}

	public IEnumerator AnimateAndInstall(float time, List<GameObject> menuOptions) {
		float dt = 0;
		int c = menuOptions.Count;
		List<Vector3> endingPositions = new List<Vector3>(c);

		float scalar = 1f + c * 0f;

		for (int i = 0; i < c; i+= 1) {
			float degrees = (Mathf.Lerp(0, 360, i/(float)(c)) +90) %360;
			double rads = (Math.PI/180)*degrees;
			Vector3 trig = new Vector3((float)Math.Cos(rads), (float)Math.Sin(rads));
			endingPositions.Add(trig * scalar + actor.transform.position);
		}

		while(dt < time){
			dt += Time.deltaTime;
			for (int i = 0; i < c; i+=1) {
				int j = (c+i-1)%c;
				Vector3 pt1 = endingPositions[j];
				Vector3 pt2 = endingPositions[i];
				Vector3 target = Vector3.Lerp(pt1, pt2, dt/time);

				menuOptions[i].transform.position = Vector3.Lerp(actor.transform.position, target, dt/time);
			}

			yield return null;
		}
	}

	private GameObject buildSpriteOption(Sprite sprite, UnitAction choice) {
		GameObject go = Instantiate(MenuSpritePrefab) as GameObject;
		go.GetComponent<SpriteRenderer>().sprite = sprite;
		go.GetComponent<MenuOption>().Setup(this, choice);
		return go;
	}

	public void optionSelected(UnitAction choice) {
		switch(choice) {
		case UnitAction.ATTACK:
			attack();
			return;
		case UnitAction.WAIT:
			wait();
			return;
		}
	}

	private void clearMenu() {
		foreach (GameObject g in menuOptions) {
			Destroy(g);
		}
	}


	private void attack() {
		clearMenu();
		AttackTargetSelectAction attackSelector = new AttackTargetSelectAction();
		attackSelector.Setup(actor, path, unitsInRange);	
		InputManager.Instance.currentAction = attackSelector;
	}
	private void wait() {
		clearMenu();
		actor.usedThisTurn = true;
		InputManager.Instance.currentAction = null;
	}
}
