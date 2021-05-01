using UnityEngine;

public class FinishZone : MonoBehaviour
{
	//A reference to the game manager
	public GameManager gameManager;
	
	// When an object enters the finish zone, let the
	// game manager know that the current game has ended
	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("FINISH");
		//string message;
		//message = "Good Work! You finished the level!";
		//string message;
		//message = "Good Work! You finished the level!";
		gameManager.FinishedGame(); 
		//if (other.gameObject.tag == "FinishZone")
		//{
			//string message;
			//message = "Good Work! You finished the level!";
			//gameManager.FinishedGame();
			//gameManager.isFinished = true;
			//canvas.GetComponent<CanvasManager>().ToggleButton("ButtonDisarmTrap", 1);
			//canvas.GetComponent<CanvasManager>().ToggleNotification(true);
			//DoorId.doorId = 1;
			//Debug.Log("FINISH");
		//}
	}

}
