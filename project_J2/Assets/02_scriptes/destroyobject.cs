using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyobject : MonoBehaviour
{
    [SerializeField]private float destroytime=2;
    void Start()
    {
        Destroy(gameObject,destroytime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
