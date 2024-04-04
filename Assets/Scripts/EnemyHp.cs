using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public int HpValue = 10;
    public TextMeshProUGUI EnemyHpText;

    public GameObject AnimationObject;
    [HideInInspector]public Animator AnimatorEnemy;
    
    [HideInInspector]public float _newEnemyHp;
    public bool allowTextToGoNegative = true;

    private void Start()
    {
        _newEnemyHp = HpValue;
        EnemyHpText.text = HpValue.ToString();
        if(AnimationObject == null)
        return;
        AnimatorEnemy = AnimationObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if(HpValue == _newEnemyHp)
        return;

        float elapsedTime = 0f;
        elapsedTime += 0.5f * Time.deltaTime;
        
        if(allowTextToGoNegative)
        {
          _newEnemyHp = Mathf.Lerp(_newEnemyHp, 0, elapsedTime);
          EnemyHpText.text = _newEnemyHp.ToString("F0"); 
        }
        else if(!allowTextToGoNegative && _newEnemyHp >= 0)
        {
          _newEnemyHp = Mathf.Lerp(_newEnemyHp, 0, elapsedTime);
          EnemyHpText.text = _newEnemyHp.ToString("F0"); 
        }
        

        if(_newEnemyHp <= 0)
        {    
            if(AnimationObject != null)
              AnimatorEnemy.SetBool("Die", true);

            Invoke("DestroyObject", 1f);
        }
        else
        {

        }
        
    }

     void DestroyObject()
    {
        Destroy(gameObject);
    }
    
}
