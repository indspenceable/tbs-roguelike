using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
	public enum Team {
		PLAYER,
		BADDIE
	}
	
	public Tile tile;
	public int movement = 5;
	public Team team;
	public int hp = 10;

	public bool usedThisTurn;

	public bool CanHitAtRange(int i) {
		return i == 1;
	}
	public int GetDamageVs(Unit u) {
		return 3;
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
