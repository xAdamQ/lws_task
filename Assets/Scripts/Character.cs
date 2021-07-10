using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

[RequireComponent(typeof(NavMeshAgent))]
public class Character : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private static readonly int poseHash = Animator.StringToHash("pose");

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            agent.destination = Camera.main.ScreenToWorldPoint(Input.mousePosition.SetZ(10));
        }

        var vel = agent.velocity;

        //0: idle
        //1: move right
        //2: move left
        //3: move up
        //4: move down

        var pose = 0;

        if (!vel.x.IsAbout(0, .05f) || !vel.y.IsAbout(0, .05f))
            if (Math.Abs(vel.x) > Math.Abs(vel.y))
                pose = vel.x > 0 ? 1 : 2;
            else
                pose = vel.y > 0 ? 3 : 4;

        anim.SetInteger(poseHash, pose);
    }
}