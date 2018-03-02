using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase] //makes it easier to select the parent object and not accidentally the childrens
[ExecuteInEditMode] // allows execution in editor
[RequireComponent(typeof(WayPoint))]
public class CubeEditor: MonoBehaviour {


    //is better to use a constant for grid consistency than a variable that can change in each cube 
    //[Range(1f,100f)][SerializeField] float gridSize = 10f;
    // Vector3 snapPos;



    WayPoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<WayPoint>();
    }


    void Update()
    {
        SnapsToGrid();

        LabelUpdate();

    }

    private void SnapsToGrid()
    {
        int gridSize = waypoint.GetGridSize();

        transform.position = new Vector3(waypoint.GetGridPos().x * gridSize, 0, waypoint.GetGridPos().y * gridSize);
    }

    private void LabelUpdate()
    {
        int gridSize = waypoint.GetGridSize();
        //prints coordinates of the cube
        TextMesh textMesh = GetComponentInChildren<TextMesh>();

        string labelText = waypoint.GetGridPos().x  + "," + waypoint.GetGridPos().y ;

        gameObject.name = "Cube " + labelText;

        textMesh.text = waypoint.GetGridPos().x + "," + waypoint.GetGridPos().y ;
    }
}
