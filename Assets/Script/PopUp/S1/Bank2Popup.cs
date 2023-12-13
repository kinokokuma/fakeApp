using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank2Popup : BasePopUp
{
    public GameObject bank;
    public GameObject home;

    void Start()
    {

    }

    // Update is called once per frame
    public void OpenBank()
    {
        StartCoroutine(open());
    }

    IEnumerator open()
    {
        bank.SetActive(true);
        yield return new WaitForSeconds(3);
        home.SetActive(true);
    }

    public void go(GameObject next)
    {
        next.SetActive(true);
    }



    public void back(string name)
    {
        manager.OpenChat(name);
        gameObject.SetActive(false);
    }
}
