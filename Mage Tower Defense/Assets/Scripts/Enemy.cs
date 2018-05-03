using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    NavMeshAgent pathfinder;
    Transform target;
	// Use this for initialization
	void Start () {
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Castle").transform;
	}
	
	// Update is called once per frame
	void Update () {
        pathfinder.SetDestination(target.position);
	}
}
