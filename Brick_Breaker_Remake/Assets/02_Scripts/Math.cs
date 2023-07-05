using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Math : MonoBehaviour
{
    int Level;
    int levelcoast = 100;
    int addtioncoast = 25;
    [SerializeField] TextMeshProUGUI costTXT1;
    [SerializeField] TextMeshProUGUI costTXT2;
    [SerializeField] TextMeshProUGUI costTXT3;

    int a, b, c;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    int coast;
    public void Calculate(int step)
    {

        if (step == 1)
        {
            coast = levelcoast + addtioncoast * (Level - 1);
        }
        else
        {
            coast = (((levelcoast + addtioncoast * (Level + step - 1) + levelcoast) * (Level + step)) / 2)
                        - (((levelcoast + addtioncoast * (Level - 1) + levelcoast) * Level) / 2);
        }

        costTXT3.text = coast.ToString();
    }

    public void CalculateButton()
    {

    }
}
