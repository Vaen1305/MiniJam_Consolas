using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public  List<NodeController> adjacentNodes = new List<NodeController>();
    public int index;

    void Start()
    {

    }
    public void AddAdjacentNode(NodeController node)
    {
        adjacentNodes.Add(node);
    }
    public NodeController SelecRandomAdjancent()
    {
        index = Random.Range(0, adjacentNodes.Count);
        return adjacentNodes[index];
    }
    // Update is called once per frame
    void Update()
    {

    }

}
