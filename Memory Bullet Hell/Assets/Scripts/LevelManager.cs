using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private float level = 0;

    private Animator animator = null;

    private void OnDisable()
    {
        StopCoroutine(StartLevel());
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartLevel());
    }
    private IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(0.5f);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Level Select"))
        {
            level += 0.5f;
            animator.SetFloat("Level", level);
            animator.SetTrigger("StartLevel");
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(StartLevel());
    }
}
