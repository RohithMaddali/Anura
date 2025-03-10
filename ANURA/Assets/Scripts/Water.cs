﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    GameObject[] enemies;
    public float toadHearingDistance = 10f;
    //assign appropriate particle effect
    public GameObject splashLocation;
    public ParticleSystem splash;
    bool splashy = false;
    private PlayerMovement playerMovement;

    // perform attack on target  
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Toad");
        StartCoroutine(Splashtime());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            Debug.Log("splash splash");
            //play the particle effect at the players location
            splashLocation.transform.position = new Vector3(other.gameObject.transform.position.x, this.gameObject.transform.position.y, other.gameObject.transform.position.z);
            playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement.isMoving)
            {
                splashy = true;
            }
            else
            {
                splashy = false;
            }

            foreach (GameObject toad in enemies)
            {
                float distance = Vector3.Distance(toad.transform.position, other.transform.position);
                if (distance < toadHearingDistance)
                {
                    //toad.GetComponent<Patroller>().patrolling = false;
                    //toad looks at and moves to player position
                    toad.GetComponent<Patroller>().lastKnownPos = other.transform.position;
                    //coroutine to delay movment?
                    //toad.GetComponent<Patroller>().investigating = true;
                    toad.GetComponent<Patroller>().action = Patroller.Behaviour.investigating;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            splashy = false;
        }
    }

    IEnumerator Splashtime()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(0.1f);
            if (splashy == true)
            {
                
                Instantiate(splash, splashLocation.transform.position, splashLocation.transform.rotation);
            }
        }
    }
}
