using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagWebPopup : BasePopUp
{
    [SerializeField]
    private Button backButton;
    private float startTime;
    void Start()
    {
        StartCoroutine(showButton());
        backButton.onClick.AddListener(onClick);
    }

    private IEnumerator showButton()
    {
        yield return new WaitForSeconds(5);
        backButton.gameObject.SetActive(true);
        startTime = Time.time;

    }
    private void onClick()
    {
        manager.OpenChat("Route1/story1-3");
        TimeRecord.Instance.SaveRecord("web-1", "ออกจากเว็บ", startTime);
        gameObject.SetActive(false);
            
    }
}
