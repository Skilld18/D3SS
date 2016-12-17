using UnityEngine;
using System.Net;
using System;
using System.Net.Sockets;
using System.Threading;  
using System.IO;




public class SSS: MonoBehaviour {
	bool mRunning = false;
	string msg = "";
	Thread mThread;
	public GameObject Wall;	
	public GameObject Floor;
 	public GameObject Player;	
	public GameObject Monster;



	 TcpListener tcp_Listener = null;
 void Start()
 {
         tcp_Listener = new TcpListener(9999);
         tcp_Listener.Start();
 }
void Update()
 {
     if (tcp_Listener.Pending()){
           

         TcpClient client = tcp_Listener.AcceptTcpClient();
         NetworkStream ns = client.GetStream();
         StreamReader reader = new StreamReader(ns);
         msg = reader.ReadToEnd();
	 int x = 0;
	 int z = 0;
	 GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
	    foreach (GameObject target in gameObjects) {
		GameObject.Destroy(target);
	    }
	 for(int i = 0;i<msg.Length;i++){
		if(msg[i] == '\n'){
			z++;
			x = 0;
			continue;		
		}
		x++;
                if(msg[i] == '.'){
		    
		    Instantiate(Floor, new Vector3(x, 0, -z), Quaternion.Euler(0, 0, 0));
		}
		else if(msg[i] == '#'){
		    Instantiate(Wall, new Vector3(x, 0, -z), Quaternion.Euler(0, 0, 0));
		}
		else if(msg[i] == '@'){
		    Instantiate(Player, new Vector3(x, 0, -z), Quaternion.Euler(0, 0, 0));
		}
		else if(msg[i] != ' '){
		    Instantiate(Monster, new Vector3(x, 0, -z), Quaternion.Euler(0, 0, 0));
		}
		

	}
         reader.Close();
         client.Close();
	}





 }
 public void stopListening()
 {
     mRunning = false;
 }
void SayHello()
 {
     try
     {

         print("Server Start");
         while (mRunning)
         {
             // check if new connections are pending, if not, be nice and sleep 100ms
             if (!tcp_Listener.Pending())
             {
                 Thread.Sleep(100);
             }
             else
             {
                 print("1");
                 TcpClient client = tcp_Listener.AcceptTcpClient();
                 print("2");
                 NetworkStream ns = client.GetStream();
                 print("3");
                 StreamReader reader = new StreamReader(ns);
                 print("4");
                 msg = reader.ReadToEnd();


                 reader.Close();
                 client.Close();
             }
         }
     }
     catch (ThreadAbortException)
     {
         print("exception");
     }
     finally
     {
         mRunning = false;
         tcp_Listener.Stop();
     }
 }
 void OnApplicationQuit()
 {
     // stop listening thread
     stopListening();
     // wait fpr listening thread to terminate (max. 500ms)
     mThread.Join(500);
 }

}

