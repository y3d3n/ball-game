﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] platforms;
    public GameObject counter;
    public GameObject counterEnableTrigger;
    public GameObject player;

    private Vector3 nextSpawnPoint;
    public int platformSize=3;
    public Transform[] spawnPositions;
    private int stage = 1;
    private int totalStage = 3;

    private int currentLevel;

    private void Awake()
    {

        if (PlayerPrefs.HasKey("CL")) //CL= Current Level;
        {
            currentLevel = PlayerPrefs.GetInt("CL");
        }
        else
        {
            currentLevel = 1;
            PlayerPrefs.SetInt("CL", 1);
        }

        totalStage = manageStages();
    }

    public void SaveLevel(int level, int sceneIndex)
    {
        PlayerPrefs.SetInt("CL", level);
        PlayerPrefs.SetInt("CS", sceneIndex);
    }

    public int Getlevel()
    {
        return currentLevel;
    }

    public void Start()
    {
        StartCoroutine(UIManager.Instance.DeactiveStage(totalStage));
        CreateLevel();
    }

    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (stage < totalStage+1)
            {
                CreateLevel();
            }
            else
            {
                NextLevel();
            }
        }

        for(int i=0; i<Input.touchCount; ++i)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (stage < totalStage + 1)
                {
                    CreateLevel();
                }
                else
                {
                    NextLevel();
                }
            }
        }
    }

    public void CreateLevel()
    {
        Transform newSpawnPoint = spawnPositions[stage].transform;
        Generate(newSpawnPoint, 6); //reset size every next level
        if (stage > 1)
        {
            UIManager.Instance.ChangeStageUI(stage);

            spawnPositions[stage - 1].gameObject.SetActive(false);
        }
        stage++;
    }

    //Generate Platforms in a specific position and size.
    public void Generate(Transform firstPosition, int size)
    {
        Transform newParent = firstPosition;
        int i = 0;
        int firstPlatform = Random.Range(0, platforms.Length);
        GameObject tempObject = Instantiate(platforms[firstPlatform], firstPosition.transform.position, Quaternion.identity) as GameObject;
        
        //parrent it to its is spawn point
        tempObject.transform.parent = newParent;

        nextSpawnPoint = tempObject.gameObject.transform.GetChild(1).position;
        Vector3 tempPosition = new Vector3(nextSpawnPoint.x, nextSpawnPoint.y, nextSpawnPoint.z);

        //Player needs to get spawn at the first platform;
        GameObject newPlayer = Instantiate(player, tempObject.gameObject.transform.GetChild(3).position, Quaternion.identity) as GameObject;
        //parrent it to its is spawn point
        newPlayer.transform.parent = newParent;

        //Retarget Camera to player;
        CameraBehaviour._camera.SetNewTarget(newPlayer);

        for (int j=0; j<size-1; j++)
        {
            i = Random.Range(0, platforms.Length);
            GameObject clone = Instantiate(platforms[i], tempPosition, Quaternion.identity) as GameObject;
            //parrent it to its is spawn point
            clone.transform.parent = newParent;

            nextSpawnPoint = clone.gameObject.transform.GetChild(1).position;
            tempPosition = new Vector3(nextSpawnPoint.x, nextSpawnPoint.y, nextSpawnPoint.z);
        }

        // Instantiate The Counter at the end of the level
        GameObject counterClone = Instantiate(counter, tempPosition, Quaternion.identity) as GameObject;

        //parrent it to its is spawn point
        counterClone.transform.parent = newParent;

        ScoreManager.Instance.counterCS = counterClone.GetComponent<ObjCounter>();

        // One Trigger is required at the end of the platform to enable counting the balls in a score manager
        Vector3 counterTriggerPosition = tempPosition;
        counterTriggerPosition.x = counterTriggerPosition.x + 1.5f;
        GameObject counterTrigger = Instantiate(counterEnableTrigger, counterTriggerPosition, Quaternion.identity) as GameObject;

        //parrent it to its is spawn point
        counterTrigger.transform.parent = newParent;
    }

    public void NextLevel()
    {
        int s;
        if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {

            s = SceneManager.GetActiveScene().buildIndex + 1;
        }
        else
        {
            s = 0;
        }

        stage = 1;
        currentLevel++;
        SaveLevel(currentLevel, s);

        SceneManager.LoadSceneAsync(s);
        Debug.Log("Current Level: " + currentLevel);
    }

    private int manageStages()
    {
        int i = 1;
        if (currentLevel >= 1 && currentLevel < 5)
        {
            i = 5;
        }
        else if(currentLevel >= 5 && currentLevel < 10)
        {
            i = Random.Range(3, 5);
        }
        else if(currentLevel >= 10 && currentLevel < 20)
        {
            i = Random.Range(3, 6);
        }
        else if(currentLevel >= 20 )
        {
            i = Random.Range(3, 7);
        }

        return i;
    }
}
