using UnityEngine;
using System.Collections;

public interface MenuInput<T> : InputAction
{
	// there's a thing for the menu tho.
	void optionSelected(T choice);
}
