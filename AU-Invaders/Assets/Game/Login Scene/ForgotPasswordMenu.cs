using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;


public class ForgotPasswordMenu : MonoBehaviour
{
    public GameObject usernameoremail;
    public GameObject ForgotPasswordMenuUI;
    public GameObject LoginMenuUI;
    public GameObject user_display;

    private string UsernameOrEmail;
    private  bool _dataRecieved = false;
    private RecievedForgotData _rfd;    

    public ForgotPasswordMenu()
    {
        WebSocketClient.OnMessageEvent += this.OnMessage;
    }

    private void OnMessage(object sender, MessageEventArgs e)
    {
        _rfd = new RecievedForgotData();
        _rfd = JsonUtility.FromJson<RecievedForgotData>(e.Data);
        _dataRecieved = true;
    }

    [Serializable]
    public class RecievedForgotData { public string Message; public int Code; }

    public bool ValidateUsernameorEmail(string UsernameOrEmail)
    {
        if (UsernameOrEmail == "")
        {
            user_display.GetComponent<Text>().text = "Field is empty";
            user_display.GetComponent<Text>().color = Color.red;
            return false;
        }

        user_display.GetComponent<Text>().text = "";
        return true;
    }

    public bool Forgot(){
        bool succes = false;

        do{ } while(!_dataRecieved);

        switch(_rfd.Message)
        {
            case "OK":
            {
                print("Email send");
                usernameoremail.GetComponent<InputField>().text = "";
                user_display.GetComponent<Text>().text = "";
                succes = true;
                break;
            }

            default:
            {
                print(_rfd.Message);
                user_display.GetComponent<Text>().text = "Unknown error!";
                user_display.GetComponent<Text>().color = Color.red; 
                _dataRecieved = false;
                break;
            }
        }
        return succes;
    }

    public void SendButton()
    {
        bool unameoremailValidate = false;
        var s = false;

        unameoremailValidate = ValidateUsernameorEmail(UsernameOrEmail);

        if(unameoremailValidate)
        {
            ServerCommandEmail ScE = new ServerCommandEmail("Forgot password", UsernameOrEmail);
        

            WebSocketClient.Start();
            WebSocketClient.Send(JsonUtility.ToJson(ScE));

            s = Forgot();
        }
        
        if(s)
        {
            ForgotPasswordMenuUI.SetActive(false);
            LoginMenuUI.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (UsernameOrEmail != "")
            {
                SendButton();
            }
        }
    }

    public void GoBackToLogin()
    {
        //SceneManager.LoadScene(1);
        ForgotPasswordMenuUI.SetActive(false);
        LoginMenuUI.SetActive(true);
    }
}

[Serializable]
public class ServerCommandEmail
{
    public string Command;
    public string UsernameOrEmail;

    public ServerCommandEmail(string command, string user)
    {
        Command = command;
        UsernameOrEmail = user;
    }
}