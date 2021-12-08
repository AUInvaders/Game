using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;

public class LoginMenu : MonoBehaviour
{
    public GameObject usernameoremail;
    public GameObject password;
    public GameObject user_display;
    public GameObject pass_display;

    private string UsernameorEmail;
    private string Password;
    private string[] Lines;
    private  bool _dataRecieved = false;
    private RecievedData _rd;
    static string TokenResponse = "";

    public LoginMenu()
    {
        WebSocketClient.OnMessageEvent += this.OnMessage;
    }

    private void OnMessage(object sender, MessageEventArgs e)
    {
        _rd = new RecievedData();
        _rd = JsonUtility.FromJson<RecievedData>(e.Data);
        _dataRecieved = true;
    }

    [Serializable]
    public class RecievedData { public string Message; public int Code; public string Token; }

    //Validering af Username
    public bool ValidateUsernameorEmail(string UsernameorEmail)
    {
        if (UsernameorEmail == "")
        {
            user_display.GetComponent<Text>().text = "Field is empty";
            user_display.GetComponent<Text>().color = Color.red;
            return false;
        }

        user_display.GetComponent<Text>().text = "";
        return true;
    }

    //Validering af Password
    public bool ValidatePassword(string Password)
    {
        if (Password == "")
        {
            pass_display.GetComponent<Text>().text = "Field is empty";
            pass_display.GetComponent<Text>().color = Color.red;
            return false;
        }

        if (Password.Length < 7)
        {
            pass_display.GetComponent<Text>().text = "Atleast 7 Characters long";
            pass_display.GetComponent<Text>().color = Color.red;
            return false;
        }
        return true;
    }

    public bool awaitLogin(){
        bool succes = false;

        do{ } while(!_dataRecieved);

        switch(_rd.Message)
        {
            case "OK":
            {
                print("Login succes: Logged in succesfully");
                usernameoremail.GetComponent<InputField>().text = "";
                password.GetComponent<InputField>().text = "";
                user_display.GetComponent<Text>().text = "";
                pass_display.GetComponent<Text>().text = "Login Successful";
                pass_display.GetComponent<Text>().color = Color.green;
                TokenResponse = _rd.Token; //Sets the usertoken
                succes = true;
                break;
            }

            case "User doesn't exist":
            {
                print("Login failed: User doesn't exist");
                user_display.GetComponent<Text>().text = "Username doesn't exist!";
                user_display.GetComponent<Text>().color = Color.red;
                _dataRecieved = false;
                break;
            }

            case "Password is incorrect":
            {
                print("Login failed: Password is incorrect");
                pass_display.GetComponent<Text>().text = "Password is invalid!";
                pass_display.GetComponent<Text>().color = Color.red;
                _dataRecieved = false;
                break;
            }

            case "Invalid username":
            {
                print("Login failed: Invalid username");
                user_display.GetComponent<Text>().text = "Username is invalid!";
                user_display.GetComponent<Text>().color = Color.red;
                _dataRecieved = false;
                break;
            }

            default:
            {
                print(_rd.Message);
                user_display.GetComponent<Text>().text = "Unknown error!";
                user_display.GetComponent<Text>().color = Color.red;
                _dataRecieved = false;
                break;
            }
        }
        return succes;
    }

    public void LoginButton()
    {
        bool unameoremailValidate = false;
        bool paswValidate = false;
        var s = false;

        unameoremailValidate = ValidateUsernameorEmail(UsernameorEmail);
        paswValidate = ValidatePassword(Password);

        if(unameoremailValidate && paswValidate)
        {
            ServerCommandLogIn Sc = new ServerCommandLogIn("Login", UsernameorEmail, Password);

            WebSocketClient.Start();
            WebSocketClient.Send(JsonUtility.ToJson(Sc));
            
            s = awaitLogin();
        }

        if(s){
            SceneManager.LoadScene(3);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (usernameoremail.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (UsernameorEmail != "" && Password != "")
            {
                LoginButton();
            }
        }

        UsernameorEmail = usernameoremail.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
    }

    public void GoToRegister()
    {
        SceneManager.LoadScene(2);
    }
}


