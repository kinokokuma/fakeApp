using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserData : MonoBehaviour
{
    public static string UserID;
    public static string Story;
    public static string Solution;
    public static string UserName;
    public static string UserSex;

    public static int Story1PostIndex;

    public Button next;
    public TMP_InputField inputID;
    public TMP_Dropdown dropdownStory;
    public TMP_Dropdown dropdownSolution;

    public Button nextPage;
    public TMP_InputField inputName;
    public TMP_Dropdown dropdownSex;

    public TMP_Text type;

    public GameObject User;
    public TMP_Text des;

    private string story1Des = "ในเกมนี้ ผู้เล่นจะได้รับบทเป็นตัวละครที่มีชื่อจริงว่า จรินทร ศรประสงค์ ผู้ที่กำลังต้องการค้นหากระเป๋ารุ่นหายาก เพื่อที่จะนำมาเป็นของขวัญในวันเกิดให้แก่แม่ของเขา\nผู้เล่นจะได้รับคำปรึกษาและความช่วยเหลือ\nจากเพื่อน 2 คน คือ ภู และ จอม\nนอกจากนี้ ผู้เล่นยังมีรักทางไกลกับแฟนที่เรียนอยู่ต่างประเทศชื่อซี\nซึ่งได้รู้จักกันผ่านทางแอปหาคู่\nและคบกันมาเป็นเวลา 3 เดือน โดยการทำธุรกรรมทางการเงินของผู้เล่นในเนื้อเรื่อง\nจะดำเนินการผ่านธนาคารกุ้งไทย ";
    private string story2Des = "ในเกมนี้ ผู้เล่นจะได้รับบทเป็นตัวละครที่มีชื่อจริงว่า\nวรินทร ไมตรีมิตร ซึ่งกำลังประสบปัญหาทางด้านการเงินทำให้ส่งผลกระทบในหลายด้าน\nหนึ่งในนั้นคือเรื่องธุรกิจไลฟ์ขายเสื้อผ้าเล็กๆ\nที่กำลังทำกับเพื่อนอีกสองคน ชื่อ นัด และ เจ\nผู้เล่นมีเงินไม่เพียงพอต่อการจ่ายเงินลงทุน\nค่าเสื้อผ้าล็อตใหม่\nแถมโทรศัพท์ที่จำเป็นต้องใช้ในการไลฟ์ก็ใกล้พัง\nโดยการทำธุรกรรมทางการเงินของผู้เล่นในเนื้อเรื่อง\nจะดำเนินการผ่านธนาคารกุ้งไทย";
    private string story3Des = "ในเกมนี้ ผู้เล่นจะได้รับบทเป็นตัวละครที่มีชื่อจริงว่า \nกรรวี มีรุ่งเรือง ผู้เล่นจะมีกลุ่มแชทกับเพื่อนอีก2คน ชื่อว่า ตรี และ เอ ซึ่งทั้งผู้เล่นและเพื่อนเป็นคนชอบทำบุญและรักสัตว์\nเหมือนกัน ตอนนี้ผู้เล่นและเพื่อนกำลังวางแผนทริปทำบุญกันอยู่ โดยการทำธุรกรรมทางการเงินของผู้เล่นในเนื้อเรื่องจะ\nดำเนินการผ่านธนาคารกุ้งไทย";

    public void Start()
    {
        next.onClick.AddListener(() => { User.SetActive(true); });
    }

    public void Update()
    {

        if (inputID.text == string.Empty)
        {
            next.interactable = false;
        }
        else
        {
            next.interactable = true;
        }
        UserID = inputID.text;
        Solution = dropdownSolution.captionText.text;
        Story = dropdownStory.captionText.text;
        type.text = Solution.Split('_')[1];
        if (inputName.text == string.Empty)
        {
            nextPage.interactable = false;
        }
        else
        {
            nextPage.interactable = true;
        }
        UserName = inputName.text;
        UserSex = dropdownSex.captionText.text;
        print(UserData.Story + " " + UserData.Solution + " " + UserData.UserID + " " + UserData.UserSex);

        if (Story == "Story1")
        {
            des.text = story1Des;
        }
        else if(Story == "Story2")
        {
            des.text = story2Des;
        }
        else if (Story == "Story3")
        {
            des.text = story3Des;
        }
    }
}
