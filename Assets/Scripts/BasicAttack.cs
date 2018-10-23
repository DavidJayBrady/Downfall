using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    private ControllerMovement _controllerMovement;
    public Vector2 attackVector = new Vector2(-1.0f, -1.0f);

    // Start is called before the first frame update
    void Start()
    {
        //_controllerMovement = GetComponent<ControllerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 location = transform.position;
        Debug.Log(Common.VectorAngleDegrees(ref location));
        //Vector2 location = (UnityEngine.Vector2Int)_controllerMovement.GetPlayerPosition();
        //attackVector = location + _controllerMovement.lookVector;
        //Debug.Log((Vector2)transform.position - attackVector);
        //Vector2 tester = _controllerMovement.lookVector.normalized;
        //Debug.Log(tester);
        //Debug.Log(Mathf.Atan(tester.y/tester.x) );
    }
}
