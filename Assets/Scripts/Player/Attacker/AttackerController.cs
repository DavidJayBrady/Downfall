using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class AttackerController : MonoBehaviour
{
    private BasicAttack _basickAttack;
    private float _attackCoolDown;
    private PlayerController _playerController;
    private bool _attacking;
    private Vector2 _lookVector;

    private float _timeUntilStackRemoved = _timeToRemoveStack;

    // static is a problem fi we ever have 2 attackers
    static private int _slowTowerStacks;

    static private float _timeToRemoveStack = 5;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _basickAttack = GetComponent<BasicAttack>();
        _attackCoolDown = 0f;
        _attacking = false;
        _lookVector = new Vector2(-1.0f, -1.0f);

        _timeUntilStackRemoved = _timeToRemoveStack;
    }

    void Awake()
    {
        _slowTowerStacks = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLookVector();

        // Attack
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

        // Restore attacker movement over time
        if (_timeUntilStackRemoved < 0 && _slowTowerStacks > 0)
        {
            _slowTowerStacks -= 1;
            _timeUntilStackRemoved = _timeToRemoveStack;
            Debug.Log(_slowTowerStacks + " took a stack away!"    );
        }



        UpdateSpeed();

        _timeUntilStackRemoved -= Time.deltaTime;
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

    public void TowerHit(int level)
    {
        _timeUntilStackRemoved = _timeToRemoveStack;
        Debug.Log("tower hit");
        _slowTowerStacks += 1;

    }

    void UpdateSpeed()
    {
        if (!_attacking)
        {
            _playerController.speed = _playerController.baseSpeed * Mathf.Pow(.9f, _slowTowerStacks);
            Debug.Log(_playerController.speed);
            //_playerController.speed = _playerController.baseSpeed;
        }
        else
        {
            _playerController.speed = 0;
        }
    }

}
