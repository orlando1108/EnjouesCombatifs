using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkPool : MonoBehaviour {

    public int count;
    public GameObject prefab;
    private int lastSelected = 0;
    private GameObject[] instances;


	// Use this for initialization
	void Start () {
        instances = new GameObject[count];
        for(int i =0; i<count; i++)
        {
            var instance = Instantiate(prefab);
            instance.SetActive(false);
            instance.transform.parent = this.transform;
            instances[i] = instance;


        }
		
	}
	
	// Update is called once per frame
	public GameObject activateSparkParticle(Vector3 position) {
        for (int i = 0; i < instances.Length; i ++ )
        {
            Debug.Log("FOR    " + instances[i]);
            if (!instances[i].activeSelf)
            {
                Debug.Log("ACTIVE SELF    " + instances[i]);
                lastSelected = i;
                instances[i].SetActive(true);
                instances[i].transform.position = position;
                Debug.Log("INSTANCE   " + instances[i]);
                return instances[i];
            }
        }
        return null;
		
	}

    public void Destroy(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
