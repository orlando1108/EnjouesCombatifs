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
        cout = -1; 
        heuristique = h;
        destinations = null;
    }
}

public class IaManager2 : MonoBehaviour {

    public List<Transform> transformList;

    private List<Node> nodeList = new List<Node> { };

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

    /// <summary>
    /// Implémentation du pathfinding astar pour le parcours de la voiture concurente. Cette méthode permet de déterminer le chemin de plus rapide pour la voiture concurente autonome.
    /// Un certain nombre de points de passages sont disposés tout au long du circuit pour guider la voiture autour du circuit. Cependant certains points de passages peuvent êtres obstrués par 
    /// des obstacles. Ce code permet donc de déterminer à la fois le chemin libre mais aussi le chemin de plus rapide autour du circuit. 
    /// </summary>
    /// <param name="startNode"></param>
    /// <param name="endNode"></param>
    /// <param name="close"></param>
    /// <param name="open"></param>
    /// <returns></returns>
    public static List<Transform> astar(Node startNode, Node endNode, List<Node> close, List<Node> open)
    {
        open.Add(startNode);
        float minOpen = open.Min(item => item.cout);
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
        int count2 = 0;
        //Boucle qui cherche le chemin le plus rapide jusqu'au point final 
        while (minOpen < minClose && count2 < 100)
        {
            count2++;
            //Si tout les neuds sont éxplorés, le pathfinding est terminé, alors je termine en allant récupèrer le chemin le plus court pour le retourner 
            if (open.Count == 0)
            {
                List<Transform> result2 = new List<Transform> { endNode.point };//Liste qui contiendra le chemin le plus rapide
                List<Node> endListNode2 = close.Where(x => x.name == endNode.name).ToList();//Liste qui contien les points "finaux" du chemin

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
                //Boucle permetant de récupèrer les parents des différents point du chemin le plus court 
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
            //Je récupère le noeud le plus proche et le moins couteux
            float min = open.Min(item => item.cout + item.heuristique);
            var nodeToExplore = open.Where(item => item.cout + item.heuristique == min).First();
            open.Remove(nodeToExplore);

            close.Add(nodeToExplore);

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
            //Exploration de toutes les destinations du noeud choisi
            foreach (Destination myDest in nodeToExplore.destinations)
            {
                //Vérification que le noeud n'est pas désactivé (obstacles sur le circuit)
                if (!myDest.node.isActive)
                {
                    continue;
                }
                //Instantiation d'un nouvel objet afin de ne pas créer de dépendances (références d'objets)
                Destination newDest = myDest;
                newDest.node = new Node(newDest.node.name, newDest.node.heuristique);

                newDest.node.cout = myDest.distance + nodeToExplore.cout;
                newDest.node.destinations = myDest.node.destinations;
                newDest.node.point = myDest.node.point;


                newDest.node.parent = nodeToExplore;

                destArray[count] = newDest;
                //Condition qui permet d'ajouter uniquement les points qui potentiellement seraient plus court (performance)
                var unique = open.FirstOrDefault(x => x.name == newDest.node.name);
                if (unique != null)
                {
                    if (unique.cout + unique.heuristique > newDest.node.cout + newDest.node.heuristique)
                    {
                        open.Remove(unique);
                        open.Add(newDest.node);
                    }
                }
                if (unique == null)
                {
                    open.Add(newDest.node);
                }
                count++;
            }
            nodeToExplore.destinations = destArray;

            //Je détermine si il est nécéssaire de continuer la recherche d'un chemin en fonction du cout des points traités et en attente (open vs close)
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
        //Si il n'y a plus de raison de continuer alors de retourne le chemin le plus court 
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
