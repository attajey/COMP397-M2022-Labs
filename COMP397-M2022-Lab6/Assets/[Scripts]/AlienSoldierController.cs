using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSoldierController : MonoBehaviour
{
    public Animator animatorController;
    // Start is called before the first frame update
    void Start()
    {
       // animatorController.GetComponent<>
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animatorController.SetInteger("AnimationState", 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animatorController.SetInteger("AnimationState", 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animatorController.SetInteger("AnimationState", 2);
        }
    }
}
