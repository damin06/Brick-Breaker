using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoretXT : MonoBehaviour
{
    [SerializeField]private float movespeed;
    [SerializeField]private float alphaspeed;
    TextMeshPro text;
    Color alpha;
    public int socre;
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        text.text="+"+socre.ToString();
        Destroy(gameObject,3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0,movespeed * Time.deltaTime,0));
        alpha.a = Mathf.Lerp(alpha.a,0,Time.deltaTime *alphaspeed);
        text.color=alpha;
    }
}
