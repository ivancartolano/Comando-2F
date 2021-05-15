using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullett : MonoBehaviour
{
    public float speed;
    public GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(Instantiate(particle, transform.position, Quaternion.identity), 0.5f);
        Destroy(gameObject);
    }
}
