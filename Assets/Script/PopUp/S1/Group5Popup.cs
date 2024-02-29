using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Group5Popup : BasePopUp
{
    public GameObject end;
    public Button[] fake;
    public Button[] real;
    void Start()
    {
        startTime = Time.time;

        foreach (Button butt in fake)
        {
            butt.onClick.AddListener(() => save("กดลิ้งค์ปลอม", true));
        }

        foreach (Button butt in real)
        {
            butt.onClick.AddListener(() => save("กดลิ้งค์จริง", true));
        }
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
