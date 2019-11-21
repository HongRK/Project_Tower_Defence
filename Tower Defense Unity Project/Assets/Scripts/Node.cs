using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Vector3 positionOffset;

    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;


    [HideInInspector]
    public GameObject turret;
	[HideInInspector]
	public TurretBlueprint turretBlueprint;
	[HideInInspector]
	public bool isUpgraded = false;

	BuildManager buildManager;

	void Start ()
	{
		buildManager = BuildManager.instance;
    }

	public Vector3 GetBuildPosition ()
	{
		return transform.position + positionOffset;
	}

	void OnMouseDown ()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (turret != null)
		{
			buildManager.SelectNode(this);
			return;
		}

            buildManager.SelectBuild(this);
            return;
	}

	public void BuildTurret ()
	{
		if (PlayerStats.Money < standardTurret.cost)
		{
			Debug.Log("Not enough money to build that!");
			return;
		}

        GameObject []Tur = new GameObject[3];

        Tur[0] = standardTurret.prefab;
        Tur[1] = missileLauncher.prefab;
        Tur[2] = laserBeamer.prefab;
         

		PlayerStats.Money -= standardTurret.cost;

		GameObject _turret = (GameObject)Instantiate(Tur[Random.Range(0,3)], GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		turretBlueprint = standardTurret;

		GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		Debug.Log("Turret build!");
	}

	public void UpgradeTurret ()
	{
		if (PlayerStats.Money < turretBlueprint.upgradeCost)
		{
			Debug.Log("Not enough money to upgrade that!");
			return;
		}

		PlayerStats.Money -= turretBlueprint.upgradeCost;

       /*
        GameObject[] Turup = new GameObject[3];

        Turup[0] = Resources.Load<GameObject>("Resources/LaserBeamer_Upgraded") as GameObject;
        Turup[1] = Resources.Load<GameObject>("Resources/MissileLauncher_Upgraded") as GameObject;
        Turup[2] = Resources.Load<GameObject>("Resources/StandardTurret_Upgraded") as GameObject;
        */
        //Get rid of the old turret
        Destroy(turret);

		//Build a new one
		GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		isUpgraded = true;

		Debug.Log("Turret upgraded!");
	}

	public void SellTurret ()
	{
		PlayerStats.Money += turretBlueprint.GetSellAmount();

		GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		Destroy(turret);
		turretBlueprint = null;
	}

	void OnMouseEnter ()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (!buildManager.CanBuild)
			return;
	}
}
