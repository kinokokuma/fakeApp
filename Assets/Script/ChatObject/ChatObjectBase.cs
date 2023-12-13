using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatObjectBase : MonoBehaviour
{
    protected ChatDataDetail data;
    protected ChatPopup chatPopup;
    protected PopUpManager manager;
    protected float timeFromStart;
    public virtual void Initialized(ChatDataDetail data, ChatPopup chatPopup, PopUpManager manager)
    {
        timeFromStart = Time.time;
        this.manager = manager;
        this.chatPopup = chatPopup;
        this.data = data;
    }

    protected IEnumerator UpdateLayoutGroup(GameObject reloadObject, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            reloadObject.SetActive(false);
            yield return new WaitForEndOfFrame();
            reloadObject.SetActive(true);
            yield return new WaitForEndOfFrame();
        }
        Canvas.ForceUpdateCanvases();
        yield return new WaitForEndOfFrame();

    }
}
