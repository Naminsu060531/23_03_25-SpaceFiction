using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public Bullet bulletScript;
    Vector2 direction;

    void Start()
    {
        direction = (transform.localRotation * Vector2.up).normalized;
    }

    public void Shoot()
    {
        GameObject go = Instantiate(bulletScript.gameObject, transform.position, Quaternion.identity);
        Bullet goBullet = go.GetComponent<Bullet>();
        goBullet.Direction = direction;
    }

}
