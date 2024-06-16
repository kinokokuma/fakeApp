using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum QuestionPhase
{
    Is_Fake,
    Level_Of_Confident,
    Have_seen,

}

public class PopUpManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private FeedData data;
    [SerializeField]
    private PostPopup popupPrefab;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private ReadPopup readPopup;
    [SerializeField]
    private VerticalLayoutGroup layoutGroup;
    [SerializeField]
    private QuestPopUp question;
    private QuestionPhase phase;
    private PostData currentPostPopupData;

    public QuestionPhase Phase => phase;
    public  PostData CurrentPostPopupData => currentPostPopupData;

    public void SetPhase(QuestionPhase phase)
    {
        this.phase = phase;
    }

    public void Awake()
    {
        data = new FeedData();
        var jsonTextFile = Resources.Load<TextAsset>("Feed/NewS1");
        print(jsonTextFile);
        data = JsonUtility.FromJson<FeedData>(jsonTextFile.ToString());
    }
    private void Start()
    {
        foreach (var postData in data.PostData)
        {
            var popup = Instantiate(popupPrefab, parent);
            popup.gameObject.SetActive(true);
            popup.Initialized(postData, this);
        }
    }

    public void SetCurrentData(PostData data)
    {
        currentPostPopupData = data;
    }
    public void Update()
    {
        if (currentPostPopupData != null)
        {
            if (currentPostPopupData.IsTask)
            {
                scrollRect.enabled = false;
            }
            else
            {
                scrollRect.enabled = true;
            }
        }
    }

    public void click()
    {
        readPopup.gameObject.SetActive(true);
        readPopup.Initialized(currentPostPopupData,this);
        StartCoroutine(CountToStartQuestion());
        //currentPostPopupData.IsTask = false;
    }
    public void Confirm()
    {
        currentPostPopupData.IsTask = false;
        question.gameObject.SetActive(false);
        readPopup.gameObject.SetActive(false);
    }

    private IEnumerator CountToStartQuestion()
    {
        yield return new WaitForSeconds(5);
        question.gameObject.SetActive(true);
        question.viewPoint.sizeDelta = new Vector2(1024, 1366) - new Vector2(0, question.select.sizeDelta.y + 20);
    }

    public IEnumerator UpdateLayoutGroup()
    {
        layoutGroup.enabled = false;
        yield return new WaitForEndOfFrame();
        layoutGroup.enabled = true;
    }

}
[System.Serializable]
public class FeedData
{
    public PostData[] PostData;
}
[System.Serializable]
public class PostData
{
    public string Name;
    public string ID;
    public string Icon;
    public string Description;
    public string PostImage;
    public string Time;
    public int LikeCount;
    public int CommentCount;
    public int ShereCount;
    public bool IsTask;
    public string TaskType;
    public PostData[] CommentData;
}