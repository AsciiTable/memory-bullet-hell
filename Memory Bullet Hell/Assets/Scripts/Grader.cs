using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grader : MonoBehaviour
{
    public static Grader GraderInstance;
    public static int MaxScoreInLevel;
    public static int PlayerScoreForLevel;

    private void OnEnable()
    {
        if(GraderInstance == null)
            GraderInstance = this;
        MaxScoreInLevel = 0;
        PlayerScoreForLevel = 0;
    }

    private void Awake()
    {
        if (GraderInstance == null)
        {
            enabled = false;
            GraderInstance = this;
        }
        else if(GraderInstance != this)
        {
            Destroy(this);
        }
    }

    public void ResetScore() {
        MaxScoreInLevel = 0;
        PlayerScoreForLevel = 0;
    }

    public void AddToMaxScore(int v) {
        MaxScoreInLevel += v;
    }

    public void AddToPlayerScore(int v) {
        PlayerScoreForLevel += v;
    }

    public float GetFloatGrade() {
        return (float)PlayerScoreForLevel / (float)MaxScoreInLevel * 100.0f;
    }

    public string GetLetterGrade() {
        float grd = GetFloatGrade();
        if (grd >= 100)
            return "A+";
        else if (grd >= 90)
            return "A";
        else if (grd >= 80)
            return "B";
        else if (grd >= 70)
            return "C";
        else if (grd >= 60)
            return "D";
        return "F";
    }
}
