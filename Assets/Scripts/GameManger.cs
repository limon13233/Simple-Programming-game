using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public int Score;
    public int Length_alg;
    public int maxScore;
    public int maxLength;
    public int Star=1;
    public Image star1;
    public Image star2;
    public void GetStars()
    {
        if (Score == maxScore)
            star1.color=Color.white;
        if (Length_alg == maxLength || Length_alg < maxLength)
            star2.color = Color.white;

    }
    
}
