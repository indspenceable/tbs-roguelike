using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path {
	public static List<Path> findPathsForUnit(Unit actor, GameManager manager){
		return findPathsMaster(actor, manager, null);
	}

	public static Path findPathForUnit(Unit actor, GameManager manager, Point target) {
		return findPathsMaster(actor, manager, target)[0];
	}

	private static List<Path> findPathsMaster(Unit actor, GameManager manager, Point target){
		Point start = actor.tile.p;

		List<Path> discoveredPaths = new List<Path>();
		List<Path> unexploredPaths = new List<Path>();
		Path[] starterPaths = {
			new Path(start),
		};
		foreach (Path p in starterPaths) {
			discoveredPaths.Add(p);
			unexploredPaths.Add(p);
		}

		while(unexploredPaths.Count > 0) {
			// TODO - get a real priority queue in here...
			Path current = unexploredPaths[0];
			unexploredPaths.RemoveAt(0);

			Point destination = current.destination();
			Point[] neighbors = {  
				new Point(destination.x-1, destination.y),
				new Point(destination.x+1, destination.y),
				new Point(destination.x, destination.y-1),
				new Point(destination.x, destination.y+1)
			};
			foreach(Point dest in neighbors) {
				Path newPath = new Path(current, dest);
				if (onMap(dest, manager) && 
				    newPath.cost(actor, manager) <= actor.movement &&
				    notSeen(start, discoveredPaths, dest)) {
					if (newPath.destination().Equals(target)) {
						List<Path> rtn = new List<Path>(1);
						rtn.Add(newPath);
						return rtn;
					}
					discoveredPaths.Add(newPath);
					queueUp(actor, manager, unexploredPaths, newPath, target);
				}
			}
		}
		if (target != null) {
			return new List<Path>();
		}

		discoveredPaths.RemoveAt(0);
		return discoveredPaths;
	}

	private static void queueUp(Unit actor, GameManager manager, List<Path> paths, Path newPath, Point target) {
		if (target == null) {
			paths.Add(newPath);
			return;
		} else {
			for (int i = 0; i < paths.Count; i+=1) {
				if (totalCost(actor, manager, newPath, target) < 
				    totalCost(actor, manager, paths[i], target)) {
					paths.Insert(i, newPath);
					return;
				}
			}
		}
		paths.Add(newPath);
	}

	private static int totalCost(Unit actor, GameManager manager, Path newPath, Point target) {
		Point dest = newPath.destination();
		int distanceCost = (int)Mathf.Sqrt((target.x - dest.x)*(target.x - dest.x) + (target.y - dest.y)*(target.y - dest.y));
		return distanceCost + newPath.cost(actor, manager);
	}

	private static bool notSeen(Point start, List<Path> seenPaths, Point dest) {
		if (start.Equals(dest)) {
			return false;
		}
		foreach (Path p in seenPaths) {
			Point pd = p.destination();
			if (pd.Equals(dest)) {
				return false;
			}
		}
		return true;
	}

	private static bool onMap(Point point, GameManager manager) {
		return !(point.x < 0 || point.y < 0 || point.x >= manager.width || point.y >= manager.height);
	}

	public Path(Point point) {
		points = new List<Point>();
		points.Add(point);
	}
	public Path(Path path, Point point) {
		points = new List<Point>(path.points);
		points.Add(point);
	}
	public Path(Path path) {
		points = new List<Point>(path.points);
	}

	List<Point> points;

	public void TrimTo(Point p) {
		int i = points.IndexOf(p);
		if (i > -1) {
			points.RemoveRange(i+1, points.Count-(i+1));
		} else {
			Debug.LogError("Trying to trim to invalid point.");
		}
	}

	public bool contains(Point p) {
		return points.IndexOf(p) != -1;
	}
	public Point at(int index) {
		return points[index];
	}
	public int distance() {
		return points.Count;
	}
	public int cost(Unit actor, GameManager manager) {
		int sum = 0;
		foreach (Point p in points) {
			if (p == points[0]) {
				// Skip the starting point.
				continue;
			}
			sum += actor.cost(manager.tiles[p.x][p.y]);
		}
		return sum;
	}
	public Point destination() {
		return points[points.Count-1];
	}
	public override string ToString() {
		string str =  "path";
		foreach (Point p in points) {
			str += p.ToString();
		}
		return str;
	}
}
