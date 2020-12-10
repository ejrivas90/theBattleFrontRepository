using UnityEngine;
using System;
using System.Collections;

public static class DiceRoll {
    private static System.Random random = new System.Random();

    /*
    public int clicked()
    {
        int diceRollOutcome = 0;
        diceRollOutcome = random.Next(1, 11);
        Debug.Log("dice roll outcome is = " + diceRollOutcome);
        return diceRollOutcome;
    }
    */

    public static int roll4Die()
    {
        int outcome = 0;
        outcome = random.Next(1, 5);
        return outcome;
    }

    public static int roll6Die()
    {
        int outcome = 0;
        outcome = random.Next(1, 7);
        return outcome;
    }

    public static int roll8Die()
    {
        int outcome = 0;
        outcome = random.Next(1, 9);
        return outcome;
    }

    public static int roll10Die()
    {
        int outcome = 0;
        outcome = random.Next(1, 11);
        return outcome;
    }
}
