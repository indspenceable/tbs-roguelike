using UnityEngine;
using System.Collections;


public class Point {
	public int x;
	public int y;
	public Point(int x, int y) {
		this.x = x;
		this.y = y;
	}
	
	public override int GetHashCode() {
		return (this.x * 251) + this.y;
	}
	public override bool Equals(object obj) {
		Point p = obj as Point;
		return (p != null && p.x == this.x && p.y == this.y);
	}
	public override string ToString() {
		return "Point(" + x + ", " + y + ")";
	}

	public int distance(Point p) {
		return (Mathf.Abs(p.x - this.x)) + (Mathf.Abs(p.y - this.y));
	}

	public bool AdjacentTo (Point p) {
		return distance(p) == 1;
	}
}