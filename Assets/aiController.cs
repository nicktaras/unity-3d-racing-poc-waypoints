/* aiController (UNITY C#)
* 
* DESCRIPTION: 
* This class can be applied to:
* - orchestrate the ai to find and race towards the nearest way points.
* - orchestrate the ai to manage speed, steer, weapons.
*
* USAGE: 
* Attach script to a gameobject.
* 
* TEST: 
* tbc
* 
* AUTHOR: 
* Nicholas Taras
*
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiController : MonoBehaviour {

	// PUBLIC VARS

	public string _tagName;

	// PRIVATE VARS

	int currentWaypoint;
	int currentLap;
	GameObject[] objectsByTagName;

//	use hashable?
	public bool[] _wayPointCheckList;
			
	void Start () {

		initialiseRaceVars ();
		initialiseWayPointCheckList ();
			
	}

	// VARS TO RESET FOR A RACE

	void initialiseRaceVars(){
		currentWaypoint = 0;
		currentLap = 0;
	}

	// CREATE AN ARRAY OF [FALSE,FALSE] ETC TO REPRESENT EACH WAY POINT.

	void initialiseWayPointCheckList(){

		objectsByTagName = GameObject.FindGameObjectsWithTag("waypoint");

		int _lengthOfWayPoints = objectsByTagName.Length;
		_wayPointCheckList = new bool[_lengthOfWayPoints]; // WHY DO WE HAVE TO ADD 1 - WEIRD?
		int _index = 0;

		foreach (GameObject objectByTagName in objectsByTagName) {

			bool _reachedCurrentWayPoint = false;
			_wayPointCheckList [_index] = _reachedCurrentWayPoint;
			_index++;
		}
			
	}
		
	// RETURN TRUE WHEN THE TRANSFORM HAS REACHED ITS TARGET / TARGET RADIUS
	bool wayPointReached(GameObject _target){

		Vector3 currentPosition = transform.position;

		bool _wayPointReached = false;

		float closestDistance = Mathf.Infinity;

		float distance = (currentPosition - _target.transform.position).sqrMagnitude;

		if (distance < Random.Range(1, 10)) { // TODO UPDATE AND RANDOMISE VALUE HERE.
			_wayPointReached = true;
		}
			
		return _wayPointReached;

	}

	// CLASS CONSTRUCTOR

	public aiController(){}

	void Update () {

		Transform _transform = this.transform;

		MoveTowards _moveTowards = GetComponent<MoveTowards>();

		bool _reachedCurrentWayPoint = _wayPointCheckList[currentWaypoint];

		if (_wayPointCheckList[currentWaypoint] == false) {

			GameObject _target = GameObject.FindGameObjectsWithTag (_tagName) [currentWaypoint]; // SHOULD FIND THE CLOSEST.

			_moveTowards.moveToTarget (_target);

			bool _hasReachedWayPoint = wayPointReached (_target); 

			_wayPointCheckList [currentWaypoint] = _hasReachedWayPoint;

			if(_hasReachedWayPoint == true) { // UPDATE AND LOOK FOR THE NEXT.

				if(currentWaypoint >= _wayPointCheckList.Length -1) {

					Debug.Log ("currentWaypoint RESET");

					_wayPointCheckList[currentWaypoint] = false;
					currentWaypoint = 0;

				} else {

					Debug.Log ("currentWaypoint INC");
					
					_wayPointCheckList[currentWaypoint] = false;
					currentWaypoint++;

				}
					
			}

		}

	}

}
