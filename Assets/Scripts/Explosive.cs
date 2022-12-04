using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private float force;
    private void Start()
    {
        Explode();
    }
    public void Explode()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            //random force
            Vector3 forceVec = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 0f), Random.Range(-1f, 1f)).normalized;
            child.GetComponent<Rigidbody>().AddForce(forceVec * force, ForceMode.Impulse);
        }
    }
}
