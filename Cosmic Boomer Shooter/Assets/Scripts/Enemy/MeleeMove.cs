using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent nma;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        nma.SetDestination(player.transform.position);
    }
}
