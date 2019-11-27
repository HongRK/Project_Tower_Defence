//È«·É±â

using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public static int Money;
	public int startMoney ;

	public static int Lives;
	public int startLives;

	public static int Rounds;

	void Start ()
	{
		Money = startMoney;
		Lives = startLives;

		Rounds = 0;
	}

}
