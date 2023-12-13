using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group2Popup : BasePopUp
{
    public string name;
    public GameObject comment;
    public GameObject page1;
    public GameObject go;

    void Start()
    {
        StartCoroutine(Run(comment));
    }

    private IEnumerator Run(GameObject obj)
    {
        yield return new WaitForSeconds(5);
        obj.SetActive(true);
    }

    public void CommentClick1()
    {
        page1.SetActive(true);
        StartCoroutine(Run(go));
    }

    public void chatClick1()
    {
        manager.OpenChat(name);
        gameObject.SetActive(false);
    }
}
