using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;  

public class read : MonoBehaviour {

    public List<GameObject> wall = new List<GameObject>();

	void Start (){

	}
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < wall.ToArray().Length;i++){
	        Destroy(wall[i]);
	    }
		string file = "/home/russell/Dropbox/code/crawl/crawl-ref/source/crawl.txt";
		if(File.Exists(file)){
			int y = 0;
		    var sr = File.OpenText(file);
		    var line = sr.ReadLine();
		    while(line != null){
		    	for(int x = 0;x<line.Length;x++){
		    		parse(line[x], x, -y);
		    	}
		        line = sr.ReadLine();
		        y++;
		    }
		} else {
		    Debug.Log("Could not Open the file: " + file + " for reading.");
		    return;
		}

	}

	void parse(char line, int x, int y){
		GameObject cube;
		if (line == '#') {
			cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
			wall.Add (cube);
			cube.transform.position = new Vector3 ((float)x, 0.5F, (float)y);
		} else if (line == '@') {
			cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
			cube.GetComponent<Renderer> ().material.color = Color.black;

			wall.Add (cube);
			cube.transform.position = new Vector3 ((float)x, 0.5F, (float)y);
		} else if (line >= (int)'a' && line <= (int)'z') {
			cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
			cube.GetComponent<Renderer> ().material.color = Color.yellow;
			wall.Add (cube);
			cube.transform.position = new Vector3 ((float)x, 0.5F, (float)y);
		}
		else if (line >= (int)'A' && line <= (int)'Z') {
			cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
			cube.GetComponent<Renderer> ().material.color = Color.blue;
			wall.Add (cube);
			cube.transform.position = new Vector3 ((float)x, 0.5F, (float)y);
		}


	}
}
