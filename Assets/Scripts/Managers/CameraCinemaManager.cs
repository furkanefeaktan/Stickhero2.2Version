using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraCinemaManager : MonoBehaviour
{
    public Transform CameraTargetObj;
    public float MoveSpeed = 2.0f; 
    public int IfKillScore = 1;
    private PlayerFight _playerFight;
    [HideInInspector] public bool CameraMove = false;
    [HideInInspector] public bool CheckTargetReached = false;

    void Start()
    {
        _playerFight = FindObjectOfType<PlayerFight>();
    }

    void Update()
    {
        if(IfKillScore != _playerFight.KillScore)
        return;
        CameraMove = true;
        if (CameraTargetObj != null)
        {
            Vector3 targetPosition = new Vector3(CameraTargetObj.position.x, CameraTargetObj.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, MoveSpeed * Time.deltaTime);           
        }
        if(Vector3.Distance(transform.position, CameraTargetObj.position) < 0.1f)
            CheckTargetReached = true;
        
    }
}
