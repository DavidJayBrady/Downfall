using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class AttackerController : MonoBehaviour
{
    private BasicAttack _basickAttack;
    private float _attackCoolDown;

    public int AttackerHealth;
    

    // Start is called before the first frame update
    void Start()
    {
        _basickAttack = GetComponent<BasicAttack>();
        _attackCoolDown = 0f;
        AttackerHealth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_attackCoolDown <= 0)
        {
            if (Common.GetControllerInputAxis(1, "Right Trigger") > .5f)
            {
                BasicAtack();
            }
        }

        else
        {
            _attackCoolDown -= Time.deltaTime;
        }
    }

    void BasicAtack()
    {
        _basickAttack.Attack();
        _attackCoolDown = 2;
    }

    public void towerHit(int dmg)
    {
        Debug.Log("tower hit");
        AttackerHealth -= dmg;
        Debug.Log("health" + AttackerHealth);

    }
}
