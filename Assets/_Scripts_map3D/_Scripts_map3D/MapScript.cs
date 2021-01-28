using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using _Scripts;

public class MapScript : MonoBehaviour
{
	// Основа
	public Queue<string> QueueNodeNames;
	public string NodeName;
	public int NodeCount = 1;
	public string[] ArrayNodeNames;
	public int[,] Matrix;
	public int intMatrixRank;
	public int[][] MatrixPathNames;

	public int[] MainParentPath;
	public int[] MainParentsNode ;
	public int[] MainDistationNode ;
	string wayBfsString = "";
	
	private void Start()
	{
		
		QueueNodeNames = new Queue<string>();
		NodeName = "Node";

		if (GameObject.Find(NodeName) == true)
		{
			QueueNodeNames.Enqueue(NodeName);
			StartCoroutine(FindNodesCEnumerator());
		}

		
	}

	IEnumerator FindNodesCEnumerator()
	{
		yield return new WaitForSeconds(1.1f);
		FindNode();

///////////
		yield return new WaitForSeconds(1.1f);
		intMatrixRank = ArrayNodeNames.Length;
		Matrix = new int[intMatrixRank, intMatrixRank];
		MainParentsNode = new int[intMatrixRank];
		MainParentPath = new int[intMatrixRank];
		MainDistationNode = new int[intMatrixRank];
		MatrixPathNames = new int[intMatrixRank][];
		
		// Заполение нулями( 0 )
		for (int i = 0; i < intMatrixRank; i++)
		{
			for (int j = 0; j < intMatrixRank; j++)
			{
				Matrix[i, j] = 0;
			}
		}

		// Заполение значениями
		for (int i = 0; i < intMatrixRank; i++)
		{
			int MatrixNodeIndex = GameObject.Find(ArrayNodeNames[i]).GetComponent<MapNode>().ThisNodeInt;
			int[] MatrixArrayNodes = new int[GameObject.Find(ArrayNodeNames[i]).GetComponent<MapNode>().ArrayNodes.Length];
			int[] MatrixArrayPath = new int[GameObject.Find(ArrayNodeNames[i]).GetComponent<MapNode>().ArrayLengthToNodes.Length];
			MatrixPathNames[i] = new int[GameObject.Find(ArrayNodeNames[i]).GetComponent<MapNode>().ArrayPaths.Length];
			
			GameObject.Find(ArrayNodeNames[i]).GetComponent<MapNode>().ArrayNodes.CopyTo(MatrixArrayNodes, 0);
			GameObject.Find(ArrayNodeNames[i]).GetComponent<MapNode>().ArrayLengthToNodes.CopyTo(MatrixArrayPath, 0);
			yield return new WaitForSeconds(1);
			GameObject.Find(ArrayNodeNames[i]).GetComponent<MapNode>().ArrayPaths.CopyTo(MatrixPathNames[i], 0);

			for (int j = 0; j < MatrixArrayNodes.Length; j++)
			{
				Matrix[MatrixNodeIndex, MatrixArrayNodes[j]] = MatrixArrayPath[j];
				Matrix[MatrixArrayNodes[j], MatrixNodeIndex] = MatrixArrayPath[j];
			}
		}

		// Отображение Матрицы смежностей
//		yield return new WaitForSeconds(1);
//		Debug.Log("** MATRIX **");
//		for (int i = 0; i < intMatrixRank; i++)
//		{
//			string Stroke = "Точка: " + i + " | ";
//			for (int j = 0; j < intMatrixRank; j++)
//			{
//				Stroke += ("__" + Matrix[i, j]);
//			}
//
//			Debug.Log(Stroke);
//		}
		Debug.Log("Матрица готова.");
	

		BFS(8);
		Debug.Log("Расстояние до точки [ " + 6 + " ] = " + MainDistationNode[6]);
		PrintNodeBFS(6);
		Debug.Log(wayBfsString);

//////////
	}

	private void FindNode()
	{
		NodeName = "Node (" + NodeCount + ")";
		if (GameObject.Find(NodeName) == true)
		{
			QueueNodeNames.Enqueue(NodeName);
			NodeCount += 1;
			FindNode();
		}
		else
		{
			ArrayNodeNames = new string[QueueNodeNames.Count];
			QueueNodeNames.CopyTo(ArrayNodeNames, 0);
			Debug.Log("Out of Nodes !");
		}
	}

	// BFS start

	private void BFS(int uNode)
	{
		bool[] usedNode = new bool[intMatrixRank];
		usedNode[uNode] = true;
		int[] parentsNode = new int[intMatrixRank];
		parentsNode[uNode] = uNode;
		int[] distationNode = new int[intMatrixRank];
		
		int[] parentsPath = new int[intMatrixRank];
		parentsPath[uNode] = 1488322;
		
		for (int i = 0; i < distationNode.Length; i++)
			distationNode[i] = 99999999;
		distationNode[uNode] = 0;

		HeapMin waitingsNodes = new HeapMin();
		waitingsNodes.Add(uNode);

		while (waitingsNodes.GetHeapSize != 0 )
		{
			int uNumberNode = waitingsNodes.GetMin();

			if (uNumberNode > 0)
			{
				for (int i = 0; i < GameObject.Find("Node (" + uNumberNode + ")").GetComponent<MapNode>().ArrayNodes.Length; i++)
				{
					int neibourNode = GameObject.Find("Node (" + uNumberNode + ")").GetComponent<MapNode>().ArrayNodes[i];
					int neibourPath = GameObject.Find("Node (" + uNumberNode + ")").GetComponent<MapNode>().ArrayLengthToNodes[i];
					int neibourWay = MatrixPathNames[uNumberNode][i];

					if (distationNode[neibourNode] > distationNode[uNumberNode] + neibourPath)
					{
						parentsPath[neibourNode] = neibourWay;																
						parentsNode[neibourNode] = uNumberNode;
						distationNode[neibourNode] = distationNode[uNumberNode] + neibourPath;
						waitingsNodes.Add(neibourNode);					
					}
				}
			}

			else if (uNumberNode == 0)                																		
			{
				for (int i = 0; i < GameObject.Find("Node").GetComponent<MapNode>().ArrayNodes.Length; i++)
				{
					int neibourNode = GameObject.Find("Node").GetComponent<MapNode>().ArrayNodes[i];
					int neibourPath = GameObject.Find("Node").GetComponent<MapNode>().ArrayLengthToNodes[i];
					int neibourWay = MatrixPathNames[uNumberNode][i];

					if (distationNode[neibourNode] > distationNode[uNumberNode] + neibourPath)
					{
						parentsPath[neibourNode] = neibourWay;	
						parentsNode[neibourNode] = uNumberNode;
						distationNode[neibourNode] = distationNode[uNumberNode] + neibourPath;
						waitingsNodes.Add(neibourNode);
					}
				}
			}
		}
		
		parentsNode[uNode] = uNode;
		parentsNode.CopyTo(MainParentsNode, 0);
		parentsPath.CopyTo(MainParentPath, 0);
		distationNode.CopyTo(MainDistationNode, 0);
		Debug.Log("Finish BFS");
	}


	
	private void PrintNodeBFS(int uNode)																						
	{
		if (uNode != 0)
		{
			if (MainParentPath[uNode] != 1488322)
			{
				if (MainParentPath[uNode] != 0)
				{
					GameObject.Find("Path (" + MainParentPath[uNode] + ")").GetComponent<MapPath>().MakePathRed();
				}
				else
				{
					GameObject.Find("Path").GetComponent<MapPath>().MakePathRed();
				}
			}
			
			GameObject.Find("Node (" + uNode + ")").GetComponent<MapNode>().MakeNodeRed();
			wayBfsString += uNode + " -> ";
			
			if (MainParentsNode[uNode] != uNode)
			{
				
				PrintNodeBFS(MainParentsNode[uNode]);
			}
		}
		else
		{
			if (MainParentPath[uNode] != 1488322)
			{
				if (MainParentPath[uNode] != 0)
				{
					GameObject.Find("Path (" + MainParentPath[uNode] + ")").GetComponent<MapPath>().MakePathRed();
				}
				else
				{
					GameObject.Find("Path").GetComponent<MapPath>().MakePathRed();
				}
			}
			
			GameObject.Find("Node").GetComponent<MapNode>().MakeNodeRed();
			wayBfsString += uNode + " -> ";

			if (MainParentsNode[uNode] != uNode)
			{
				
				PrintNodeBFS(MainParentsNode[uNode]);

			}
		}
	}
	
//	private void PrintWayBFS(int uPath)																						//TODO
//	{
//		if (uPath != 0)
//		{
//			GameObject.Find("Path (" + uPath + ")").GetComponent<MapPath>().MakePathRed();
//			
//			
//			if (MainParentPath[uPath] != uPath)
//			{
//				PrintWayBFS(MainParentsNode[uPath]);
//			}
//		}
//		else
//		{
//			GameObject.Find("Path").GetComponent<MapPath>().MakePathRed();
//			
//
//			if (MainParentPath[uPath] != uPath)
//			{
//				PrintWayBFS(MainParentPath[uPath]);
//
//			}
//		}
//	}

	// BFS end
	
}