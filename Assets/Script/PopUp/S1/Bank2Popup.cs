using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank2Popup : BasePopUp
{
    public GameObject bank;
    public GameObject home;

    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    public void OpenBank()
    {
        StartCoroutine(open());
        TimeRecord.Instance.SaveRecord(ID, "เปิดธนาคาร", startTime);
    }

    IEnumerator open()
    {
        bank.SetActive(true);
        yield return new WaitForSeconds(3);
        startTime = Time.time;
        home.SetActive(true);
    }

    public void go(GameObject next)
    {
        next.SetActive(true);
    }



    public void back(string name)
    {
        manager.OpenChat(name);
        TimeRecord.Instance.SaveRecord(ID, "กลับแชท", startTime, false);
        gameObject.SetActive(false);
    }

    public void Story2()
    {
        UserData.S2Pass = true;
    }
}
