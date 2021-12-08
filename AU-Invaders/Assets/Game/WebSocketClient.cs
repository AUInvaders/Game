using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WebSocketSharp;

public class WebSocketClient : MonoBehaviour
{
    // Start is called before the first frame update
    private static WebSocket _ws;

    public static event EventHandler<MessageEventArgs> OnMessageEvent;

    public static void Start()
    {
        _ws = new WebSocket("wss://43-review-bugfix-web-etkotu.devops.lasse-it.dk/");
        //ws://127.0.0.1:7890/Echo     Local
        //wss://prj4-backend.devops.lasse-it.dk/
        //wss://43-review-bugfix-web-etkotu.devops.lasse-it.dk/
        _ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;

        _ws.OnMessage += Webs_OnMessage;

        _ws.Connect();
    }

    public static void Send(string Data) => _ws.Send(Data);

    public static void Webs_OnMessage(object sender, MessageEventArgs e)
    {
        OnMessageEvent?.Invoke(null, e);
    }
}
