using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitPlacementManager : MonoBehaviour {

	public GameObject UnitPrefab;

	private StageManager mapManager;

	public List<Unit> setupUnits() {
		List<Unit> unitList = new List<Unit>();
		// For now, just generate a single unit at 3, 3
		unitList.Add(createUnitAt(3, 3, Unit.Team.PLAYER));
		unitList.Add(createUnitAt(4, 3, Unit.Team.BADDIE));

		return unitList;
	}

	public Unit createUnitAt(int x, int y, Unit.Team team) {
		Tile t = GetComponent<StageManager>().tiles[x][y];
		GameObject go = Instantiate(UnitPrefab, t.transform.position, Quaternion.identity) as GameObject;
		go.transform.parent = transform;
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
