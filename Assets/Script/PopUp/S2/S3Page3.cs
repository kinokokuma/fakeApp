using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3Page3 : BasePopUp
{
    public void Start()
    {
        startTime = Time.time;
    }
    public void Open(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Close(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void OpenCount(GameObject obj)
    {
        StartCoroutine(Count(obj));
    }

    IEnumerator Count(GameObject obj)
    {
        yield return new WaitForSeconds(3);
        obj.SetActive(true);
    }
    public void back(string name)
    {
        manager.OpenChat(name);
        TimeRecord.Instance.SaveRecord(ID, "กลับแชท", startTime);
        gameObject.SetActive(false);
    }
}
