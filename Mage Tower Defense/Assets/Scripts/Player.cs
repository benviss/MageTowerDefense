using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    //public float TargetMass = 100;
    //public float baseMoveSpeed = 7;
    public float moveSpeed = 7;
    public Transform Hand;
    //public float baseAttackRange = 1;
    //public float attackRange = 1;
    public float attackSpeed = .5f;
    public float lastAttack;

    //public float size = 1;
    //Rigidbody rb;

    //public GameObject attackConeObject;
    //public GameObject[] arms;
    //public GameObject[] legs;
    //float baseDamage = 10;

    /// <summary>
    /// This is stuff that will be moved to specific class for Force Push
    /// </summary>
    public float radius;
    public float power;
    public float coneAngle;




    Camera viewCamera;
    PlayerController controller;

    void Start()
    {
    }


    void Awake()
    {
        viewCamera = Camera.main;
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        // Movement input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        // Look input
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance)) {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);
        }

        //Attacks
        if (Input.GetMouseButton(0)) {
            if (Time.time - lastAttack > attackSpeed) {
                lastAttack = Time.time;
                ForcePush();
            }
        }
    }

    void ForcePush()
    {

        Vector3 explosionPos = Hand.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        foreach (Collider hit in colliders) {
            float angle = Vector3.Angle(Hand.transform.right, hit.GetComponent<Collider>().transform.position - Hand.transform.position);
            //Will n eed to check if object is a gripped object. if it isn't, continue with explosion force. if it is, will look like grippedObject.AddForcePushPower(power, explosionPos)
            if (hit.GetComponent<Rigidbody>() != null && (Vector3.Angle(Hand.transform.right, hit.GetComponent<Collider>().transform.position - Hand.transform.position) <= coneAngle)) {
                if (hit.name != "Player") hit.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 3f);
            }
        }
        //RaycastHit hit;
        //Vector3 target = transform.right * 10;

        //Ray ray = new Ray(hand.transform.position, target);
        //Debug.DrawRay(hand.transform.position, target);

        //Vector3 forceDirection;
        //if (Physics.Raycast(ray, out hit))
        //{
        //  forceDirection = hit.transform.position - hand.transform.position;
        //  //forceDirection.normalized;
        //  hit.rigidbody.AddForce(forceDirection * power);
        //}
    }

}
