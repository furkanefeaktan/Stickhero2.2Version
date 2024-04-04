using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelPlayerSet : MonoBehaviour
{
    public Transform RoomToDestroy;
    public GameObject RoomBoomParticleEffect;
    public Vector3 TargetPositionPlayer;
    private PlayerFight playerFight;
    private DragHandler dragHandler;
    private CameraCinemaManager cameraCinemaManager;
    private float movementDuration = 2;
    private float elapsedTime = 0f;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        dragHandler = GetComponent<DragHandler>();
        playerFight = GetComponent<PlayerFight>();
        cameraCinemaManager = FindObjectOfType<CameraCinemaManager>();
    }

    void FixedUpdate()
    {
        if(_startedScaleEnumerator)
        {
             if(RoomToDestroy != null)
             {
              transform.position = RoomToDestroy.position;
             }
             else
             {
              transform.position = TargetPositionPlayer;
             }
        }

        if(cameraCinemaManager.CheckTargetReached)
        {   
             elapsedTime += Time.deltaTime;
            if (elapsedTime < movementDuration)
            {
               transform.position = RoomToDestroy.position;
               dragHandler.DraggingAllowed = false;
            }
            else
            {
                playerFight._dragHandler.DraggingAllowed = false;
                if(!_scaleTimeWork)
                {
                   StartCoroutine(ScaleOverTime()); 
                }
            }
        }
    }
   
    private float _targetScale = 1.3f;
    private float _currentScale = 1;
    private bool _scaleTimeWork = false;
    private bool _startedScaleEnumerator = false;
    
    IEnumerator ScaleOverTime()
    {
        //GameObject particleEffectInstance = null;

        while (_currentScale < _targetScale)
        {
            _startedScaleEnumerator = true;
            _currentScale += Time.deltaTime * 0.1f; 
            transform.localScale = new Vector3(_currentScale, _currentScale, 1f);
            yield return null;
        }
          /*
         if (RoomBoomParticleEffect != null && particleEffectInstance == null)
         {
              particleEffectInstance = Instantiate(RoomBoomParticleEffect);
              particleEffectInstance.GetComponent<ParticleSystem>().Play();
         }*/
         if (RoomToDestroy != null)
         {
              Destroy(RoomToDestroy.gameObject);
             
              while (_currentScale < 4f)
              {
                   _rb.constraints = RigidbodyConstraints2D.FreezeRotation; 
                  _currentScale += Time.deltaTime * 0.1f; 
                   transform.localScale = new Vector3(_currentScale, _currentScale, 1f);
                    yield return null;
              }

              dragHandler.IsPlayerInRoom = true;
         }

         _scaleTimeWork = true;
    }
}
