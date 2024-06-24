using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PostPopup : BasePopUp
{
    private const string moreText = "อ่านเพิ่มเติม";

    [SerializeField]
    private TMP_Text userName;
    [SerializeField]
    private TMP_Text userTime;
    [SerializeField]
    private Image bg;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text description;
    [SerializeField]
    private TMP_Text likeText;
    [SerializeField]
    private TMP_Text commentText;
    [SerializeField]
    private Image postImage;
    private PostData postData;
    [SerializeField]
    private PostPopup commentPrefab;
    [SerializeField]
    private Transform commentParent;
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private RectTransform scrollRectTransform;
    [SerializeField]
    private Button click;
    [SerializeField]
    private GameObject layout;
    [SerializeField]
    private AspectRatioFitter imageRatio;

    public Image Icon => icon;
    public Image BG => bg;
    PopUpManager json;
    
    public void Initialized(PostData data,PopUpManager json,bool isHintDescription = true)
    {
        this.json = json;
        description.text = data.Description;
        userTime.text = data.Time;
        likeText.text = data.LikeCount.ToString();
        commentText.text = $"ความคิดเห็น {data.CommentCount} รายการ   แชร์ {data.ShereCount} ครั้ง";

        if (isHintDescription)
        {
            if (data.Description.Length > 200)
            {
                char[] textData = data.Description.ToCharArray();
                string resultText = "";
                for (int i = 0; i < 200; i++)
                {
                    resultText += textData[i];
                }

                description.text = $"{resultText}...";
            }
        }

        var texture = Resources.Load<Texture2D>($"Image/Icon/{data.Icon}");
        icon.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        userName.text = data.Name;
        postData = data;
        if (data.PostImage != string.Empty)
        {
            var poseTexture = Resources.Load<Texture2D>($"Image/PostImage/{data.PostImage}");
            postImage.gameObject.SetActive(true);
            postImage.sprite = Sprite.Create(poseTexture, new Rect(0.0f, 0.0f, poseTexture.width, poseTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
            print(poseTexture.width / poseTexture.height);
            imageRatio.aspectRatio = (float)poseTexture.width/poseTexture.height;
        }
        else
        {
            postImage.gameObject.SetActive(false);
        }

        /*if (postData.CommentData != null)
        {
            foreach (var commentData in postData.CommentData)
            {
                var popup = Instantiate(commentPrefab, commentParent);
                popup.gameObject.SetActive(true);
                popup.Initialized(commentData, json);
            }
        }*/

        if (click != null)
        {
            click.onClick.AddListener(() => 
            {
                json.click();
                TimeRecord.Instance.SaveRecord("readnews");
            });
        }

        StartCoroutine(json.UpdateLayoutGroup());
        StartCoroutine(UpdateLayoutGroup(layout, 2));
        print(postData);
    }

    public void ShowAllText(GameObject button)
    {
        if (!postData.IsTask)
        {
            description.text = postData.Description;
            button.SetActive(false);
            StartCoroutine(UpdateLayoutGroup(layout, 2));
        }
    }

    public void Update()
    {
        if (rectTransform != null && postData!=null)
        {
            if (scrollRectTransform.anchoredPosition.y + rectTransform.anchoredPosition.y >= -600 && scrollRectTransform.anchoredPosition.y + rectTransform.anchoredPosition.y < rectTransform.sizeDelta.y - 30)
            {
                if (postData != null)
                {
                    json.SetCurrentData(postData);
                }

                if (click != null)
                {
                    if (postData.IsTask)
                    {
                        click.gameObject.SetActive(true);
                        StartCoroutine(UpdateLayoutGroup(layout));

                    }
                    else
                    {
                        click.gameObject.SetActive(false);
                    }
                }
            }
            else
            {//unread
               // BG.color = Color.black;
                if (click != null)
                {
                    click.gameObject.SetActive(false);
                }
            }
        }
    }
}
