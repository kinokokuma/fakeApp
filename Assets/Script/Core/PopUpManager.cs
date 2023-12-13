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
    private Transform feedParent;
    [SerializeField]
    private Transform popupParent;
    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private ChatPopup chatPopup;
    [SerializeField]
    private VerticalLayoutGroup layoutGroup;
    [SerializeField]
    private QuestPopUp question;
    private QuestionPhase phase;
    private PostData currentPostPopupData;

    [SerializeField]
    private Dictionary<string, ChatPopup> chatPopUpDic;
    [SerializeField]
    private Dictionary<string, BasePopUp> popUpDic;

    [SerializeField]
    private List<BasePopUp> allPopUpList;
    public QuestionPhase Phase => phase;
    public  PostData CurrentPostPopupData => currentPostPopupData;
    public ChatDataDetail currentChatData;

    public void SetPhase(QuestionPhase phase)
    {
        this.phase = phase;
    }

    public void Awake()
    {
        chatPopUpDic = new Dictionary<string, ChatPopup>();
        popUpDic = new Dictionary<string, BasePopUp>();
        data = new FeedData();
        data = JsonUtility.FromJson<FeedData>(ReadFile("Feed/Class1").ToString());
        OpenChat("Route1/story1-14");
    }

    public TextAsset ReadFile(string fileName)
    {
        return Resources.Load<TextAsset>("Feed/Class1");
    }

    private void Start()
    {
        foreach (var postData in data.PostData)
        {
            var popup = Instantiate(popupPrefab, feedParent);
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

    public void OpenChat(string path)
    {
        ChatData newData = ReadChatData($"Feed/Story1/{path}");
        string id = newData.ID;
        if (chatPopUpDic.ContainsKey(id))
        {
            foreach(BasePopUp popUp in chatPopUpDic.Values)
            {
                popUp.gameObject.SetActive(false);
            }

            chatPopUpDic[id].gameObject.SetActive(true);
            StartCoroutine(chatPopUpDic[id].ShowChat(newData));

        }
        else
        {
            ChatPopup popup = Instantiate(chatPopup, popupParent);
            popup.SetManager(this);
            popup.gameObject.SetActive(true);
            chatPopUpDic[id] = popup;
            popUpDic[id] = popup;
            StartCoroutine(popup.ShowChat(newData));
        }
    }

    public ChatData ReadChatData(string path)
    {
        ChatData data = new ChatData();
        print(path);
        var jsonTextFile = Resources.Load<TextAsset>(path);

        return data = JsonUtility.FromJson<ChatData>(jsonTextFile.ToString());

        //StartCoroutine(ShowChat());

    }

    public void click()
    {
        /* if ()
             ChatPopup popup = Instantiate<chatPopup,>();*/
        OpenChat(currentPostPopupData.TaskType);
        //chatPopup.gameObject.SetActive(true);
        //chatPopup.ReadData(currentPostPopupData.TaskType);
        //chatPopup.Initialized(currentPostPopupData,this);
        //StartCoroutine(CountToStartQuestion());

        

        //currentPostPopupData.IsTask = false;
    }
    public void Confirm()
    {
        currentPostPopupData.IsTask = false;
        question.gameObject.SetActive(false);
        chatPopup.gameObject.SetActive(false);
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

    public BasePopUp FindPopup(string id)
    {
        foreach(var popUp in allPopUpList)
        {
            if(id == popUp.ID)
            {
                return popUp;
            }
        }

        return null;
    }

    public void CreatePopup(string id)
    {
        if (chatPopUpDic.ContainsKey(id))
        {
            foreach (BasePopUp popUp in popUpDic.Values)
            {
                popUp.gameObject.SetActive(false);
            }

            popUpDic[id].gameObject.SetActive(true);

        }
        else
        {
            print(id);
            print(FindPopup(id).ID+"");
            BasePopUp popup = Instantiate(FindPopup(id), popupParent);
            popup.SetManager(this);
            popup.gameObject.SetActive(true);
            popUpDic[id] = popup;
        }
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