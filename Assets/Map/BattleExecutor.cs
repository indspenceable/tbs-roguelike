using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface BattleExecutor {
	IEnumerator doCombat(Unit attacker, Unit defender);
}


