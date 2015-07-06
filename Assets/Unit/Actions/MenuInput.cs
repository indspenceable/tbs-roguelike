using UnityEngine;
using System.Collections;
using System;

public interface MenuInput<T> : InputAction
{
	// there's a thing for the menu tho.
	void optionSelected(T choice);
}


public abstract class MenuPlacementManager {
	public int count;
	public MenuPlacementManager(int count) {
		this.count = count;
	}
	public abstract Vector3 placement(int index);
}

public class CircularMenuPlacementManager : MenuPlacementManager {
	public CircularMenuPlacementManager (int c): base(c) { }

	public override Vector3 placement(int index) {
		float scalar = 1f + count * 0f;

		float degrees = (Mathf.Lerp(0, 360, index/(float)(count)) +90) %360;
		double rads = (Math.PI/180)*degrees;
		Vector3 trig = new Vector3((float)Math.Cos(rads), (float)Math.Sin(rads));
		return trig * scalar;
	}
}

public class ArcMenuPlacementManager : MenuPlacementManager {
	int MAX_DEGREES = 165;
	int MIN_DEGREES = 15;
	public ArcMenuPlacementManager (int c): base(c) { }
	
	public override Vector3 placement(int index) {
		float scalar = 1f + count * 0.05f;

		float degrees = 0f;
		if (count == 1) {
			degrees = (MAX_DEGREES + MIN_DEGREES)/2;
		} else {
			degrees = (Mathf.Lerp(15, 165, index/(float)(count-1)));
		}
		double rads = (Math.PI/180)*degrees;
		Vector3 trig = new Vector3((float)Math.Cos(rads), (float)Math.Sin(rads));
		return trig * scalar;
	}
}