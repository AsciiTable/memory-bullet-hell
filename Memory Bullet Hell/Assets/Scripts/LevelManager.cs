﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

    private Animator animator = null;
    private AudioSource audioSource = null;

    [SerializeField] private int level = 0;
    [Tooltip("0 = Level Select, 1 = Level Intro, 2 = Intro Loop, 3 = Level")]
    [SerializeField] private int levelStage = 0;

    [SerializeField] private int score;

    [Header("Level Music")]
    [SerializeField] private AudioClip[] tutorialMusic = null;
    [SerializeField] private AudioClip[] tutorialLoops = null;
    [SerializeField] private AudioClip[] levelMusic = null;

    private int[] tutorialRequirements = { 1, 6 };


    public int AddScore(int score) { 
        this.score += score;
        return this.score;
    }
    public int GetScore() { return score; }
    private void OnEnable()
    {
        //Singleton
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);

        UpdateHandler.UpdateOccurred += CheckLevelEnded;
    }
    private void OnDisable()
    {
        StopCoroutine(LevelSelect());
        UpdateHandler.UpdateOccurred -= CheckLevelEnded;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
        StartCoroutine(LevelSelect());
    }

    private AudioClip GetMusic()
    {
        //1 - Tutorial Intro
        if (levelStage == 0)
        {
            audioSource.clip = tutorialMusic[level - 1];
            return tutorialLoops[level - 1];
        }
        //2 - Tutorial Loop
        else if (levelStage == 1)
        {
            audioSource.clip = tutorialLoops[level - 1];
            return tutorialMusic[level - 1];
        }
        else if (levelStage == 2)
        {
            audioSource.clip = levelMusic[level - 1];
            return levelMusic[level - 1];
        }
        else
            return null;
        
    }

    //Go to next Level if in Level Select
    private IEnumerator LevelSelect()
    {
        yield return new WaitForSeconds(0.5f);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Level Select"))
        {
            animator.SetInteger("Level", level);
            StartLevel();
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(LevelSelect());
    }

    private void StartLevel()
    {
        Debug.Log("Changing Song");
        animator.SetTrigger("TransitionLevel");
        GetMusic();
        audioSource.Play();
 
    }
    private void RestartLevel()
    {
        animator.StopPlayback();
        animator.Play(0, -1, 0f);
        audioSource.Play();
    }

    private void CheckLevelEnded()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Level Select"))
            return;

        Debug.Log("Animator Time: " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        if (levelStage == 0)
        {
            if (!audioSource.isPlaying)
            {
                levelStage++;
                animator.SetInteger("Stage", levelStage);
                StartLevel();
            }
        } 
        else if (levelStage == 1)
        {
            if (!audioSource.isPlaying)
            {
                if (score >= tutorialRequirements[level - 1])
                {
                    levelStage++;
                    animator.SetInteger("Stage", levelStage);
                    StartLevel();
                }
                else
                    RestartLevel();
            }
        }
        if (levelStage == 2 || levelStage == 3)
        {
            if (!audioSource.isPlaying)
            {
                
            }
        }
    }
}
