﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    public int roomCount;
    public int pRoomCount;
    public SpaceCheck[] activeRooms;
    public List<GameObject> spawnedRooms = new List<GameObject>();
    public GameObject builtRoom;
    public bool levelComplete = true;
    public bool levelWorks;
    public bool levelBuilt;
    public Animator loadingScreen;
    public Animator loadingLogo;
    public GameManager gm;
    public NavMeshSurface surface;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Invoke("checkActiveSpawners", 3.5f);
    }
    

    public void checkActiveSpawners()
    {
        /*activeSpawners = FindObjectsOfType<RoomSpawner>();
        Debug.Log("filling array");
        foreach (RoomSpawner spawner in activeSpawners)
        {
            if (!spawner.spawned)
            {
                levelComplete = false;
            }
        }
        
        Debug.Log("checked array");*/
        if (/*levelComplete &&*/ pRoomCount == 3 && roomCount < 20)
        {
            levelWorks = true;
            //Debug.Log("PERFECT");
            //fill rooms
            activeRooms = FindObjectsOfType<SpaceCheck>();
            foreach (SpaceCheck tempRoom in activeRooms)
            {
                builtRoom = Instantiate(tempRoom.myRoom, tempRoom.transform.position, tempRoom.transform.rotation);
                spawnedRooms.Add(builtRoom);
                //add built room to spawned rooms array
                Destroy(tempRoom.gameObject);
                Debug.Log("Generated Room");
            }
            surface.BuildNavMesh();
            Debug.Log("NAVMESH BUILT");
            //have each room in spawned rooms array activate their AI
            foreach (GameObject room in spawnedRooms)
            {
                if (room.GetComponent<BuiltRoom>() != null)
                {
                    room.GetComponent<BuiltRoom>().ActivateAI();
                    Debug.Log("Activated AI");
                }
                else
                    Debug.Log("No builtroom script");

            }
            loadingScreen.SetTrigger("Load1");
            loadingLogo.SetTrigger("Load2");
            levelBuilt = true;

        }
        else if (/*levelComplete &&*/ pRoomCount < 3 || roomCount >= 20)
        {
            //restart
            //Debug.Log("RESTART");
            //reload level?
            gm.KeyCount = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        /*else if(!levelComplete)
        {
            levelComplete = true;
            Debug.Log("RESET");
        }
        else
        {
            Debug.Log("DUNNO LOLOL");
        }*/
    }

}
