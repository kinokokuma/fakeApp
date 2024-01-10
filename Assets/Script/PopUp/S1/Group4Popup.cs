using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group4Popup : BasePopUp
{
    private float startTime;
    public GameObject comment;
    public string path;
    void Start()
    {
        StartCoroutine(Run(comment));
    }

    private IEnumerator Run(GameObject obj)
    {
        yield return new WaitForSeconds(5);
        startTime = Time.time;
        obj.SetActive(true);
    }

    public void chatClick1()
    {
        manager.OpenChat(path);
        TimeRecord.Instance.SaveRecord("group-3", "แชทกับร้านปลอม", startTime);
        gameObject.SetActive(false);
    }
}
