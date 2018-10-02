using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GridManager : MonoBehaviour 
{


	[SerializeField]
	LayerMask unWalkAble;
//	[SerializeField]
//	WayPoint Node;		//setmanual Place Node Bottom Left for Debug

	public int GridSizeX = 30;	//for setting up grid Debug
	public int GridSizeZ = 30;  //for setting up grid Debug

	public bool getPathRun;

	//for storing Waypoints with wpID as index
	[HideInInspector]
	public Dictionary<Vector2, WayPoint> grid = new Dictionary<Vector2, WayPoint>(); 


	Queue <WayPoint> queue = new Queue<WayPoint>();
	List <WayPoint> path = new List<WayPoint> ();
	public List <EnemyWP> unitPath = new List<EnemyWP> ();

	WayPoint searchOrigin;

	Vector2[] directions = 
	{
		Vector2.up,  	// = new Vector2 (0X,1Y)
		Vector2.right, 	// = new Vector2 (1X,0Y)
		Vector2.down,	// = new Vector2 (0X,-1Y)
		Vector2.left	// = new Vector2 (-1X,0Y)
	};

	public WayPoint startNode;
	public WayPoint endNode;


	void LoadDictionairyWP ()
	{
		var Waypoints = FindObjectsOfType<WayPoint> ();
		foreach (WayPoint wp in Waypoints)
		{
			if (!grid.ContainsKey (wp.wpID))//check for overlapping

			{
				grid.Add (wp.wpID, wp);
			//	CheckPassable (wp);
			}
			else
			{
				print ("!!OVERLAPPING NODES!!" + wp.name);
			}
		}
	}

	void CheckPassable(WayPoint _wp)
	{
		unWalkAble = 1 << 9;
		//runOnce when lvlDesign finished
		_wp.passable = !Physics.CheckBox (_wp.transform.position, new Vector3 (0.5f, 0.5f, 0.5f),Quaternion.identity, unWalkAble);
	}

	void Awake () 
	{
		LoadDictionairyWP ();
	}
		
	public void BFSearch(WayPoint start, WayPoint end) //1input start & endNode
	{

		//2.bfs
		if(start.wpID == end.wpID){return;}
		getPathRun = true;
		queue.Enqueue (start);
		while (queue.Count > 0 && getPathRun)
		{
			searchOrigin = queue.Dequeue ();
			searchOrigin.explored = true;				

			BFSGotEnd (end);
			CheckNeighbours (end);	

		}
		//3.create path
		BuildPath (start,end);
	}

	void BFSGotEnd(WayPoint _end)
	{
		if (searchOrigin.wpID == _end.wpID)
		{
			ResetPath ();
			getPathRun = false;
		}
	}
	void ResetPath()
	{
		path = new List<WayPoint> ();
		foreach (WayPoint wp in grid.Values)
		{
			wp.explored = false;
		}
	}
	//stores gridNodes in dictionairy.. Also check and set if node is passable


	void CheckNeighbours (WayPoint stop)
	{
		if (!getPathRun || searchOrigin.wpID == stop.wpID){return;}
		foreach (Vector2 direction in directions)
		{
			Vector2 NeighbourID = direction + searchOrigin.wpID;
			if(grid.ContainsKey(NeighbourID))
			{
				QueueNewNeighBours (NeighbourID);

			}
		}
	}

	void QueueNewNeighBours (Vector2 _neighbourID)
	{
		WayPoint Neighbour = grid [_neighbourID];	// (grid[NeighbourID] = find the waypoint with the NeighbourID
		if (!Neighbour.explored || !queue.Contains(Neighbour))	
		{
			if (Neighbour.passable)
			{
				queue.Enqueue (Neighbour);
			
				if(Neighbour.foundBy == null)
				Neighbour.foundBy = searchOrigin;
			}
		}
	}

	void BuildPath (WayPoint _start,WayPoint _end)
	{
		path.Add (_end);

		WayPoint previous = _end.foundBy;

		while (previous != _start)
		{
			path.Add (previous);
			previous = previous.foundBy;
		}


		path.Add (_start);
		//4.reverse List = path van start naar end
		path.Reverse ();

		//*5.Check welke waypoints have an enemyWP Set in List truePath. 
		for(int i = 0; i < path.Count; i++)
		{
			//check if node has enemy wp
			if (path [i].hasEwp)
			{
				//get enemy wp store in list.. make sure order is correct	Debug List waarschijnlijk niet nodig
				unitPath.Add(path[i].eWP) ;
			//	break;
			}
			//Debug
			if(path[i] != _end)
			{
				if(path[i] != _start)
				path [i].SetColor (Color.blue);
			}
		}
	}


	//Only For Debug. building a grid from relative bottomLeftNode 
//	void SpawnGrid ()	
//	{
//		for (int i = 0; i < GridSizeX; i++)
//		{
//			Vector3 newPosX = Node.transform.position;
//			newPosX.x = Node.transform.position.x + i;
//			if (Node.transform.position != newPosX)
//			{
//				Instantiate (Node, newPosX, Quaternion.identity, this.transform);
//			}
//			for (int b = 1; b < GridSizeZ; b++)
//			{
//				Vector3 newPosZ = newPosX;
//				newPosZ.z = newPosX.z + b;
//				Instantiate (Node, newPosZ, Quaternion.identity, this.transform);
//			}
//		}
//	}

}
