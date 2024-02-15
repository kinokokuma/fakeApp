using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class ChatPopup : BasePopUp
{
    private const int chatSizeOffset = 285;
    private ChatData data;
    private int chatIndex;
    private string[] emoji = { "<sprite=0>", "<sprite=1>", "<sprite=2>", "<sprite=3>", "<sprite=4>", "<sprite=5>" };

    [SerializeField]
    private Button allChatButton;
    [SerializeField]
    private GameObject allChatObject;
    [SerializeField]
    private TMP_Text userInputText;
    [SerializeField]
    private TMP_Text headerName;
    [SerializeField]
    private Image[] headerIcon;
    [SerializeField]
    private Image[] headerIconMask;
    [SerializeField]
    private ChatObjectBase chatobject;
    [SerializeField]
    private RectTransform chatViewPoint;
    [SerializeField]
    private RectTransform chatParent;
    [SerializeField]
    private Transform imageChoiceParent;
    [SerializeField]
    private Transform QuestionObject;
    [SerializeField]
    private ChatChoice imageChoice;
    [SerializeField]
    private ScrollRect chatParentScrollRect;
    [SerializeField]
    private GameObject reloadObj;
    [SerializeField]
    private RectTransform bottomChat;
    [SerializeField]
    public List<ChatObjectBase> chatObject;
    public GameObject allChatGuildlind;
    private bool isReload;

    private List<ChatChoice> choiceList = new List<ChatChoice>();
    private float timeToShowQuestion;
    private bool isFirstTime = true;
    private int oldIndex = 0;
    public GameObject textGuild;
    public GameObject imageGuild;
    public GameObject blockText;
    public Image header;
    public Image headerImage;
    public void Start()
    {
        allChatButton.interactable = false;
           chatObject = new List<ChatObjectBase>();
        allChatButton.onClick.AddListener(ShowChatList);
        allChatGuildlind.SetActive(false);
        //ReadData();
    }

    private void ShowChatList()
    {
        TimeRecord.Instance.SaveRecord(ID, $"เปิดแชททั้งหมด", manager.timeToClickChat);
        manager.ShowAllChat();
        allChatButton.interactable = false;
        allChatGuildlind.SetActive(false);
    }



    public IEnumerator ShowChat(ChatData data,bool muteSound)
    {
        oldIndex = 0;
        chatIndex = 0;
        this.data = data;
        int contextCharecterIndex = 0;
        ID = data.ID;
        if (isFirstTime)
        {
            print("xxxxxx");
            headerName.text = data.ChatName;
            print(data.ChatName);
            if (data.Icon.Length >=2)
            {
                headerIcon[0].sprite = ImageManager.Instance.LoadImage(data.Icon[0]);
                headerIcon[1].sprite = ImageManager.Instance.LoadImage(data.Icon[1]);
            }
            else if(data.Icon != null)
            {
                headerIcon[1].sprite = ImageManager.Instance.LoadImage(data.Icon[0]);
                headerIconMask[0].gameObject.SetActive(false);
            }

            if(data.Header != null)
            {
                headerImage.sprite = ImageManager.Instance.LoadImage(data.Header);
                header.gameObject.SetActive(true);

            }

            isFirstTime = false;
        }

        bool haveQuestion =false;

        while (chatIndex < data.DataDetail.Length)
        {
            if (oldIndex != chatIndex || oldIndex == 0)
            {
                if (data.DataDetail[chatIndex].ChoiceImage != null)
                {
                    if (data.DataDetail[chatIndex].ChoiceImage.Length > 0)
                    {
                        for (int i = 0; i < data.DataDetail[chatIndex].ChoiceImage.Length; i++)
                        {
                            timeToShowQuestion = Time.time;
                            imageGuild.SetActive(true);
                            ChatChoice choice = Instantiate(imageChoice, imageChoiceParent);
                            choice.gameObject.SetActive(true);
                            choice.InitializedImage(i, ChoiceType.Image, data.DataDetail[chatIndex].ChoiceImage[i]);
                            choice.Button.onClick.AddListener(() => OnClickImageChoice(choice.DataImage));
                            choiceList.Add(choice);
                        }
                        QuestionObject.gameObject.SetActive(true);
                        chatViewPoint.sizeDelta = new Vector2(chatViewPoint.sizeDelta.x, chatViewPoint.sizeDelta.y - chatSizeOffset);
                        haveQuestion = true;
                    }
                }
                else if (data.DataDetail[chatIndex].Choice != null)
                {
                    if (data.DataDetail[chatIndex].Choice.Length > 0)
                    {
                        CreateLike();
                        for (int i = 0; i < data.DataDetail[chatIndex].Choice.Length; i++)
                        {
                            timeToShowQuestion = Time.time;
                            textGuild.SetActive(true);
                            ChatChoice choice = Instantiate(imageChoice, imageChoiceParent);
                            choice.gameObject.SetActive(true);
                            choice.InitializedText(i, ChoiceType.String, data.DataDetail[chatIndex].Choice[i]);
                            choice.Button.onClick.AddListener(() => OnClickTextChoice(choice.DataText, data.DataDetail[data.DataDetail.Length - 1]));

                            choiceList.Add(choice);
                        }
                        QuestionObject.gameObject.SetActive(true);
                        chatViewPoint.sizeDelta = new Vector2(chatViewPoint.sizeDelta.x, chatViewPoint.sizeDelta.y - chatSizeOffset);
                        haveQuestion = true;
                    }
                }
                else
                {

                    if (data.DataDetail[chatIndex].OnwerName == "my" && data.DataDetail[chatIndex].DelayTime !=0)
                    {

                        if (oldIndex != chatIndex || oldIndex == 0)
                        {
                            for (contextCharecterIndex = 0; contextCharecterIndex < data.DataDetail[chatIndex].Content.Length; contextCharecterIndex++)
                            {
                                userInputText.text += data.DataDetail[chatIndex].Content[contextCharecterIndex];
                                yield return new WaitForSeconds(0.05f);
                            }
                            

                            ChatObjectBase chat = Instantiate(chatobject);
                            chat.gameObject.SetActive(true);
                            chat.Initialized(data.DataDetail[chatIndex], this, manager, muteSound);
                            chatObject.Add(chat);
                            chat.gameObject.transform.SetParent(chatParent);
                            oldIndex = chatIndex;
                            userInputText.text = string.Empty;
                            //Reload();
                        }
                    }
                    else
                    {
                        ChatObjectBase chat = Instantiate(chatobject, chatParent);
                        chat.gameObject.SetActive(true);

                        chat.Initialized(data.DataDetail[chatIndex], this, manager, muteSound);
                        chatObject.Add(chat);
                        oldIndex = chatIndex;
                        oldIndex = chatIndex;
                        //Reload();
                    }

                }
                StartCoroutine(UpdateLayoutGroup(reloadObj));
                //Reload();





            }
            if (haveQuestion) break;

            yield return new WaitForSeconds(data.DataDetail[chatIndex++].DelayTime);

            

        }

        yield return new WaitForEndOfFrame();
       // Canvas.ForceUpdateCanvases();
        Canvas.ForceUpdateCanvases();
        StartCoroutine(UpdateLayoutGroup(reloadObj,2));

        yield return new WaitForSeconds(5);

        if (!haveQuestion && data.DataDetail[chatIndex-1].ChatType == "Normal")
        {
            manager.NextFileName = data.DataDetail[data.DataDetail.Length - 1].FileName;
            if (data.DataDetail[chatIndex - 1].LinkType == "chat")
            {
                ChatData newData = manager.ReadChatData($"Feed/Story1/{data.DataDetail[data.DataDetail.Length - 1].FileName}");
                print("check ID : "+ID + " " + newData.ID);
                if (newData.ID != ID)
                {
                    manager.timeToClickChat = Time.time;
                    allChatButton.interactable = true;
                    allChatGuildlind.SetActive(true);
                    manager.NextChatID = data.DataDetail[data.DataDetail.Length - 1].ID;
                    if (!muteSound)
                    {
                        SoundManager.Instance.PlaySound(SoundID.newChat);
                    }
                }
                else
                {
                    print(data.DataDetail[data.DataDetail.Length - 1].FileName);
                    manager.OpenChat(data.DataDetail[data.DataDetail.Length - 1].FileName);
                }
            }
            else if (!haveQuestion && data.DataDetail[chatIndex - 1].LinkType == "SP1")
            {
                manager.SP1Button.SetActive(true);
                Button b = manager.SP1Button.GetComponent<Button>();
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(() => manager.OnclickOgpage(manager.SP1Button, (data.DataDetail[data.DataDetail.Length - 1].FileName)));
            }
            else
            {
                manager.gopageButton.SetActive(true);
                Button b = manager.gopageButton.GetComponent<Button>();
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(() => manager.OnclickOgpage(manager.gopageButton, (data.DataDetail[data.DataDetail.Length - 1].FileName)));
            }

            if(data.DataDetail[chatIndex - 1].ID == "block")
            {
                blockText.SetActive(true);
                bottomChat.gameObject.SetActive(false);
            }

        }
    }


    public void CreateLike()
    {
        ChatObjectBase chat = Instantiate(chatobject);
        chat.gameObject.SetActive(true);

        ChatDataDetail dataDetail = new ChatDataDetail();
        chatObject.Add(chat);
        dataDetail.OnwerName = "my";
        dataDetail.Icon = string.Empty;
        dataDetail.Content = "";
        dataDetail.PostImage = "Image/Like";


        chat.Initialized(dataDetail, this, manager);
        chatObject.Add(chat);
        chat.gameObject.transform.SetParent(chatParent);
    }

    
    int countLoad = 0;

    private void LateUpdate()
    {

        if (chatParent.sizeDelta.y <= 1134)
        {
            chatParent.pivot = new Vector2(chatParent.pivot.x, 1);
        }
        else
        {
            chatParent.pivot = new Vector2(chatParent.pivot.x, 0);
        }
        /* if (isReload)
         {
             chatParent.anchoredPosition = new Vector2(chatParent.anchoredPosition.x, chatParent.sizeDelta.y);
             countLoad++;
             //if (countLoad > 4)
             {
                 isReload = false;
                 countLoad = 0;
             }
         }*/
        if (manager != null)
        {
            if (manager.allChat.active)
            {
                bottomChat.sizeDelta = new Vector2(635, bottomChat.sizeDelta.y);
            }
            else
            {
                bottomChat.sizeDelta = new Vector2(1024, bottomChat.sizeDelta.y);
            }
        }
    }

    private void OnClickTextChoice(ChoiceText choiceText, ChatDataDetail data)
    {
        HintChoice();
        ChatObjectBase chat = Instantiate(chatobject, chatParent);
        chat.gameObject.SetActive(true);
        ChatDataDetail dataDetail = new ChatDataDetail();
        chatObject.Add(chat);
        dataDetail.OnwerName = "my";
        dataDetail.Icon = string.Empty;
        dataDetail.Content = data.Content.Replace("{0}", choiceText.Path);

        dataDetail.PostImage = string.Empty;
        dataDetail.LinkType = choiceText.LinkType;
        dataDetail.FileName = choiceText.FileName;

        TimeRecord.Instance.SaveRecord(choiceText.ID, choiceText.Path, timeToShowQuestion);

        chat.Initialized(dataDetail, this, manager);
        QuestionObject.gameObject.SetActive(false);


        chatViewPoint.sizeDelta = new Vector2(chatViewPoint.sizeDelta.x, chatViewPoint.sizeDelta.y + chatSizeOffset);

        if ((int)chatParent.sizeDelta.y >= (int)chatParent.anchoredPosition.y)
        {
            isReload = true;
        }
        Canvas.ForceUpdateCanvases();
        print(dataDetail.FileName);
        if (choiceText.LinkType == "chat")
        {
            manager.OpenChat(dataDetail.FileName);
        }
        else if(choiceText.LinkType != null)
        {
            manager.CreatePopup(choiceText.FileName);
        }
        StartCoroutine(UpdateLayoutGroup(reloadObj));
    }

    private void HintChoice()
    {
        imageGuild.SetActive(false);
        textGuild.SetActive(false);
        foreach (var choice in choiceList)
        {
            choice.gameObject.SetActive(false);
        }
    }

    private void OnClickImageChoice(ChoiceImage choiceImage)
    {
        HintChoice();
        ChatObjectBase chat = Instantiate(chatobject, chatParent);
        chat.gameObject.SetActive(true);
        ChatDataDetail dataDetail = new ChatDataDetail();
        chatObject.Add(chat);
        dataDetail.OnwerName = "my";
        dataDetail.Icon = string.Empty;
        dataDetail.Content = string.Empty;
        dataDetail.PostImage = choiceImage.Path;
        dataDetail.LinkType = choiceImage.LinkType;
        dataDetail.FileName = choiceImage.FileName;
        chat.Initialized(dataDetail, this, manager);
        QuestionObject.gameObject.SetActive(false);

        TimeRecord.Instance.SaveRecord(choiceImage.ID, Path.GetFileName(choiceImage.Path), timeToShowQuestion);

        chatIndex = 0;

        chatViewPoint.sizeDelta = new Vector2(chatViewPoint.sizeDelta.x, chatViewPoint.sizeDelta.y + chatSizeOffset);

        if ((int)chatParent.sizeDelta.y >= (int)chatParent.anchoredPosition.y)
        {
            isReload = true;
        }
        Canvas.ForceUpdateCanvases();
        manager.OpenChat(choiceImage.FileName);
        manager.NextFileName = choiceImage.FileName;
        StartCoroutine(UpdateLayoutGroup(reloadObj));
    }

    public void ClearChat()
    {
        print("Button");
        if (chatObject.Count > 0)
        {
            print("Button");
            foreach (var chat in chatObject)
            {
                Destroy(chat.gameObject);
            }
            chatObject.Clear();
        }
    }
}
