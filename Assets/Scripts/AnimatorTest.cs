using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTest : MonoBehaviour
{
    public Animator animator;

    public string triggerToPlay = "FlyBool";
    public KeyCode keyToTrigger = KeyCode.A;
    //public KeyCode keyToExit = KeyCode.D;

    private void Update()
    {
        if (Input.GetKeyDown(keyToTrigger))
        {
            animator.SetBool(triggerToPlay, !animator.GetBool(triggerToPlay));
        }
    }

    private void OnValidate()
    {
        if (animator == null) animator = GetComponent<Animator>();
    }

}
