using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;

public class TimeRecord : MonoSingleton<TimeRecord>
{
    private string csv_name = "Story1_";
    private int taskNumber = 1;
    private string id = "1";
    private List<string[]> rowData = new List<string[]>();

    public PopUpManager manager;

    public void Start()
    {
        CreatePlayerCsv(csv_name);
    }

    public void AddTaskNumber()
    {
        taskNumber++;
    }

   /* void Confirm()
    {
        seen.SetActive(false);
        Back(true);
        taskNumber++;
        manager.Confirm();
        manager.SetPhase(QuestionPhase.Is_Fake);
    }

    void Back(bool ok)
    {
        select.SetActive(ok);
        
        score.SetActive(!ok);
    }*/

    public void CreatePlayerCsv(string name)
    {
        List<string[]> rowData = new List<string[]>();

        // Creating First row of titles manually..
        string[] rowDataTemp = new string[6];
        rowDataTemp[0] = "Date";
        rowDataTemp[1] = "TimeFromStartQuestTion";
        rowDataTemp[2] = "ID";
        rowDataTemp[3] = "AnsTime";
        rowDataTemp[4] = "Ans";
        rowDataTemp[5] = "IsSignificant";
        rowData.Add(rowDataTemp);

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));

        string filePath = Application.dataPath + "/CSV/" + UserData.Story + "_" + UserData.Solution+ "_" + UserData.UserID + ".csv";

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.Write(sb);
        outStream.Close();

    }

    private string[] rowDataTemp = new string[6];

    public void SaveRecord(string ID, string Ans, float startTime,bool isSignificant =false)
    {
        rowData = new List<string[]>();

        // Creating First row of titles manually..

        DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
        long unixTime = ((DateTimeOffset)serverTime).ToUnixTimeMilliseconds();

        rowDataTemp[0] = unixTime.ToString();
        rowDataTemp[1] = startTime.ToString();
        rowDataTemp[2] = ID.ToString();
        rowDataTemp[3] = (Time.time - startTime).ToString();
        rowDataTemp[4] = Ans.Replace("\n"," ");
        rowDataTemp[5] = isSignificant.ToString();
        rowData.Add(rowDataTemp);

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));

        print(UserData.Story + " " + UserData.Solution+ "_" + " " + UserData.UserID);
        string filePath = Application.dataPath + "/CSV/" + UserData.Story+"_"+UserData.Solution+ "_" + UserData.UserID + ".csv";

        StreamWriter outStream = System.IO.File.AppendText(filePath);
        outStream.Write(sb);
        outStream.Close();

    }

    void Update()
    {
      
    }


    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + UserData.Story + "_" + UserData.Solution+ "_" + UserData.UserID + ".csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath  + "/CSV/" + UserData.Story + "_" + UserData.Solution+ "_" + UserData.UserID + ".csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath  + "/CSV/" + UserData.Story + "_" + UserData.Solution+ "_" + UserData.UserID + ".csv";
#else
        return  "/CSV/" + UserData.Story + "_" + UserData.Solution+ "_" + UserData.UserID + ".csv";
#endif
        Debug.Log("get path leido");
    }
}
