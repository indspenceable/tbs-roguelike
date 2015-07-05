﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	// Static singleton instance
	public static GameManager instance;
	
	// Use this for initialization
	void Start () {
		tilesContainer = new GameObject("Tile Container");
		tilesContainer.transform.parent = transform;

		if (instance != null) {
			Destroy(this.gameObject);
		} else {
			instance = this;
			Build();
		}
	}
	
	[HideInInspector]
	public int width;
	[HideInInspector]
	public int height;


	[HideInInspector]
	public List<List<Tile>> tiles;
	public List<Unit> units;
	private GameObject tilesContainer;

	void Build() {
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
				GameObject obj = Instantiate(prefab, new Vector3(x, y), Quaternion.identity) as GameObject;
				obj.transform.parent = tilesContainer.transform;
				Tile tile = obj.GetComponent<Tile>();
				tile.p = new Point(x,y);
				column.Add(tile);
			}
			tiles.Add (column);
		}

		units = GetComponent<UnitPlacementManager>().setupUnits();
	}

	public void buildMenuOption (ConfirmMovement confirmMovement, object attack)
	{

	}
}