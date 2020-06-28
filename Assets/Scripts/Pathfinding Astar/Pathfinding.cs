using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class Pathfinding : MonoBehaviour {
	//delete this M6
	//public Transform seeker, target;

	Grid2D grid;
	//add this M6
	static Pathfinding instance;

	void Awake() {
		grid = GetComponent<Grid2D>();
		//add this M6
		instance = this;
	}

	//add this M6
	public static Vector2[] RequestPath(Vector2 from, Vector2 to) {
		return instance.FindPath (from, to);
	}

	//delete this M6
	/*
	void Update(){
		//if(Input.GetKeyDown(KeyCode.K)){
			FindPath (seeker.position, target.position);
		//}
	}
	*/

	//modify this M6
	//void FindPath(Vector2 from, Vector2 to){
	Vector2[] FindPath(Vector2 from, Vector2 to) {

		Stopwatch sw = new Stopwatch ();
		sw.Start ();

		//add this M6
		Vector2[] waypoints = new Vector2[0];
		bool pathSuccess = false;

		Node startNode = grid.NodeFromWorldPoint(from);
		Node targetNode = grid.NodeFromWorldPoint (to);
		//add this M6
		startNode.parent = startNode;

		//add this M6 after class Grid2D
		if (!startNode.walkable) {
			startNode = grid.ClosestWalkableNode (startNode);
		}
		if (!targetNode.walkable) {
			targetNode = grid.ClosestWalkableNode (targetNode);
		}

		if (startNode.walkable && targetNode.walkable) {//-----

			//List<Node> openSet = new List<Node>();
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);

			while (openSet.Count > 0) {
				Node currentNode = openSet.RemoveFirst();
				/*
				for (int i = 1; i < openSet.Count; i++) {
					if (openSet [i].fCost < currentNode.fCost || 
						openSet [i].fCost == currentNode.fCost && 
						openSet [i].hCost < currentNode.hCost) {
						currentNode = openSet [i];
					}
				}

				openSet.Remove (currentNode);
				*/
				closedSet.Add (currentNode);

				if (currentNode == targetNode) {
					sw.Stop();
					print ("Path found: " + sw.ElapsedMilliseconds + " ms");
					//modify this M6
					//RetracePath(startNode, targetNode);
					//return;
					//to this
					pathSuccess = true;
					break;
				}
					
				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
					if (!neighbour.walkable || closedSet.Contains(neighbour)) {
						continue;
					}

					int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
					if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
						neighbour.gCost = newMovementCostToNeighbour;
						neighbour.hCost = GetDistance(neighbour, targetNode);
						neighbour.parent = currentNode;

						if (!openSet.Contains(neighbour))
							openSet.Add(neighbour);
						else 
							openSet.UpdateItem(neighbour);
					}
				}
			}
		}
		//add this
		if (pathSuccess) {
			waypoints = RetracePath(startNode,targetNode);
		}

		return waypoints;

	} //----

	int TurningCost(Node from, Node to) {
		return 0;
	}

	//modify this M6
	//public void RetracePath(Node startNode, Node endNode) {
	Vector2[] RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		//modify this M6
		/*path.Reverse ();
		grid.path = path;*/
		//to this
		Vector2[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;
	}

	//add SimplifyPath function M6
	Vector2[] SimplifyPath(List<Node> path) {
		List<Vector2> waypoints = new List<Vector2>();
		Vector2 directionOld = Vector2.zero;

		for (int i = 1; i < path.Count; i ++) {
			Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX,path[i-1].gridY - path[i].gridY);
			if (directionNew != directionOld) {
				waypoints.Add(path[i].worldPosition);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray();
	}

	public int GetDistance(Node nodeA, Node nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}
}
