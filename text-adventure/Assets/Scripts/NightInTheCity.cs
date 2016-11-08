using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class NightInTheCity : MonoBehaviour {

	public AudioSource sfxSource;
	public Camera mainCam;

	//declaring an audioClip so I can change the SFX later.
	public AudioClip winSound;

	public Image city;
	public Image keyImage;

	public string currentRoom;
	public string myText;

	private bool hasKey = false;

	//variables to store possible room connections.
	private string room_north;
	private string room_south;
	private string room_west;
	private string room_east;

	// Use this for initialization
	void Start () {

		myText = "We ran our scene.";
		currentRoom = "title";
	
	}
	
	// Update is called once per frame
	void Update () {
		

		//deactivate picture if you're not in the title page or win room
		if (currentRoom == "title" || (currentRoom == "locked door" && hasKey))
		{
			city.enabled = true;
		} else {
			city.enabled = false;

			//if I wanted to change the image with another
			//portraitOfDiego.sprite = mySpriteVariable;
		}

		//only activate key image if you don't have the key, or you've used it to win
		if (currentRoom == "locked door" || !hasKey)
		{
			keyImage.enabled = false;
		} else {
			keyImage.enabled = true;
		}


		//we set our rooms to nil, so that if we haven't overwritten them by the time
		//we check for keypresses, we know there's no room.
		room_east = "nil";
		room_north = "nil";
		room_south = "nil";
		room_west = "nil";

		//resetting the background and text color, so that if i leave a room
		//where I change it, it doesn't stay that color
		mainCam.backgroundColor = Color.black;
		GetComponent<Text>().color = Color.white;


		// if I'm in the entryway, I want the game to say "you are in the entryway."
		// else, check the other statements.
		if (currentRoom == "title"){
			myText = "Night in the City\n\nBy Anna Stassen\n\nPress Space to Begin";

			if (Input.GetKeyDown(KeyCode.Space)) {
				currentRoom = "entry";
			}
		} else if (currentRoom == "entry"){

			room_north = "hallway";

			myText = "you are in the entryway.\n";
			myText += "The entryway is cool.";


		} else if ( currentRoom == "hallway") {

			room_east = "kitchen";
			room_south = "entry";
			room_west = "locked door";

			myText = "you are in the hallway.";

		} else if ( currentRoom == "kitchen") {

			room_west = "hallway";

			myText = "You are in the subway.";
			if (!hasKey){
				myText += " You see something lit up on the tracks. Press \"i\" to inspect.";

				if (Input.GetKeyDown(KeyCode.I)){

					currentRoom = "drain";


				}
			}

		}  else if (currentRoom == "drain"){
			//changing background color and text color
			mainCam.backgroundColor = Color.white;
			GetComponent<Text>().color = Color.black;

			myText = "You found your phone! Press spacebar to return to the kitchen.";
			if (!hasKey) {
				sfxSource.Play();
			}
			hasKey = true;

			if (Input.GetKeyDown(KeyCode.Space)) {
				currentRoom = "kitchen";
			}

		}else if (currentRoom == "locked door") {

			sfxSource.clip = winSound;
			if (!sfxSource.isPlaying) {
				sfxSource.Play();
			}

			if (hasKey) {
				myText = "HEYOOOOO YOU GOT TO THE WIN ROOOM!!!! BOIIOIOIOIOING, NICE! Let'S ENJOY CAKE.";

			} else {

				myText = "Sorry, you unlucky person, you need a key.\n\n Press space to return to the subway";

				if (Input.GetKeyDown(KeyCode.Space)) {
					currentRoom = "hallway";
				}

			}


		} else {

			myText = "You have fallen into a void.";

		}


		// here we're checking for keyboard input
		// if a directional key is pressed
		// we go to the corresponding room.

		myText += "\n\n";
		if (room_north != "nil"){

			myText += "Press Up to go to the " + room_north + "\n";

			if (Input.GetKeyDown(KeyCode.UpArrow)) {

				currentRoom = room_north;

			}
		}


		if (room_south != "nil"){

			myText += "Press Down to go to the " + room_south + "\n";

			if (Input.GetKeyDown(KeyCode.DownArrow)){


				currentRoom = room_south;

			}
		}

		if (room_east != "nil"){

			myText += "Press Right to go to the " + room_east + "\n";

			if (Input.GetKeyDown(KeyCode.RightArrow)){

				currentRoom = room_east;

			}
		}

		if (room_west != "nil") {

			myText += "Press Left to go to the " + room_west + "\n";

			if (Input.GetKeyDown(KeyCode.LeftArrow)){

				currentRoom = room_west;

			}
		}

		//We are acccesing the text component, then using dot notation
		//to modify the text attribute.
		GetComponent<Text>().text = myText;

	}

}
