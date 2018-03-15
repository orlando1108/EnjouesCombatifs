using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Destination
{
    public Node node;
    public float distance;
    public Destination(Node n, float d)
    {
        node = n;
        distance = d;
    }
}

public class Node
{
    public Transform point;
    public string name;
    public float cout;
    public float heuristique;
    public Node parent;
    public Destination[] destinations;
    public bool isActive;

    public Node(string n, float h)
    {
        name = n;
        cout = -1; // cout cumulé à mettre à jour lors de l'exploration
        heuristique = h;
        destinations = null;
    }
}

public class IaManager2 : MonoBehaviour {

    public List<Transform> transformList;

    private List<Node> nodeList = new List<Node> { };

	// Use this for initialization
	void Start () {
        int i = 0;
        foreach(var x in transformList)
        {
            Node localNode = new Node(x.name, 1);
            localNode.point = x;      
            localNode.cout = 1;
            nodeList.Add(localNode);
            i++;
        }

        int j = 1;
        foreach (var x in nodeList)
        {
            if(j < 9)
            {
                Destination[] dest = new Destination[] { new Destination(nodeList[j], 1) };
                x.destinations = dest;
            }
            j++;
        }
        List<Node> close = new List<Node> { };
        List<Node> open = new List<Node> { };
        /*var res = astar(nodeList[0], nodeList[8], new List<Node>(), new List<Node>());
        foreach (var iterate in res)
        {
            Debug.Log(iterate.name);
        }*/
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public static Node getParent(Node enterNode)
    {
        try
        {
            return enterNode.parent;
        }
        catch
        {
            return null;
        }
    }

    public static List<Transform> astar(Node startNode, Node endNode, List<Node> close, List<Node> open)
    {
        open.Add(startNode);
        float minOpen = open.Min(item => item.cout);
        Node test = close.Where(x => x.name == endNode.name).FirstOrDefault();
        List<Node> mesNodes = close.Where(x => x.name == endNode.name).ToList();
        float minClose;
        if (mesNodes.Count() != 0)
        {
            minClose = mesNodes.Min(item => item.cout);
        }
        else
        {
            minClose = minOpen + 1;
        }

        while (minOpen < minClose)
        {
            if(open.Count == 0)
            {
                List<Transform> result2 = new List<Transform> { endNode.point };
                List<Node> endListNode2 = close.Where(x => x.name == endNode.name).ToList();

                Node finalNode2;
                
                if (endListNode2.Count == 1)
                {
                    finalNode2 = endListNode2[0];
                }
                else
                {
                    float minCloseNode2 = endListNode2.Min(item => item.cout);
                    finalNode2 = endListNode2.Where(x => x.cout == minCloseNode2).FirstOrDefault();
                }

                while (true)
                {
                    if (getParent(finalNode2) != null)
                    {
                        result2.Add(getParent(finalNode2).point);
                        finalNode2 = getParent(finalNode2);
                    }
                    else
                    {
                        result2.Reverse();
                        return result2;
                    }
                }
            }
            float min = open.Min(item => item.cout + item.heuristique);
            var nodeToExplore = open.Where(item => item.cout + item.heuristique == min).First();
            open.Remove(nodeToExplore);

            close.Add(nodeToExplore);
            foreach(var tt in close)
            {
                Debug.Log("close"+tt.name);
            }
            
            if (nodeToExplore.name == endNode.name)
            {
                continue;
            }
            if (nodeToExplore.destinations == null)
            {
                continue;
            }
            Destination[] destArray = new Destination[nodeToExplore.destinations.Length];
            int count = 0;
            foreach (Destination myDest in nodeToExplore.destinations)
            {
                Destination newDest = myDest;
                newDest.node = new Node(newDest.node.name, newDest.node.heuristique);
                newDest.node.cout = myDest.distance + nodeToExplore.cout;
                newDest.node.destinations = myDest.node.destinations;
                newDest.node.point = myDest.node.point;

                newDest.node.parent = nodeToExplore;

                destArray[count] = newDest;
                
                var unique = open.Where(x => x.cout == newDest.node.cout && x.name == newDest.node.name && x.parent == newDest.node.parent);
                //Debug.Log("test " + newDest.node.name);
                if (unique.Count() == 0)
                {
                    open.Add(newDest.node);
                    //Debug.Log("open name" + newDest.node.name +" "+open.Count);
                }
                count++;
            }
            nodeToExplore.destinations = destArray;

            float minNodeOpen = open.Min(item => item.cout + item.heuristique);
            var littleNodeOpen = open.Where(item => item.cout + item.heuristique == minNodeOpen).First();
            minOpen = littleNodeOpen.cout + littleNodeOpen.heuristique;

            var minTest = close.Where(x => x.name == endNode.name);
            if (minTest.Count() > 0)
            {
                float minNodeClose = minTest.Min(item => item.cout);
                var littleNodeClose = minTest.Where(item => item.cout == minNodeClose).First();
                minClose = littleNodeClose.cout;
            }
            else
            {
                minClose = minOpen;
                minClose++;
            }
        }

        List<Transform> result = new List<Transform> { endNode.point };
        List<Node> endListNode = close.Where(x => x.name == endNode.name).ToList();

        float minCloseNode = endListNode.Min(item => item.cout);
        Node finalNode = endListNode.Where(x => x.cout == minCloseNode).FirstOrDefault();

        while (true)
        {
            if (getParent(finalNode) != null)
            {
                result.Add(getParent(finalNode).point);
                finalNode = getParent(finalNode);
            }
            else
            {
                result.Reverse();
                return result;
            }
        }
    }
}
