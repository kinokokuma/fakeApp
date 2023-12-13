using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group5Popup : BasePopUp
{
    public GameObject end;
    public void CommentClick1(GameObject obj)
    {
        obj.SetActive(true);

    }

    public void Close(GameObject obj)
    {
        obj.SetActive(false);

    }

public void End()
    {
        end.SetActive(true);

    }
}
