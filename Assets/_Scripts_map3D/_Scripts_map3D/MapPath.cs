using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPath : MonoBehaviour {

	public Stack<int> StackNodes;
	public Stack<string> StackNodesString;
	public int[] ArrayNodes;
	public bool redWayBool = false;
	public int PathLength;

	
	private void Start()
	{
		StackNodes = new Stack<int>();
		StackNodesString = new Stack<string>();

		string thisname = "n" + this.name;
		this.GetComponentInChildren<TextMesh>().text = "" + PathLength;

		StartCoroutine(stackToArrayEnumerator());
	}
	
	public void MakePathRed()
	{
		redWayBool = !redWayBool;
		if (redWayBool == true)
		{
			gameObject.GetComponent<Renderer>().material.color = Color.red;
		}
	}

	

	private void OnTriggerEnter(Collider other)
	{
		StackNodesString.Push(other.gameObject.name);
		//   Пример:   Преобразовать "Node (34)" в 34 .
		if (other.gameObject.name != "Node")
		{
			string StringNode = other.gameObject.name;
			StringNode = StringNode.Substring(5);
			StringNode = StringNode.Trim(new char[] {'(', ')'});
			int NumNode = Int32.Parse(StringNode);
			StackNodes.Push(NumNode);
		}
		else
		{
			StackNodes.Push(0);
		}
	}

	IEnumerator stackToArrayEnumerator()
	{
		yield return new WaitForSeconds(1);		
		ArrayNodes = new int[StackNodes.Count];
		StackNodes.CopyTo(ArrayNodes, 0);
	}
	
}
