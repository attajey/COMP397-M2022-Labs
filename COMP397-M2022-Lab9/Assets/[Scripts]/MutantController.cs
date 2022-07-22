using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantController : MonoBehaviour
{
    public Animator animatorController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            animatorController.SetInteger("AnimationState", 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            animatorController.SetInteger("AnimationState", 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            animatorController.SetInteger("AnimationState", 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            animatorController.SetInteger("AnimationState", 3);
        }
    }
}
