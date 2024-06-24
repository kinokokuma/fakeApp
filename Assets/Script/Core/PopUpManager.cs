using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum QuestionPhase
{
    Start,
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
    [SerializeField]
    private GameObject endPopup;
    [SerializeField]
    private TMP_Text endText;

    private QuestionPhase phase;
    private PostData currentPostPopupData;
    private int currentIndex;

    public QuestionPhase Phase => phase;
    public PostData CurrentPostPopupData => currentPostPopupData;

    public void SetPhase(QuestionPhase phase)
    {
        this.phase = phase;
    }

    public void Awake()
    {
        endText.text = UserData.Instance.IsTutorial ? "จบการทดลองเล่น" : "ขอบคุณ";
        currentIndex = 0;
        var jsonTextFile = new TextAsset();
        if (UserData.Instance.IsTutorial)
        {
            jsonTextFile = Resources.Load<TextAsset>("Feed/tutorial");
        }
        else
        {
            if (UserData.Instance.TaskIndex == "game_1")
            {
                jsonTextFile = Resources.Load<TextAsset>("Feed/NewS1");
            }
            else
            {
                jsonTextFile = Resources.Load<TextAsset>("Feed/NewS2");

            }
        }
        data = JsonUtility.FromJson<FeedData>(jsonTextFile.ToString());
        data.PostData = Shuffle(data.PostData);
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

    public void Back()
    {
        SceneManager.LoadScene(1);
    }

    public void click()
    {
        readPopup.gameObject.SetActive(true);
        readPopup.Initialized(currentPostPopupData, this);
        StartCoroutine(CountToStartQuestion());
        //currentPostPopupData.IsTask = false;
    }
    public void Confirm()
    {
        currentPostPopupData.IsTask = false;
        question.gameObject.SetActive(false);
        readPopup.gameObject.SetActive(false);
        currentIndex++;
        if (currentIndex >= data.PostData.Length)
        {
            StartCoroutine(CountToEnd());
        }
    }

    private IEnumerator CountToEnd()
    {
        yield return new WaitForSeconds(5);
        endPopup.SetActive(true);
    }

    private IEnumerator CountToStartQuestion()
    {
        question.viewPoint.sizeDelta = new Vector2(1024, 1366) - new Vector2(0, question.select.sizeDelta.y + 20);
        question.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        //question.gameObject.SetActive(true);
       // question.viewPoint.sizeDelta = new Vector2(1024, 1366) - new Vector2(0, question.select.sizeDelta.y + 20);
    }

    public IEnumerator UpdateLayoutGroup()
    {
        layoutGroup.enabled = false;
        yield return new WaitForEndOfFrame();
        layoutGroup.enabled = true;
    }

    public PostData[] Shuffle(PostData[] data)
    {
        PostData[] newData = new PostData[data.Length];
        List<int> index = new List<int>();
        for (int i = 0; i < data.Length; i++)
        {
            index.Add(i);
        }

        for (int i = 0; i < data.Length; i++)
        {
            int ranIndex = Random.Range(0, index.Count);
            newData[i] = data[index[ranIndex]];
            index.RemoveAt(ranIndex);
        }
        return newData;
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