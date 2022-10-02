using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefabs : MonoBehaviour
{
    [SerializeField] private uint numberOfPrefabs;
    [SerializeField] private float radiusOfCircle = 1;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform centerOfCircle;
    List<GameObject> generatedObjects = new List<GameObject>();
    private float x;
    private float y;
    private float z;
    private float radiusForPrefab = 0;

    // Start is called before the first frame update

    void Start()
    {
        InstantiateObjects();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationToAdd = new Vector3(0, 0, 30) * Time.deltaTime;
        centerOfCircle.Rotate(rotationToAdd);
    }

    void InstantiateObjects()
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            x = radiusOfCircle * Mathf.Sin(Mathf.PI * 2 * radiusForPrefab / 360) + centerOfCircle.position.x;
            y = radiusOfCircle * Mathf.Cos(Mathf.PI * 2 * radiusForPrefab / 360) + centerOfCircle.position.y;
            z = centerOfCircle.position.z;
            generatedObjects.Add(Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity, centerOfCircle));
            radiusForPrefab += 360 / (float)numberOfPrefabs;
        }
    }

    void DestroyObjects()
    {
        foreach (var obj in generatedObjects)
        {
            DestroyImmediate(obj);
        }
    }

    void OnValidate()
    {

        /*if(generatedObjects.Count != 0)DestroyObjects();
        */
        if(UnityEditor.EditorApplication.isPlaying)
        { 
            UnityEditor.EditorApplication.delayCall += () =>
            {
                DestroyObjects();
                InstantiateObjects();

            };
        }
        
        
        //Invoke("InstantiateObjects",2f);

        //Debug.Log("test");
    }
}
