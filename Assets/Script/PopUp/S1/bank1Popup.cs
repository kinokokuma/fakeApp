using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bank1Popup : BasePopUp
{
    public GameObject bank;
    public GameObject home;
    public GameObject p1;
    public GameObject p2;
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

    public void Tran()
    {
        StartCoroutine(TranCount());
    }

    IEnumerator TranCount()
    {
        p1.SetActive(true);
        yield return new WaitForSeconds(3);
        p2.SetActive(true);
    }

    public void back(string name)
    {
        manager.OpenChat(name);
        gameObject.SetActive(false);
    }
}
