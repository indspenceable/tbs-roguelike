using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
	public enum Team {
		PLAYER = 0,
		BADDIE = 1
	}
	public enum Klass {
		SWORDFIGHTER = 0,
		FLYER = 1 
	}

	Animator animator;
	SpriteRenderer spriteRenderer;
	void Start() {
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator.SetInteger("team", (int)team);
		animator.SetInteger("class", (int)klass);
		animator.SetInteger("animation", 0);
	}

	public Tile tile;
	public int movement = 5;

	private Team _team;
	public Team team {
		get {
			return _team;
		}
		set {
			if (animator != null) {
				animator.SetInteger("team", (int)value);
			}
			_team = value;
		}
	}
	private Klass _klass;
	public Klass klass {
		get {
			return _klass;
		}
		set {
			if (animator != null) {
				animator.SetInteger("class", (int)value);
			}
			_klass = value;
		}
	}


	public int hp = 10;

	private bool _usedThisTurn;
	public bool usedThisTurn {
		get {
			return _usedThisTurn;
		}
		set {
			if (value) {
				spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
			} else {
				spriteRenderer.color = new Color(1f, 1f, 1f);
			}
			_usedThisTurn = value;
		}
	}

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
