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
		if (currentRoom == "title" || (currentRoom == "bar" && hasKey))
		{
			city.enabled = true;
		} else {
			city.enabled = false;

			//if I wanted to change the image with another
			//portraitOfDiego.sprite = mySpriteVariable;
		}

		//only activate key image if you don't have the key, or you've used it to win
		if (currentRoom == "bar" || !hasKey)
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
				currentRoom = "Washington Square Park";
			}
		} else if (currentRoom == "Washington Square Park"){

			room_north = "2 Bros. $1 Pizza";

			myText = "It has been a long Friday night in the city. All of your friends have returned home, so you are all alone. Daylight is approaching and you must get back home soon.\n\n";
			myText += "Wait, but first you are really hungry and only have a few bucks left on you!";


		} else if ( currentRoom == "2 Bros. $1 Pizza") {

			room_east = "subway";
			room_south = "Washington Square Park";
			room_west = "bar";

			myText = "You are in 2 Bros. $1 Pizza.\n\n";
			myText += "You are waiting in line and can't wait to get a slice. While you are waiting you decide to pull out your phone and text Jason to see if he got home ok.\n";
			myText += "Uh oh, you lost your phone, which was also your wallet, which means you can't buy pizza.\n";
			myText += "Retrace your steps and go find your phone!";

		} else if ( currentRoom == "subway") {

			room_west = "2 Bros. $1 Pizza";

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

			myText = "You found your phone! Press spacebar to return to the subway.";
			if (!hasKey) {
				sfxSource.Play();
			}
			hasKey = true;

			if (Input.GetKeyDown(KeyCode.Space)) {
				currentRoom = "subway";
			}

		}else if (currentRoom == "bar") {

			sfxSource.clip = winSound;
			if (!sfxSource.isPlaying) {
				sfxSource.Play();
			}

			if (hasKey) {
				myText = "You go the win room.";

			} else {

				myText = "Sorry, you didn't lose your phone here.\n\n Press space to return to the subway";

				if (Input.GetKeyDown(KeyCode.Space)) {
					currentRoom = "2 Bros. $1 Pizza";
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

			myText += "Press Up to go to " + room_north + "\n";

			if (Input.GetKeyDown(KeyCode.UpArrow)) {

				currentRoom = room_north;

			}
		}


		if (room_south != "nil"){

			myText += "Press Down to go to " + room_south + "\n";

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
