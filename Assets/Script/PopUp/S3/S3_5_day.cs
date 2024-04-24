using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3_5_day : BasePopUp
{
    // Start is called before the first frame update
    public void Back(string id)
    {

        manager.OpenChat(id);
        TimeRecord.Instance.SaveRecord(ID, "กลับแชท", startTime);
        gameObject.SetActive(false);
    }
}
