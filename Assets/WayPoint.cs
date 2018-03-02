using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint: MonoBehaviour {

    public bool isExplored = false;
    public WayPoint exploredFrom; 



    Vector2Int gridPos;

    const int gridSize = 10;


    public int GetGridSize(){

        return gridSize;
    }
	

    public Vector2Int GetGridPos(){

        return new Vector2Int(

            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );


    }

    public void SetTopColor(Color color){

        MeshRenderer topMeshRend = transform.Find("Top").GetComponent<MeshRenderer>();

        topMeshRend.material.color = color;
    }
}
