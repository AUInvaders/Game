using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using WebSocketSharp;
using UnityEngine;
using TMPro;
using System;
using Backend.Types;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HighscoreText;
    public static ScoreManager Instance;

    private bool _dataRecieved = false;
    //private RecievedData _rd;
    //I toppen sammen med de andre privates
    private Outer _sr;
    public static int score = 0;
    int Highscore = 0;
    
    public ScoreManager()
    {
        WebSocketClient.OnMessageEvent += this.OnMessage;
    }

    void OnMessage(object sender, MessageEventArgs e)
    {
        //_sr = new ServerResponse();
        //_sr = JsonUtility.FromJson<ServerResponse>(e.Data);
        _dataRecieved = true;

        _sr = new Outer();
        _sr = JsonUtility.FromJson<Outer>(e.Data);
        Highscore = _sr.Game.Highscore;

        print(_sr.Game.Highscore+" Fra On message");
        print(Highscore+" Highscore i onmessage");
        HighscoreText.text = "Highscore: "+Highscore.ToString();
        print(JsonUtility.ToJson(_sr));
        print(e.Data);
        Debug.Log("Lortet er modstaget");
    }

    

    private void Awake()
    {
        Instance = this; 
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
        //Game game = new Game();
        //Request HighscoreReq = new Request();
        //WebSocketClient.Start();
        //HighscoreReq.Command = Commands.GET_BEST_GAME;
        //WebSocketClient.Send(HighscoreReq.ToString());
        Request request = new Request();
        request.Command = Commands.GET_BEST_GAME;
        
        WebSocketClient.Send(request.ToString());
        //Highscore = game.Highscore;
        do { } while (!_dataRecieved);
        print(Highscore + " Fra Start");
        HighscoreText.text = "Highscore: " + Highscore.ToString();


        scoreText.text = score.ToString() + " Coins!";
        
    }

    [Serializable]
    public class RecievedData { public string Message; public int Code; public int score; public int HighestScore;}
    public void AddPoint(int pointscore)
    {
        score += pointscore;
        scoreText.text = score.ToString() + " Coins!";
    }
    public void New_Highscore()
    {
        if (score > Highscore)
        {
            Highscore = score;
            HighscoreText.text = score.ToString() + " Coins!";
        }
    }
    public void SendHighscore()
    {
        //Send Highscore to db
        //HighscoreCommand hc = new HighscoreCommand(Highscore, score);
        //AddGameCommand gc = new AddGameCommand(hc);
        //WebSocketClient.Start();
        //WebSocketClient.Send(JsonUtility.ToJson(gc));
        Game game = new Game();
        game.Coinsgained = score;
        game.Highscore = Highscore;
        AddGameRequest req = new AddGameRequest();
        req.Game = game;
        req.Command = Commands.ADD_GAME;
        //WebSocketClient.Start();
        WebSocketClient.Send(req.ToString());
    }
}

//------------------------------------------
[Serializable]
public class Outer
{
    public Outer()
    {
        Game = new ServerResponseGame();
    }
    public ServerResponseGame Game;
}

[Serializable]
public class ServerResponseGame
{
    public ServerResponseGame()
    {
        User = new NestedServerResponse();
    }
    public int Highscore;
    public int Coinsgained;
    public string Starttime;
    public NestedServerResponse User;
    public string Message;
    public int Code;
}

[Serializable]
public class NestedServerResponse
{
    public string Username;
}


