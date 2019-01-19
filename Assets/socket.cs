using System.Net.Sockets;
using UnityEngine;
using System;
using System.Diagnostics;
using System.IO;
using Debug = UnityEngine.Debug;

public class socket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Process.Start("/bin/bash", "-c \"echo 'Hello World!'\"");
            setupSocket();
            //string msg = "__SUBSCRIBE__"+channel+"__ENDSUBSCRIBE__";
            string msg = "Sending By Sona";
            writeSocket(msg);
            writeSocket("a\n");
            writeSocket("a\n");
            writeSocket("a\n");
            writeSocket("a\n");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                writeSocket("ee");
            }
            else if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                writeSocket("'\n'");
            }
            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                writeSocket("tt");
            }
            else
            {
                writeSocket(Input.inputString);
            }

            Debug.Log(Input.inputString);
            
        }


    }
    
        internal Boolean socketReady = false;

        TcpClient mySocket;
        NetworkStream theStream;
        StreamWriter theWriter;
        StreamReader theReader;
        String Host = "localhost";
        Int32 Port = 2345;
        string channel = "testingSona";


        public void setupSocket() { 
            try {
                mySocket = new TcpClient(Host, Port);
                theStream = mySocket.GetStream(); 
                theWriter = new StreamWriter(theStream);
                theReader = new StreamReader(theStream);
                socketReady = true;         
            }
            catch (Exception e) {
                Debug.Log("Socket error: " + e);
            }
        }
        public void writeSocket(string theLine) {
            if (!socketReady)
                return;
            String foo = theLine;
            theWriter.Write(foo);
            theWriter.Flush();

        }
        
        public void closeSocket() {
            if (!socketReady)
                return;
            theWriter.Close();
            theReader.Close();
            mySocket.Close();
            socketReady = false;
        }

}
