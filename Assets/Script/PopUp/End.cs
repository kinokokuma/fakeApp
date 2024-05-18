using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : BasePopUp
{
    public GameObject[] Image;

    void Start()
    {
        StartCoroutine(Count());
    }

    IEnumerator Count()
    {
        for (int i = 0; i < Image.Length; i++)
        {
            yield return new WaitForSeconds(3);
            Image[i].SetActive(false);
        }

    }
    void Update()
    {
        
    }
}
