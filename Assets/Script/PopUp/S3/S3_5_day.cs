using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3_5_day : BasePopUp
{
    // Start is called before the first frame update
    public string nextId;

    public void Awake()
    {
        startTime = Time.time;
        StartCoroutine(Wait());
    }

    public void Back(string id)
    {

        manager.OpenChat(id);
        TimeRecord.Instance.SaveRecord(ID, "กลับแชท", startTime);
        gameObject.SetActive(false);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        Back(nextId);
    }
}
