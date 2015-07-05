using UnityEngine;
using System.Collections;

public class Highlight : MonoBehaviour {
	[HideInInspector]
	public Point p;

	public enum Style {
		GREEN,
		RED,
		BLUE
	}

	public void setColor(Style s) {
		switch(s) {
		case Style.BLUE:
			GetComponent<SpriteRenderer>().color = Blue;
			return;
		case Style.RED:
			GetComponent<SpriteRenderer>().color = Red;
			return;
		case Style.GREEN:
			GetComponent<SpriteRenderer>().color = Green;
			return;
		}
	}

	public Color Green;
	public Color Red;
	public Color Blue;
}
