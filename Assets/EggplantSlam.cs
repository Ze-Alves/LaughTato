using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggplantSlam : MonoBehaviour
{

    public AnimationCurve AscendingCurve,DecendingCurve;
    [SerializeField] float _slamRadius = 10f;
    [SerializeField] float _slamForce = 100f;

    int SlamState;

    public int damage;

    public float timer;

  
    void Update()
    {
        if (SlamState == 1)
        {
            timer += Time.deltaTime;

            transform.position += Vector3.up * Time.deltaTime * AscendingCurve.Evaluate(timer/2)  * 50;
            Debug.Log(AscendingCurve.Evaluate(.5f));
            if (transform.position.y > 30)
                SlamState = 2;
        }
        else if (SlamState == 2)
        {
            timer += Time.deltaTime;

            transform.position += Vector3.up * Time.deltaTime * AscendingCurve.Evaluate(timer/2) * 50;
            if (transform.position.y < 0)
            {
                SlamState = 0;
                PushEnemies();
                transform.position -= Vector3.up * transform.position.y;
                timer = 0;
            }

        }

    }

    public void Slam()
    {
        SlamState = 1;
    }

    private void PushEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _slamRadius);

        foreach (Collider collider in colliders)
        {
            var rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log("Pushing: " + collider.name);
                rb.AddForce(Vector3.up * _slamForce/10, ForceMode.Impulse);
                rb.AddExplosionForce(_slamForce, transform.position, _slamRadius);
            }
        }
    }
}
