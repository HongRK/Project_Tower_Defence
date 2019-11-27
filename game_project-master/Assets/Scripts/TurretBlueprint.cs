//정준권, 홍령기

using UnityEngine;
using System.Collections;

[System.Serializable]
public class TurretBlueprint {

	public GameObject prefab;
	public int cost {get; set;}
	public TurretBlueprint(int cost) {
		this.cost = cost;
	}

	public void upgradeTurret() {
		this.cost = cost * 2;
	}
	public int GetSellAmount ()
	{
		return cost / 2;
	}

}
