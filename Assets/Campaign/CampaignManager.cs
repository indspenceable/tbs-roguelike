using UnityEngine;
using System.Collections;

public class CampaignManager : MonoBehaviour {
	public static CampaignManager Instance;

	// TODO FE sprites are here: http://www.feplanet.net/sprites-archive-overworld-sheets/7/1112/928

	[SerializeField]
	private GameObject StagePrefab;
	private StageManager currentStage = null;
	private Camera mainCamera;

	void Start() {
		Instance = this;
		mainCamera = Camera.main;

		buildNewStage();
	}

	private void buildNewStage() {
		GameObject g = Instantiate(StagePrefab) as GameObject;
		currentStage = g.GetComponent<StageManager>();
		currentStage.Build();
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
		buildNewStage();
		yield return null;
	}

	public IEnumerator DoPlayerDefeat() {
		yield return StartCoroutine(FadeOut(3f));
		yield return null;
	}
}
