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
    private ServerResponse _sr;

    private void OnMessage(object sender, MessageEventArgs e)
    {
        //_sr = new ServerResponse();
        //_sr = JsonUtility.FromJson<ServerResponse>(e.Data);
        _dataRecieved = true;
        print(e);
        Debug.Log(e);
        Debug.Log("Lortet er sendt");
    }

    public static int score = 0;
    int Highscore = 0;

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
        /*do { } while (!_dataRecieved);
        switch (_sr.Message)
        {
            case "OK":
                {
                    print(_sr.Message);
                    
                    break;
                }
                
                    
        }
        */

        scoreText.text = score.ToString() + " Coins!";
        HighscoreText.text = "Highscore: " + Highscore.ToString();
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

[Serializable]
public class ServerResponse
{
    public string Message;
    public int Code;
    public List<NestedServerResponse> Games;
}

[Serializable]
public class NestedServerResponse
{
    public int Coinsgained;
    public int Highscore;
}
