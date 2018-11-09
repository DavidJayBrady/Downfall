using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class AttackerController : MonoBehaviour
{
    private BasicAttack _basickAttack;
    private float _attackCoolDown;
    private PlayerController _playerController;
    private float slowStackerPercentage;
    private bool _attacking;
    private Vector2 _lookVector;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _basickAttack = GetComponent<BasicAttack>();
        _attackCoolDown = 0f;
        _attacking = false;
        _lookVector = new Vector2(-1.0f, -1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLookVector();
        if (Common.GetControllerInputAxis(1, "Right Trigger") > .5f)
        {
            _attacking = true;
            if (_attackCoolDown <= 0)
            {
                BasicAtack();
            }
        }
        else
        {
            _attacking = false;
        }

       UpdateSpeed();
        _attackCoolDown -= Time.deltaTime;

    }

    void UpdateLookVector()
    {
        _lookVector = Common.GetScaledVectorInput(1, "Left Horizontal", "Left Vertical");
    }

    void BasicAtack()
    {
        _basickAttack.Attack(_lookVector);
        _attackCoolDown = .5f;
    }

    public void TowerHit(int dmg)
    {
        Debug.Log("tower hit");
    }

    void UpdateSpeed()
    {
        if (!_attacking)
        {
            _playerController.speed = _playerController.baseSpeed;
        }
        else
        {
            _playerController.speed = 0;
        }
    }

}
