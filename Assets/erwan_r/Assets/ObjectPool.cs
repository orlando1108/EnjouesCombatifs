using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    
        private List<GameObject> list = new List<GameObject>();

        public void Init(int count, GameObject debugPrefab)
    {
        GameObject parent = new GameObject("debugParent");

        for (int i = 0; i < count; i++)
        {
        list.Add(Instantiate(debugPrefab, parent.transform));
        }
    }

        public void Insert(GameObject debug)
    {
        debug.name = "default";
        debug.transform.position = Vector3.zero;
        debug.GetComponent<SpriteRenderer>().color = Color.white;
            
        list.Add(debug);
    }

        public GameObject Get()
    {
        GameObject next = list.First();
        list.Remove(next);
        return next;
    
    }
}
