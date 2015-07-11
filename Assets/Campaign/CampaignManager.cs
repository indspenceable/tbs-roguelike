using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CampaignManager : MonoBehaviour {
	public static CampaignManager Instance;

	// TODO FE sprites are here: http://www.feplanet.net/sprites-archive-overworld-sheets/7/1112/928

	[SerializeField]
	private GameObject StagePrefab;
	private StageManager currentStage = null;
	private Camera mainCamera;
	private List<UnitStats> units;

	void Start() {
		if (Instance == null) {
			Instance = this;
			mainCamera = Camera.main;
			BuildPlayerArmy();
			BuildNewStage(units);
		}
	}

	private void BuildPlayerArmy() {
		units = new List<UnitStats>();
		units.Add(UnitStats.initAsEnemy(new UnitClass.Soldier(), 1, 1));
		units.Add(UnitStats.initAsEnemy(new UnitClass.Flyer(), 1, 1));
	}

	private void BuildNewStage(List<UnitStats> playerArmy) {
		GameObject g = Instantiate(StagePrefab) as GameObject;
		currentStage = g.GetComponent<StageManager>();
		currentStage.Build(playerArmy);
		BoundCameraToBoard b = mainCamera.GetComponent<BoundCameraToBoard>();
		if (b == null) {
			b = mainCamera.gameObject.AddComponent<BoundCameraToBoard>();
		}
		b.stage = currentStage.gameObject;
	}

	public StageManager CurrentStage() {
		return currentStage;
	}

	public IEnumerator FadeOut(float time) {
		yield return null;
	}
	public IEnumerator DoPlayerVictory() {
		yield return StartCoroutine(FadeOut(3f));
		Destroy(currentStage.gameObject);
		BuildNewStage(units);
		yield return null;
	}

	public IEnumerator DoPlayerDefeat() {
		yield return StartCoroutine(FadeOut(3f));
		yield return null;
	}
}
