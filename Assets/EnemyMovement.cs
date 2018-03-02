using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour {

    //[SerializeField] List<WayPoint> path;

	
	void Start ()
    {
        PathFinder pathfinder = FindObjectOfType<PathFinder>();

        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<WayPoint> path)
    {
        print("StartPatrolling");

        foreach (WayPoint wayPoint in path)
        {

            transform.position = wayPoint.transform.position;

            yield return new WaitForSeconds(1f);

        }

        print("End Patrolling");
    }

  
}
