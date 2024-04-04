using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    public float PlayerHp = 5;
    public TextMeshProUGUI PlayerHpText;
    
    private Vector2 _raycastSentPosition = Vector2.right;
    [HideInInspector] public DragHandler _dragHandler;

    private bool _sendRaycast = true;

    private EnemyHp _enemyHpScript;
    private float _enemyHp;
    private bool _isIncrease = false;
    private bool _isDecline = false;
    private float _playerFightStartHp;
    private GameObject _enemyGameobject;

    public GameObject AnimationObject;
    private Animator _animator;
    [HideInInspector] public int KillScore = 0;
    private BoxCollider2D boxCollider;

    void Start()
    {
         boxCollider = GetComponent<BoxCollider2D>();
        _animator = AnimationObject.GetComponent<Animator>();
        _dragHandler = GetComponent<DragHandler>();
        PlayerHpText.text = PlayerHp.ToString();
    }

    void FixedUpdate()
    {
        SendRaycastFromRight();      
    }

    private void Update()
    {
        float elapsedTime = 0f;
        elapsedTime += 0.5f * Time.deltaTime;

        if(_isDecline)
        {
             if(_enemyHpScript.AnimatorEnemy != null)
              {
                  _enemyHpScript.AnimatorEnemy.SetBool("Fight", true);
              }
              PlayerHp = Mathf.Lerp(PlayerHp, 0, elapsedTime);
              PlayerHpText.text = PlayerHp.ToString("F0"); 

           if(PlayerHp <= 0.5f)
           {
             _animator.SetBool("Die", true);
              if(_enemyHpScript.AnimatorEnemy != null)
              {
                  _enemyHpScript.AnimatorEnemy.SetBool("Fight", false);
              }
             Invoke("DestroyObject", 1f);
             _isDecline = false;
           }

        }
        if(!_isIncrease)
        return;
        
        elapsedTime += Time.deltaTime;
        PlayerHp = Mathf.Lerp(PlayerHp, _enemyHp + _playerFightStartHp, elapsedTime);
        PlayerHpText.text = PlayerHp.ToString("F0");
        _enemyHpScript._newEnemyHp = _enemyHpScript._newEnemyHp - 0.1f;
        
        if (PlayerHp >= _enemyHp + _playerFightStartHp - 0.5f)
        {
            elapsedTime = 0f;
            PlayerHp = _enemyHp + _playerFightStartHp;
            PlayerHpText.text = PlayerHp.ToString("F0");

            _dragHandler.DraggingAllowed = true;
            _isIncrease = false;
            _animator.SetBool("Fight", false);
            KillScore++;
        }
        else
        {
             _animator.SetBool("Fight", true);
        }
    }


    private void SendRaycastFromRight()
    {   
         Vector2 objPosition = transform.position;
        float objScaleX = transform.localScale.x;

        Vector2 rightSide = objPosition + _raycastSentPosition * objScaleX;

        int layerMask = LayerMask.GetMask("Enemy");
        RaycastHit2D hit = Physics2D.Raycast(rightSide, _raycastSentPosition, distance: 2f, layerMask);
      
        if (hit.collider != null)
        {
             if(!_dragHandler.IsPlayerInRoom)
            return;
             if(_dragHandler.IsDragging)
            return;
             if(_isIncrease)
             return;

             _enemyHpScript = hit.collider.GetComponent<EnemyHp>();
            _enemyHp = hit.collider.GetComponent<EnemyHp>().HpValue;
            
            _enemyGameobject = hit.collider.gameObject;
            if(PlayerHp <= _enemyHp)
            {
             _dragHandler.DraggingAllowed = false;
            _isDecline = true;
            }
            else
            {
            _dragHandler.DraggingAllowed = false;
            _isIncrease = true;
            _playerFightStartHp = PlayerHp;
            }    
        }
    }

    private void DestroyObject()
    {
      Destroy(gameObject);
    }
    

}
