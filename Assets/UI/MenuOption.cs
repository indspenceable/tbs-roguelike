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
}
