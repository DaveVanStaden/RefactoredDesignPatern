using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject child;
    [SerializeField] private float speed;
    private void Awake()
    {
        //finds the player and an object under it that holds the camera in place.
        //player = GameObject.FindGameObjectWithTag("Player");
        //child = player.transform.Find("CameraConstraint").gameObject;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        follow();
    }

    private void follow()
    {
        //lets the camera follow the player through the position of the child.
        gameObject.transform.position = Vector3.Lerp(transform.position, child.transform.position, Time.deltaTime * speed);
        gameObject.transform.LookAt(player.gameObject.transform.position);
    }
}
