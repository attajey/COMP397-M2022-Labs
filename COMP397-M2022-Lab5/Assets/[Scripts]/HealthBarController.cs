using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider bar;
    public TMP_Text healthLabel;
    [Range(0, 100)]
    public int healthValue = 100;

    private int startingHealthValue;
    void Start()
    {
        startingHealthValue = healthValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthValue != startingHealthValue)
        {
            startingHealthValue = healthValue;
            bar.value = healthValue;
            OnValueChanged();
        }
    }

    public void OnValueChanged()
    {
        healthLabel.text = bar.value.ToString();

    }
}
