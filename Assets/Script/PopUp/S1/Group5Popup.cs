using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group5Popup : BasePopUp
{
    public GameObject end;

    void Start()
    {
        startTime = Time.time;
    }

    public void CommentClick1(GameObject obj)
    {
        obj.SetActive(true);

        TimeRecord.Instance.SaveRecord(ID, "ดูคอมเม้น", startTime);
        startTime = Time.time;
    }

    public void Close(GameObject obj)
    {
        obj.SetActive(false);
        TimeRecord.Instance.SaveRecord(ID, "ปิด popup ข้อมูล", startTime);
        startTime = Time.time;
    }

    public void End()
    {
        end.SetActive(true);

    }
}
