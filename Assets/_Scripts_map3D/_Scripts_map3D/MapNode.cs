using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{

	public Stack<string> StackPaths;
	public Stack<int> StackPathsSave;  					
	public Stack<int> StackNodes;
	public Stack<int> StackLengthToNodes;
	public int[] ArrayNodes;
	public int[] ArrayPaths;
	public int[] ArrayLengthToNodes;
	public string ThisNodeString;
	public int ThisNodeInt;
	public bool redWayBool;

	
	
	private void Start()
	{
		StackPaths = new Stack<string>();
		StackPathsSave = new Stack<int>();
		StackNodes = new Stack<int>();
		StackLengthToNodes = new Stack<int>();
		ThisNodeString = this.name;
		
		if (ThisNodeString != "Node")
		{
			ThisNodeString = ThisNodeString.Substring(5);
			ThisNodeString = ThisNodeString.Trim(new char[] {'(', ')'});
			ThisNodeInt = Int32.Parse(ThisNodeString);
		}
		else
		{
			ThisNodeInt = 0;
		}

		StartCoroutine(PathsEnumerator());

		this.GetComponentInChildren<TextMesh>().text = "" + ThisNodeInt;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		StackPaths.Push(other.gameObject.name);
		
		
		
	}	
	
	IEnumerator PathsEnumerator()
	{
		yield return new WaitForSeconds(1.1f);

		while (StackPaths.Count > 0)
		{
			string PopPath = StackPaths.Pop();
			
			if (GameObject.Find(PopPath).GetComponent<MapPath>().ArrayNodes[0] != ThisNodeInt)
			{
				StackNodes.Push(GameObject.Find(PopPath).GetComponent<MapPath>().ArrayNodes[0]);
				StackLengthToNodes.Push(GameObject.Find(PopPath).GetComponent<MapPath>().PathLength);
				//   Пример:   Преобразовать "Node (34)" в 34 .
				if (PopPath != "Path")
				{
					string StringNode = PopPath;
					StringNode = StringNode.Substring(5);
					StringNode = StringNode.Trim(new char[] {'(', ')'});
					int NumNode = Int32.Parse(StringNode);
					StackPathsSave.Push(NumNode);
				}
				else
				{
					StackPathsSave.Push(0);
				}
			}
			else
			{
				StackNodes.Push(GameObject.Find(PopPath).GetComponent<MapPath>().ArrayNodes[1]);
				StackLengthToNodes.Push(GameObject.Find(PopPath).GetComponent<MapPath>().PathLength);
				
				if (PopPath != "Path")
				{
					string StringNode = PopPath;
					StringNode = StringNode.Substring(5);
					StringNode = StringNode.Trim(new char[] {'(', ')'});
					int NumNode = Int32.Parse(StringNode);
					StackPathsSave.Push(NumNode);
				}
				else
				{
					StackPathsSave.Push(0);
				}
			}
			
			
		}
		
		yield return new WaitForSeconds(1);

		ArrayPaths = new int[StackPathsSave.Count];
		ArrayNodes = new int[StackNodes.Count];
		ArrayLengthToNodes = new int[StackNodes.Count];
		StackNodes.CopyTo(ArrayNodes, 0);
		StackPathsSave.CopyTo(ArrayPaths, 0);
		StackLengthToNodes.CopyTo(ArrayLengthToNodes, 0);
	}

	public void MakeNodeRed()
	{
		redWayBool = !redWayBool;
		if (redWayBool == true)
		{
			gameObject.GetComponent<Renderer>().material.color = Color.red;
		}
		
	}

}
