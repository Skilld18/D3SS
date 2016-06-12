using UnityEngine;
using System.Collections;
using System.IO;  

public class read : MonoBehaviour {
	void Start (){

		string file = "/home/rdunk/crawl.txt";
		if(File.Exists(file)){
			int y = 0;
		    var sr = File.OpenText(file);
		    var line = sr.ReadLine();
		    while(line != null){
		    	for(int x = 0;x<line.Length;x++){
		    		parse(line[x], x, y);
		    	}



		        Debug.Log(line); // prints each line of the file
		        line = sr.ReadLine();
		        y++;
		    }  
		} else {
		    Debug.Log("Could not Open the file: " + file + " for reading.");
		    return;
		}

	}
	// Update is called once per frame
	void Update () {
	
	}

	void parse(char line, int x, int y){
		if(line=='c'){
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        	cube.transform.position = new Vector3((float)x, 0.5F, (float)y);
		}
	}
}
