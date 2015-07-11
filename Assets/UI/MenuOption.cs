using UnityEngine;
using System.Collections;

public class MenuOption : MonoBehaviour {
	MenuInput<ConfirmMovement.UnitAction> menuInput;
	ConfirmMovement.UnitAction choice;

	public void Setup(MenuInput<ConfirmMovement.UnitAction> menuInput, ConfirmMovement.UnitAction choice) {
		this.menuInput = menuInput;
		this.choice = choice;
	}

	void OnMouseDown() {
		menuInput.optionSelected(choice);
	}

	void OnMouseEnter() {
		StopAllCoroutines();
		StartCoroutine(resize(1f, 0.1f));
	}
	void OnMouseExit() {
		StopAllCoroutines();
		StartCoroutine(resize(0.75f, 0.1f));
	}

	void OnDestroy() {
		StopAllCoroutines();
	}
	
	private IEnumerator resize(float scale, float time) {
		float startingScale = transform.localScale.x;
		float dt = 0f;
		while(dt < time) {
			dt += Time.deltaTime;
			float newScale = Mathf.Lerp(startingScale, scale, dt/time);
			transform.localScale = new Vector3(newScale, newScale, 1f);
			yield return null;
		}
	}
}
