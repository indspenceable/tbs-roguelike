using UnityEngine;
using System.Collections;

public class Highlighter : MonoBehaviour {
	private GameObject container;
	[SerializeField]
	private GameObject TileHighlight;

	void Start() {
		container = new GameObject("Highlight Container");
		container.transform.parent = transform;
	}

	public void removeHighlightAt(Point p) {
		foreach (Transform child in container.transform)
		{
			if (p.Equals(child.gameObject.GetComponent<Highlight>().p)) {
				Destroy(child.gameObject);
			}
		}
	}
	
	public void highlight(Point p, Highlight.Style s) {
		removeHighlightAt(p);
		GameObject go = Instantiate(TileHighlight) as GameObject;
		go.transform.parent = container.transform;
		go.transform.position = new Vector3(p.x, p.y, -1);
		Highlight h =  go.GetComponent<Highlight>();
		h.p = p;
		h.setColor(s);
	}
	
	// TODO -  this should actually draw arrows! WOMPITY WOMP.
	public void highlightPath(Point previousPoint, Point p) {
		highlight(p, Highlight.Style.BLUE);
	}
	
	public void removeAllHighlights() {
		foreach (Transform child in container.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}
}
