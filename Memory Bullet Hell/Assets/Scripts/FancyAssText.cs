using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FancyAssText : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void fadeInText() {
        anim.SetBool("visible", true);
    }

    public void fadeOutText() {
        anim.SetBool("visible", false);
    }
}
