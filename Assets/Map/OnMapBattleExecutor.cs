using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnMapBattleExecutor : BattleExecutor {
	private StageManager stageManager;

	public OnMapBattleExecutor() {
		this.stageManager = CampaignManager.Instance.CurrentStage();
	}

	public IEnumerator doCombat(Unit attacker, Unit defender) {
		yield return stageManager.StartCoroutine(doFight(attacker, defender));
		// Do experience + level up if needed.
	}

	private IEnumerator doFight(Unit attacker, Unit defender) {
		yield return stageManager.StartCoroutine(animateAttack(attacker, defender, 0.3f));
		yield return new WaitForSeconds(0.2f);
		defender.hp -= attacker.GetDamageVs(defender);
		// if defender is alive
		if (defender.hp <= 0) {
			yield return stageManager.StartCoroutine(animateDeath(defender, 0.2f));
			yield break;
		}

		yield return stageManager.StartCoroutine(animateAttack(defender, attacker, 0.3f));
		yield return new WaitForSeconds(0.2f);
		attacker.hp -= defender.GetDamageVs(attacker);
		// if defender is alive
		if (attacker.hp <= 0) {
			yield return stageManager.StartCoroutine(animateDeath(attacker, 0.2f));
			yield break;
		}
	}
	private IEnumerator animateDeath(Unit actor, float time) {
		float dt = 0f;
		SpriteRenderer sr = actor.GetComponent<SpriteRenderer>();
		for(;;) {
			dt += Time.deltaTime;
			if (dt > time) {
				break;
			}
			Color col = new Color(sr.color.r, sr.color.b, sr.color.g, 1-(dt/time));
			sr.color = col;
			yield return null;
		}
	}

	private IEnumerator animateAttack(Unit attacker, Unit defender, float time) {
		Vector3 attackerPosition = attacker.transform.position;
		Vector3 defenderPosition = defender.transform.position;

		float dt = 0f;
		for (;;) {
			dt += Time.deltaTime;
			if (dt > time) {
				break;
			}

			float pct = 0f;
			if (dt > time/2) {
				pct = ((time/2) - dt + (time/2))/time;
			} else {
				pct = dt/time;
			}

			attacker.transform.position = Vector3.Lerp(attackerPosition, defenderPosition, pct);
			yield return null;
		}

		attacker.transform.position = attackerPosition;
	}
}


