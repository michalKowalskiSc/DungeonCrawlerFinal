using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{	
	// Place holders to allow connecting to other objects
	// public Transform spawnPoint;
	public GameObject player;

	// Flags that control the state of the game
	private float elapsedTime = 0;
	private bool isRunning = false; 
	private bool isFinished = false;
	public Texture2D cursorArrow;

	// So that we can access the player's controller from this script
	private FirstPersonController fpsController;


	// Use this for initialization
	void Start ()
	{
		// Finds the First Person Controller script on the Player
		fpsController = player.GetComponent<FirstPersonController> ();
	
		// Disables controls at the start.
		fpsController.enabled = false;

		Cursor.visible = true;
		Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
	}


	//This resets to game back to the way it started
	private void StartGame()
	{
		isRunning = true;
		isFinished = false;
		fpsController.enabled = true;
	}


	// Update is called once per frame 
	void Update() { 

	}


	// Runs when the player enters the finish zone
	public void FinishedGame()
	{
		//isRunning = false;
		//isFinished = true;
		//fpsController.enabled = false;
	}


	//This section creates the Graphical User Interface (GUI)
	void OnGUI() {
		
		if(!isRunning)
		{
			string message;

			if(isFinished)
			{
				message = "Good Work! You finished the level!";
			}
			else
			{
				message = "Start your Journey!";
			}

			Rect startButton = new Rect(Screen.width/2 - 120, Screen.height/2, 240, 30);

			if (GUI.Button(startButton, message) || Input.GetKeyDown(KeyCode.Return))
			{
				StartGame ();
			}
		}
		
		if(isFinished)
		{
			GUI.Box(new Rect(Screen.width / 2 - 65, 185, 130, 40), "Good Work! You finished the level!");
		}
	}
}
