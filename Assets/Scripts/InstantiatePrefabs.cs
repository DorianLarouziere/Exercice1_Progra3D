using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefabs : MonoBehaviour
{
    [SerializeField] private uint numberOfPrefabs;
    [SerializeField] private float radiusOfCircle = 1;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform centerOfCircle = null;
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
        if(centerOfCircle != null)
        {
            Vector3 rotationToAdd = new Vector3(0, 0, 30) * Time.deltaTime;
            centerOfCircle.Rotate(rotationToAdd);
        }
    }

    void InstantiateObjects()
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            x = radiusOfCircle * Mathf.Sin(Mathf.PI * 2 * radiusForPrefab / 360);
            y = radiusOfCircle * Mathf.Cos(Mathf.PI * 2 * radiusForPrefab / 360);
            z = 0;
            if(centerOfCircle != null)
            {
                x += centerOfCircle.position.x;
                y += centerOfCircle.position.y;
                z += centerOfCircle.position.z;
                generatedObjects.Add(Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity, centerOfCircle));
            }

            else
            {
                generatedObjects.Add(Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity));
            }
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

    //Méthode appelée quand une variable est modifiée au niveau de l'inspector
    void OnValidate()
    {
        if(UnityEditor.EditorApplication.isPlaying)
        { 
            UnityEditor.EditorApplication.delayCall += () =>
            {
                DestroyObjects();
                InstantiateObjects();

            };
        }
    }
}
