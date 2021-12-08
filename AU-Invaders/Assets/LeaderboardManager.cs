using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using WebSocketSharp;
using System;
using Backend.Types;

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI HighscoreText1;
    public TextMeshProUGUI HighscoreText2;
    public TextMeshProUGUI HighscoreText3;
    public TextMeshProUGUI HighscoreText4;
    public TextMeshProUGUI HighscoreText5;
    public TextMeshProUGUI HighscoreText6;
    public TextMeshProUGUI HighscoreText7;
    public TextMeshProUGUI HighscoreText8;
    public TextMeshProUGUI HighscoreText9;
    public TextMeshProUGUI HighscoreText10;
    public TextMeshProUGUI UserText1;
    public TextMeshProUGUI UserText2;
    public TextMeshProUGUI UserText3;
    public TextMeshProUGUI UserText4;
    public TextMeshProUGUI UserText5;
    public TextMeshProUGUI UserText6;
    public TextMeshProUGUI UserText7;
    public TextMeshProUGUI UserText8;
    public TextMeshProUGUI UserText9;
    public TextMeshProUGUI UserText10;

    int Highscore1 = 0;
    int Highscore2 = 0;
    int Highscore3 = 0;
    int Highscore4 = 0;
    int Highscore5 = 0;
    int Highscore6 = 0;
    int Highscore7 = 0;
    int Highscore8 = 0;
    int Highscore9 = 0;
    int Highscore10 = 0;

    string User1;
    string User2;
    string User3;
    string User4;
    string User5;
    string User6;
    string User7;
    string User8;
    string User9;
    string User10;

    private bool _dataRecieved = false;
    private GamesResponse _gr;

    public LeaderboardManager()
    {
        WebSocketClient.OnMessageEvent += this.OnMessage;
    }
    void OnMessage(object sender, MessageEventArgs e)
    {
        //_sr = new ServerResponse();
        //_sr = JsonUtility.FromJson<ServerResponse>(e.Data);
        _dataRecieved = true;
        Debug.Log(e.Data);

        _gr = new GamesResponse();
        _gr = JsonUtility.FromJson<GamesResponse>(e.Data);
    }

    // Start is called before the first frame update
    void Start()
    {
        Request request = new Request();
        request.Command = Commands.GET_BEST_GAMES;
        WebSocketClient.Send(request.ToString());
        do { } while (!_dataRecieved);
        //print(_gr.Games[0].Highscore);

        Debug.Log(_gr);

       // HighscoreText1.text = arr[0].ToString();
        HighscoreText2.text = _gr.Games[1].Highscore.ToString();
        HighscoreText3.text = _gr.Games[2].Highscore.ToString();
        HighscoreText4.text = _gr.Games[3].Highscore.ToString();
        HighscoreText5.text = _gr.Games[4].Highscore.ToString();
        HighscoreText6.text = _gr.Games[5].Highscore.ToString();
        HighscoreText7.text = _gr.Games[6].Highscore.ToString();
        HighscoreText8.text = _gr.Games[7].Highscore.ToString();
        HighscoreText9.text = _gr.Games[8].Highscore.ToString();
        HighscoreText10.text = _gr.Games[9].Highscore.ToString();

        UserText1.text = User1.ToString();
        UserText2.text = User2.ToString();
        UserText3.text = User3.ToString();
        UserText4.text = User4.ToString();
        UserText5.text = User5.ToString();
        UserText6.text = User6.ToString();
        UserText7.text = User7.ToString();
        UserText8.text = User8.ToString();
        UserText9.text = User9.ToString();
        UserText10.text = User10.ToString();
    }
}

[Serializable]
public class GamesResponse
{
    public GamesResponse()
    {
        Games = new List<GamesResponseNested>();
    }
    public string Message;
    public int Code;
    public List<GamesResponseNested> Games;

}

[Serializable] 
public class GamesResponseNested
{
    public GamesResponseNested() { }
    public int Coinsgained;
    public int Highscore;
}