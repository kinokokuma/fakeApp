using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group2Popup : BasePopUp
{
    public string name;
    public GameObject comment;
    public GameObject page1;
    public GameObject go;
    private float startTime;
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

    public void CommentClick1()
    {
        page1.SetActive(true);
        StartCoroutine(Run(go));
    }

    public void save(string comment)
    {
        TimeRecord.Instance.SaveRecord("group-2", comment, startTime);
    }

    public void chatClick1()
    {
        manager.OpenChat(name);
        gameObject.SetActive(false);

    }
}
