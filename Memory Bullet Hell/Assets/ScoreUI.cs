﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public Text floatscore;
    public Text LetterScore;

    private void Start()
    {
        floatscore.text = "" + Grader.GraderInstance.GetFloatGrade() + "%";
        LetterScore.text = "" + Grader.GraderInstance.GetLetterGrade();
    }

    public void returnToMain()
    {
        SceneManager.LoadScene(0);
    }
}
