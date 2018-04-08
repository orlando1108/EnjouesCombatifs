using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkPool : MonoBehaviour {


    /* pool de particle qui fonctionne pour la creation des instances
     * permet de generer plusieurs particules d'etincelles à reutiliser dans les triggers des collisions
     * */
    public int count;
    public GameObject prefab;
    private static int lastSelected = 0;
    private static GameObject[] instances;

    
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
	
	public static GameObject activateSparkParticle(Vector3 position) {
        for (int i = 0; i < instances.Length; i ++ )
        {
            if (!instances[i].activeSelf)
            {
                lastSelected = i;
                instances[i].SetActive(true);
                instances[i].transform.position = position;
                return instances[i];
            }
        }
        return null;
		
	}

    public static void Destroy(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
