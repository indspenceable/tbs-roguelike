using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class UnitClass {
	public int hpBase;
	public int strengthBase;
	public int magicBase;
	public int skillBase;
	public int speedBase;
	public int luckBase;
	public int defenseBase;
	public int resistanceBase;
	public int conBase;
	public int moveBase;
	
	public int hpGrowth;
	public int strengthGrowth;
	public int magicGrowth;
	public int skillGrowth;
	public int speedGrowth;
	public int luckGrowth;
	public int defenseGrowth;
	public int resistanceGrowth;

	public abstract int ClassId();
	
	public UnitClass(int hpBase, int strengthBase, int magicBase, int skillBase, int speedBase, int luckBase, int defenseBase, int resistanceBase, int conBase, int moveBase,
	                 int hpGrowth, int strengthGrowth, int magicGrowth, int skillGrowth, int speedGrowth, int luckGrowth, int defenseGrowth, int resistanceGrowth) {
		this.hpBase = hpBase;
		this.strengthBase = strengthBase;
		this.magicBase = magicBase;
		this.skillBase = skillBase;
		this.speedBase = speedBase;
		this.luckBase = luckBase;
		this.defenseBase = defenseBase;
		this.resistanceBase = resistanceBase;
		this.conBase = conBase;
		this.moveBase = moveBase;
		
		this.hpGrowth = hpGrowth;
		this.strengthGrowth = strengthGrowth;
		this.magicGrowth = magicGrowth;
		this.skillGrowth = skillGrowth;
		this.speedGrowth = speedGrowth;
		this.luckGrowth = luckGrowth;
		this.defenseGrowth = defenseGrowth;
		this.resistanceGrowth = resistanceGrowth;
	}

	public virtual int Cost(Tile t) {
		switch(t.terrain) {
		case Terrain.Open: return 1;
		case Terrain.Wall: return 999;
		default: return 999;
		}
	}

	public virtual int Movement() {
		return 5;
	}

	public class Soldier : UnitClass {
		public Soldier() : base(20, 3, 3, 0, 1, 0, 0, 0, 6, 5, 80, 50, 50, 30, 20, 25, 12, 15) { }
		public override int ClassId() {
			return 0;
		}
	}

	public class Flyer : UnitClass {
		public Flyer() : base(20, 3, 3, 0, 1, 0, 0, 0, 6, 5, 80, 50, 50, 30, 20, 25, 12, 15) { }
		public override int ClassId() {
			return 1;
		}
		public override int Cost(Tile t) {
			return 1;
		}
		public override int Movement() {
			return 7;
		}
	}
}

public class UnitStats {
	public UnitClass klass;
	public int level = 1;
	
	public int curHP;
	public int maxHp;
	public int strength;
	public int magic;
	public int defense;
	public int resistance;
	public int speed;
	public int constitution;

	private static int randomFromSpread(int growth, int level, int growthStrength) {
		int spread = (growth*level*growthStrength/100);
		int percentValue = growth*level + Random.Range(0, spread);
		return (percentValue/100) + (Random.Range(1, 100) > percentValue%100 ? 1 : 0);
	}
	
	public static UnitStats initAsEnemy(UnitClass klass, int level, int growthStrength) {
		UnitStats stats = new UnitStats();

		stats.klass = klass;
		stats.level = level;
		
		stats.maxHp = klass.hpBase + randomFromSpread(klass.hpGrowth, level, growthStrength);
		stats.strength = klass.strengthBase + randomFromSpread(klass.strengthGrowth, level, growthStrength);
		stats.magic = klass.magicBase + randomFromSpread(klass.magicGrowth, level, growthStrength);
		stats.defense = klass.defenseBase + randomFromSpread(klass.defenseGrowth, level, growthStrength);
		stats.resistance = klass.resistanceBase + randomFromSpread(klass.resistanceGrowth, level, growthStrength);
		stats.speed = klass.speedBase + randomFromSpread(klass.speedGrowth, level, growthStrength);
		
		stats.curHP = stats.maxHp;

		return stats;
	}
}
