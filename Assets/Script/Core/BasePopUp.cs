using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopUp : MonoBehaviour
{
    protected IEnumerator UpdateLayoutGroup(GameObject reloadObject, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            reloadObject.SetActive(false);
            yield return new WaitForEndOfFrame();
            reloadObject.SetActive(true);
            yield return new WaitForEndOfFrame();
        }
    }
}
