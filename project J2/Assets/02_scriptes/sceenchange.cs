using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceenchange : MonoBehaviour
{
    private void Awake()
    {

    }
    public void sceen()
    {

        SceneManager.LoadScene("play");
    }
}
