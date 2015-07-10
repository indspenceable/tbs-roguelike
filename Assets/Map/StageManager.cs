using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour {	
	[HideInInspector]
	public int width;
	[HideInInspector]
	public int height;
	
	[HideInInspector]
	public List<List<Tile>> tiles;
	public List<Unit> units;
	private GameObject tilesContainer;

	// Static singleton instance
	private InputManager inputManagerInstance;
	
	// Static singleton property
	public InputManager InputManager
	{
		// Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
		// otherwise we assign instance to a new component and return that
		get { 
			if (inputManagerInstance == null) {
				inputManagerInstance = new GameObject("InputManager").AddComponent<InputManager>();
				inputManagerInstance.transform.parent = transform;
			}
			return inputManagerInstance;
		}
	}

	public void RemoveDeadUnits () {
		foreach(Unit u in units.FindAll((Unit u) => u.hp <= 0)) {
			units.Remove(u);
			Destroy(u.gameObject);
		}
	}


	public void Build() {
		tilesContainer = new GameObject("Tile Container");
		tilesContainer.transform.parent = transform;

		MapGenerationManager mapGenerator = GetComponent<MapGenerationManager>();
		mapGenerator.Generate();

		tiles = new List<List<Tile>>();
		width = mapGenerator.width;
		height = mapGenerator.height;
		for (int x = 0; x < width; x++) {
			List<Tile> column = new List<Tile>();
			for (int y = 0; y < height; y++) {
				Terrain terrain = mapGenerator.GetTerrainForXY(x,y);
				GameObject prefab = mapGenerator.GetTilePrefabForTerrain(terrain);
				GameObject obj = Instantiate(prefab, new Vector3(x, y, 1), Quaternion.identity) as GameObject;
				obj.transform.parent = tilesContainer.transform;
				Tile tile = obj.GetComponent<Tile>();
				tile.p = new Point(x,y);
				column.Add(tile);
			}
			tiles.Add (column);
		}

		units = GetComponent<UnitPlacementManager>().setupUnits();
	}

	public IEnumerator TakeEnemyTurn(){
		InputManager.currentAction = new NoInput();

		foreach (Unit u in units) {
			if (u.team == Unit.Team.BADDIE) {
				u.usedThisTurn = false;
			}
		}

		foreach (Unit u in units) {
			if (u.team == Unit.Team.BADDIE) {
				List<Path> paths = Path.findPathsForUnit(u, this).FindAll((Path path) => path.distance() > 2);
				if (paths.Count > 0) {
					Path p = paths[Random.Range(0, paths.Count-1)];
					yield return StartCoroutine(Movement.moveUnitAlongPath(0.1f*p.distance(), u, p, this));
				}
			}
		}

		foreach (Unit u in units) {
			if (u.team == Unit.Team.PLAYER) {
				u.usedThisTurn = false;
			}
		}

		InputManager.currentAction = null;
	}

	public BattleExecutor spawnBattleExecutor() {
		return new OnMapBattleExecutor();
	}

	public bool NoMoreUnitsToMove(Unit.Team team) {
		foreach (Unit u in units) {
			if (u.team == team && !u.usedThisTurn) {
				return false;
			}
		}
		return true;
	}

	public bool PlayerVictory () {
		foreach(Unit u in units) {
			if (u.team == Unit.Team.BADDIE) {
				return false;
			}
		}
		return true;
	}
	public bool PlayerDefeat () {
		return false;
	}
}
