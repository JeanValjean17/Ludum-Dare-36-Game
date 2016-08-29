using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformGeneratorScript : MonoBehaviour {

    public float objectsMinDistance = 5.0f;
    public float objectsMaxDistance = 10.0f;
    public float objectsMinY = -1.4f;
    public float objectsMaxY = 1.4f;
    public float objectsMinRotation = 0f;
    public float objectsMaxRotation = 0f;

    public GameObject managers;
    public GameObject[] availableObjects;


    public List<GameObject> objects;

    private float screenWidthInPoints;
    private GuiDebugManager debugGui;
    GameObject obj;

    // Use this for initialization
    void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
        debugGui = managers.GetComponent<GuiDebugManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GenerateObjectsIfRequired();
    }

    void AddObjectSet(float lastPlatformX)
    {
        int randomIndex = 0;

        //3
        randomIndex = Random.Range(0, availableObjects.Length);
        obj = Instantiate(availableObjects[randomIndex]);

        float objectPositionX = lastPlatformX + Random.Range(objectsMinDistance, objectsMaxDistance);
        float randomY = Random.Range(objectsMinY, objectsMaxY);
        obj.transform.position = new Vector3(objectPositionX, randomY, 0);

        //4
        float rotation = Random.Range(objectsMinRotation, objectsMaxRotation);
        obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);

        //5
        objects.Add(obj);
    }


    void GenerateObjectsIfRequired()
    {
        //1
        float playerX = transform.position.x;
        float removeObjectsX = playerX - screenWidthInPoints;
        float addObjectX = playerX + screenWidthInPoints;
        float farthestObjectX = 0;

        //2
        List<GameObject> objectsToRemove = new List<GameObject>();

        foreach (GameObject obj in objects)
        {
            //3
            if(obj != null)
            {

                float objX = obj.transform.position.x;

                //4
                farthestObjectX = Mathf.Max(farthestObjectX, objX);

                //5
                if (objX < removeObjectsX)
                    objectsToRemove.Add(obj);
            }
        }

        //6
        foreach (var obj in objectsToRemove)
        {
            objects.Remove(obj);
            Destroy(obj);
        }

        //7
        if (farthestObjectX < addObjectX)
            AddObjectSet(farthestObjectX);
    }
}
