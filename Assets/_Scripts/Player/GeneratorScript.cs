using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GeneratorScript : MonoBehaviour
{  

    public float objectsMinDistance = 5.0f;
    public float objectsMaxDistance = 10.0f;
    //public float objectsMinY = -1.4f;
    public float objectsMaxY = 1.4f;
    public float objectsMinRotation = 0f;
    public float objectsMaxRotation = 0f;

    public GameObject managers, enemy;   
    public GameObject[] availableRooms;
    public List<GameObject> currentRooms;

    public GameObject[] availableObjects;
    public List<GameObject> objects;

    private float screenWidthInPoints;
    private GuiDebugManager debugGui;
    private float addObjectX, addRoomX;
    private GameManager gameManager;
    GameObject obj;
        

    // Use this for initialization
    void Start ()
    {
        gameManager = GameManager.Instance;
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
        debugGui = managers.GetComponent<GuiDebugManager>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (enemy != null)
        {
            GenerateRoomIfRequired();
            GenerateObjectsIfRequired();
        }          
    }

    void AddRoom(float farhtestRoomEndX)
    {
        int randomRoomIndex = Random.Range(0, availableRooms.Length);

        GameObject room = Instantiate(availableRooms[randomRoomIndex]);

        float roomWidth = room.transform.FindChild("Floor").localScale.x;
        float roomCenter = farhtestRoomEndX + roomWidth * 0.5f;
        room.transform.position = new Vector3(roomCenter, 0, 0);

        currentRooms.Add(room);
    }

    void GenerateRoomIfRequired()
    {
        List<GameObject> roomsToRemove = new List<GameObject>();
        bool addRooms = true;

        float enemyX = enemy.transform.position.x;
        float playerX = transform.position.x;
        //debugGui.PrintDebug("PlayerPosition : " + playerX.ToString(), 0);

        float removeRoomX = playerX - screenWidthInPoints;
        //debugGui.PrintDebug("removeRoomX : " + removeRoomX.ToString(), 5);
        if (enemyX > playerX)
        {
            addRoomX = enemyX + screenWidthInPoints;
        }
        else
        {
            addRoomX = playerX + screenWidthInPoints;
        }
        
        //debugGui.PrintDebug("addRoomX : " + addRoomX.ToString(), 4);

        float farthestRoomEndX = 0;

        foreach (GameObject room in currentRooms)
        {
            
            float roomWidth = room.transform.FindChild("Floor").localScale.x;
            //debugGui.PrintDebug("roomWidth : " + roomWidth.ToString(), 1);

            float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
            //debugGui.PrintDebug("roomStartX : " + roomStartX.ToString(), 2);

            float roomEndX = roomStartX + roomWidth;
            //debugGui.PrintDebug("roomEndX : " + roomEndX.ToString(), 3);

            if (roomStartX > addRoomX)
                addRooms = false;

            if (roomEndX < removeRoomX)
                roomsToRemove.Add(room);

            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }

        foreach (var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
        }

        if (addRooms)
            AddRoom(farthestRoomEndX);
    }

    void AddObjectSet(float lastPlatformX)
    {
        int randomIndex = 0;

        //3
        randomIndex = Random.Range(0, availableObjects.Length);
        obj = Instantiate(availableObjects[randomIndex]);

        float objectPositionX = lastPlatformX + Random.Range(objectsMinDistance, objectsMaxDistance);
        float randomY = Random.Range(1.4f, objectsMaxY);
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
        float enemyX = enemy.transform.position.x;
        float playerX = transform.position.x;
        float removeObjectsX = playerX - screenWidthInPoints;

        if (transform.position.x < gameManager.goal)
        {
            if (enemyX > playerX)
            {
                addObjectX = enemyX + screenWidthInPoints;
            } else
            {
                addObjectX = playerX + screenWidthInPoints;
            }
        }

        float farthestObjectX = 0;

        //2
        List<GameObject> objectsToRemove = new List<GameObject>();

        foreach (GameObject obj in objects)
        {
            //3
            if (obj != null)
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

        if (transform.position.x < gameManager.goal)
        {
            //7
            if (farthestObjectX < addObjectX)
            AddObjectSet(farthestObjectX);
        }
    }

}
