using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    Car parent;
    private void Start()
    {
        parent = GetComponentInParent<Car>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectible")
        {
            Destroy(other.gameObject);
            StartCoroutine(SpeedUp());
        }
    }

    IEnumerator SpeedUp()
    {
        GameManager.GetInstance().powerUp = 2f;
        yield return new WaitForSeconds(3f);
        GameManager.GetInstance().powerUp = -6.5f;
        yield return new WaitForSeconds(3f);
        GameManager.GetInstance().powerUp = 1f;
    }
}
