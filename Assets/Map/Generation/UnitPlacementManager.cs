using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitPlacementManager : MonoBehaviour {

	public GameObject UnitPrefab;

	private GameManager mapManager;

	public List<Unit> setupUnits() {
		List<Unit> unitList = new List<Unit>();
		// For now, just generate a single unit at 3, 3
		unitList.Add(createUnitAt(3, 3, 0));
		unitList.Add(createUnitAt(4, 3, 1));

		return unitList;
	}

	public Unit createUnitAt(int x, int y, int team) {
		Tile t = GetComponent<GameManager>().tiles[x][y];
		GameObject go = Instantiate(UnitPrefab, t.transform.position, Quaternion.identity) as GameObject;
		Unit u = go.GetComponent<Unit>();
		u.tile = t;
		t.unit = u;
		u.team = team;

		return u;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
