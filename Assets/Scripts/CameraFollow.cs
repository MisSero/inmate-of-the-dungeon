using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4;
    //private GameObject playerKeeper;
    private Transform playerTransform;
    private Transform cameraTransform;
    private Vector3 targetPosiont;

    void Start()
    {
        cameraTransform = gameObject.transform;
        playerTransform = ReferencesToObjects.PlayerObject.transform;
        //playerTransform = playerKeeper.transform.GetChild(0);
        cameraTransform.position = gameObject.transform.position;
        targetPosiont = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z - 10);
        cameraTransform.position = targetPosiont;
    }

    void Update()
    {
        targetPosiont = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z - 10);
        if (cameraTransform.position != targetPosiont)
        {
            cameraTransform.position = Vector3.Lerp(gameObject.transform.position, targetPosiont, moveSpeed * Time.deltaTime);
        }
    }
}
