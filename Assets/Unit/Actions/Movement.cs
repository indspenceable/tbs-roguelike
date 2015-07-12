using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : InputAction {
	public Unit actor;
	public Path currentPath;
	private HashSet<Point> destinationPoints;

	private StageManager currentStage;
	private Highlighter highlighter;

	public void Setup(Unit actor) {
		this.actor = actor;
		this.currentStage = CampaignManager.Instance.CurrentStage();
		this.highlighter = currentStage.GetComponent<Highlighter>();

		destinationPoints = new HashSet<Point>(Path.findPathsForUnit(actor, currentStage).ConvertAll((Path input) => input.destination()));
		currentPath = new Path(actor.tile.p);

		RecalculateHighlights();
	}

	public void RecalculateHighlights() {
		highlighter.removeAllHighlights();
		foreach (Point dest in destinationPoints) {
			Unit otherUnit = currentStage.tiles[dest.x][dest.y].unit;
//			if (otherUnit == null || (otherUnit != actor && otherUnit.team == actor.team)) {
				highlighter.highlight(dest, Highlight.Style.GREEN);
//			}
		}
		for(int i = 0; i < currentPath.distance() - 1; i+= 1) {
			highlighter.highlightPath(currentPath.at(i), currentPath.at(i+1));
		}
		highlighter.highlight(actor.tile.p, Highlight.Style.BLUE);
	}

	public static IEnumerator moveUnitAlongPath(float time, Unit u, Path path, StageManager stage) {
		float dt = 0;
		int distance = path.distance();
		
		for(;;){
			dt += Time.deltaTime;
			if (dt >= time || distance == 1) {
				break;
			}
			//which step
			float completedAMT = ((dt*(distance-1))/time);
			int sq = (int)completedAMT;
			float pct = completedAMT - sq;
			
			Point p1 = path.at(sq);
			Point p2 = path.at(sq+1);
			float x = Mathf.Lerp(p1.x, p2.x, pct);
			float y = Mathf.Lerp(p1.y, p2.y, pct);
			
			u.transform.position = new Vector2(x, y);
			yield return null;
			
		}
		Point dest = path.destination();
		u.transform.position = new Vector2(dest.x, dest.y);
		u.tile.unit = null;
		u.tile = stage.tiles[dest.x][dest.y];
		u.tile.unit = u;
	}

	public IEnumerator finishMovementCoroutine(float time) {
		yield return currentStage.StartCoroutine(moveUnitAlongPath(time, actor, currentPath, currentStage));

		ConfirmMovement confirmMovement = currentStage.GetComponent<ConfirmMovement>();
		currentStage.InputManager.currentAction = confirmMovement;
		confirmMovement.SetupAndInstall(actor, currentPath);
	}


	public void OnUnitClicked(Unit u, InputManager.MouseButton button) {
		if (u == actor) {
			OnTileClicked(u.tile, button);
		}
	}
	public void OnTileClicked(Tile t, InputManager.MouseButton button) {
		// We allow you to move to an uninhabited tile only;
		// the tile hover logic should cover moving to valid tiles
		if (t.p.Equals(currentPath.destination())) {
			// We're good to go!
			highlighter.removeAllHighlights();
			currentStage.InputManager.currentAction = new NoInput();
			currentStage.StartCoroutine(finishMovementCoroutine(0.1f * currentPath.distance()));
		} else {
			highlighter.removeAllHighlights();
			currentStage.InputManager.currentAction = null;
		}
	}
	public void OnTileHovered(Tile t) {
		// Are we on the current path?
		if (currentPath.contains(t.p)) {
			currentPath.TrimTo(t.p);
			RecalculateHighlights();
			return;
		}
		// Are we adjacent to a member of the current path?
		for (int i = currentPath.distance()-1; i >= 0; i-=1) {
			if (t.p.AdjacentTo(currentPath.at(i))) {
				Path copyPath = new Path(currentPath);
				copyPath.TrimTo(currentPath.at (i));
				Path newPath = new Path(copyPath, t.p);
				if (newPath.cost(actor, currentStage) <= actor.stats.klass.Movement()) {
					currentPath = newPath;
					RecalculateHighlights();
					return;
				}
			}
		}

		// Are we pathable?
		if (destinationPoints.Contains(t.p)) {
			currentPath = Path.findPathForUnit(actor, currentStage, t.p);
		} else {
			// Default to no path, if we're not already set to that.
			currentPath = new Path(actor.tile.p);
		}
		RecalculateHighlights();
	}
}