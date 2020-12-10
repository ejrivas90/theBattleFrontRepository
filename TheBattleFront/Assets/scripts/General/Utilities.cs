using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

public class Utilities : MonoBehaviour {
    private static System.Random random = new System.Random();

    private void Awake()
    {
    }

	private static int getRandomDiceRoll(int dieSize) {
		int startingNumber = random.Next (1, 10000);
		int dividingNumber = random.Next (2,11);

		while (startingNumber > dieSize) {
			startingNumber = startingNumber / dividingNumber;
		}
		if (startingNumber == 0) {
			startingNumber = getRandomDiceRoll (dieSize);
		}

		return startingNumber;
	}

    public static int roll4Die()
    {
        int outcome = 0;
		outcome = getRandomDiceRoll (4);
        return outcome;
    }

    public static int roll6Die()
    {
        int outcome = 0;
		outcome = getRandomDiceRoll (6);
        return outcome;
    }

    public static int roll8Die()
    {
        int outcome = 0;
		outcome = getRandomDiceRoll (8);
        return outcome;
    }

    public static int roll10Die()
    {
        int outcome = 0;
		outcome = getRandomDiceRoll (10);
        return outcome;
    }

    public static int rollDie()
    {
        
        int diceRollOutcome = 0;
		diceRollOutcome = getRandomDiceRoll (10);
        Debug.Log("dice roll outcome is = " + diceRollOutcome);
        return diceRollOutcome;
    }

}
