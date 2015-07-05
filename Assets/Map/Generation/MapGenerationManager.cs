using UnityEngine;
using System.Collections;

public class MapGenerationManager : MonoBehaviour {
	public int width;
	public int height;

	public GameObject OpenPrefab;
	public GameObject WallPrefab;
	public 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Generate() {}
	
	public Terrain GetTerrainForXY(int x, int y) {
		if (x == 0 || x == width-1 || y == 0 || y == height-1) {
			return Terrain.Wall;
		} else {
			if (Random.Range(0,3) == 2) {
				return Terrain.Wall;
			} else {
				return Terrain.Open;
			}
		}
	}
	
	public GameObject GetTilePrefabForTerrain(Terrain t) {
		switch(t) {
		case Terrain.Wall:
			return WallPrefab;
		case Terrain.Open:
			return OpenPrefab;
		}
		return null;
	}
}
