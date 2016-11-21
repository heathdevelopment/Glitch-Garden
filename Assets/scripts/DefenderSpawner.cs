using UnityEngine;
using System.Collections;

public class DefenderSpawner : MonoBehaviour
{
	public Camera myCamera;
	

	private GameObject defenderParent;
	private StarDisplay starDisplay;
	
	
	// Use this for initialization
	void Start ()
	{
		defenderParent = GameObject.Find ("Defenders");
		starDisplay = GameObject.FindObjectOfType<StarDisplay> ();
		
		if (!defenderParent) {
			defenderParent = new GameObject ("Defenders");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnMouseDown ()
	{
		Vector2 rawPos = CalculateWorldPointOfMouseClick ();
		Vector2 spawnPosition = SnapToGrid (rawPos);
		GameObject defender = Button.selectedDefender;

		int defenderCost = defender.GetComponent<Defender> ().starCost;

		if (starDisplay.UseStars (defenderCost) == StarDisplay.Status.SUCCESS) {
			SpawnDefender (spawnPosition, defender);
		} else {
			Debug.Log ("Insufficient Stars to spawn");
		}
	}
	//SpawnDefender uses a CheckIfPosEmpty to determine if valid placement.
	void SpawnDefender (Vector2 spawnPosition, GameObject defender)
	{
		Quaternion zeroRot = Quaternion.identity;
		if (CheckIfPosEmpty (spawnPosition)) {
			GameObject newDef = Instantiate (defender, spawnPosition, zeroRot) as GameObject;
			newDef.transform.parent = defenderParent.transform;
		} else {
			print ("space is occupied by another defender");
		}
	}

	Vector2 SnapToGrid (Vector2 rawWorldPos)
	{
		float newX = Mathf.RoundToInt (rawWorldPos.x);
		float newY = Mathf.RoundToInt (rawWorldPos.y);
		return new Vector2 (newX, newY);
	}

	Vector2 CalculateWorldPointOfMouseClick ()
	{
		//int mouseX = Mathf.RoundToInt (Input.mousePosition.x/100);
		//int mouseY = Mathf.RoundToInt (Input.mousePosition.y/100);
		
		
		float mouseX = Input.mousePosition.x;
		float mouseY = Input.mousePosition.y;
		float distanceFromCamera = 10f;
		
		Vector3 weirdTriplet = new Vector3 (mouseX, mouseY, distanceFromCamera);
		Vector2 worldPos = myCamera.ScreenToWorldPoint (weirdTriplet);
		return worldPos;
	}
	//This method will loop through each defender within the defenderArray to determine position.  If that position is occupied
	//it will return false.  Otherwise it will return true.
	public bool CheckIfPosEmpty (Vector2 spawnPosition)
	{
		Defender[] defenderArray = GameObject.FindObjectsOfType<Defender> ();
		
		foreach (Defender defender in defenderArray) {
			Vector2 defenderPos = defender.transform.position;
			if (defenderPos == spawnPosition) {
				return false;
			} 
		}
		return true;
	}
}
