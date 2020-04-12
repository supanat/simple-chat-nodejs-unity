using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SocketIO;

public class SimpleChat : MonoBehaviour
{

    static SocketIOComponent socket;

    public string inputText = "Hello World";
    public string displayText = "";

    private List<string> messageList = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);
        socket.On("disconnected", OnDisconnected);
        socket.On("message.sent", OnListenerMessage);


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnConnected(SocketIOEvent obj)
    {
        Debug.Log("conected");
    }

    private void OnDisconnected(SocketIOEvent obj)
    {

    }

    private void OnListenerMessage(SocketIOEvent obj)
    {
        //print(obj.data["message"]);

        string msg = obj.data["message"].str;

        displayText += msg+System.Environment.NewLine;

    }


    void OnGUI()
    {
        displayText = GUI.TextArea(new Rect(10, 10, 500, 100), displayText, 25);


        inputText = GUI.TextField(new Rect(10, 120, 500, 20), inputText, 25);



        if (GUI.Button(new Rect(10, 160, 50, 30), "Send"))
        {

            JSONObject jsonObject = new JSONObject(JSONObject.Type.OBJECT);
            jsonObject.AddField("message", inputText);
            socket.Emit("message.send", jsonObject);
        }



    }




}
