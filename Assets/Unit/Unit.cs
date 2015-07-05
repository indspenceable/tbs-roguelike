using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
	public Tile tile;
	public int movement = 5;
	public int team;
	public bool usedThisTurn;

	public bool canHitAtRange(int i) {
		return i == 1;
	}

	public int cost(Tile t) {
		if (t.unit != null && (t.unit.team != this.team)) {
			// TODO check the team - only apply cost if they're enemies.
			return 999;
		}
		switch(t.terrain) {
		case Terrain.Open: return 1;
		case Terrain.Wall: return 999;
		default: return 999;
		}
	}
}
