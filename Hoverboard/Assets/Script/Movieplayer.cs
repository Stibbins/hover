using UnityEngine;
using System.Collections;

public class Movieplayer : MonoBehaviour {
	//GUI Texture
	private GUITexture videoGUItex;
	//The movie texture
	public MovieTexture mTex;
	//The movie name
	public string movieName;

	void Awake (){
		//Get the guitexture
		videoGUItex = this.GetComponent<GUITexture>();
		//Moviename
		//mTex = (MovieTexture)Resources.Load (movieName);
		//Anamorphic Fullscreen
		videoGUItex.pixelInset = new Rect (Screen.width / 2, -Screen.height / 2, 0, 0);
	}

	// Use this for initialization
	void Start () {
		//Set the videoguitexture to be the same as mTex
		videoGUItex.texture = mTex;
		//Plays movie
		mTex.Play ();
		//Loop Movie
		mTex.loop = true;

		//MovieTexture movie = renderer.material.mainTexture as MovieTexture;
		//movie.Play ();
		//movie.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
