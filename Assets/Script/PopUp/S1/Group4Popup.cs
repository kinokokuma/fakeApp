using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group4Popup : BasePopUp
{
    public GameObject comment;
    void Start()
    {
        StartCoroutine(Run(comment));
    }

    private IEnumerator Run(GameObject obj)
    {
        yield return new WaitForSeconds(5);
        obj.SetActive(true);
    }

    public void chatClick1()
    {
        manager.OpenChat("Route1/story1-14");
        gameObject.SetActive(false);
    }
}
