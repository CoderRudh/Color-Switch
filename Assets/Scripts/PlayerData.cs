using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int starsCollected;
    public int bestScore;

    public PlayerData(int stars, int score)
    {
        starsCollected = stars;
        bestScore = score;
    }
}
