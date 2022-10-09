using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float OnscreenDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, OnscreenDelay);    // destroy this object after 3 sec
    }

    private void OnCollisionEnter(Collision collision)
    {
     
        Destroy(this.gameObject);
  
    }

}
