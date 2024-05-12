using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveFunc : MonoBehaviour
{
    [SerializeField] Vector2 _pointA;
    [SerializeField] Vector2 _pointB;

    [SerializeField] float _speed;
    [SerializeField] float _amplitude;
    [SerializeField] float _amplitudeOffset;

    float _rand;
    bool _canMove = false;
    // Start is called before the first frame update
    void Start()
    {
        _rand = Random.Range(2.0f, 6.0f);
        _pointA = new Vector2(transform.position.x, transform.position.y + _rand);
        _pointB = new Vector2(transform.position.x, transform.position.y - _rand);

        float temp = Random.Range(0.0f, 0.5f);

        Invoke("CanMove", temp);
    }

    // Update is called once per frame
    void Update()
    {
        

        if(_canMove)
            transform.position = Vector2.Lerp(_pointA, _pointB, Mathf.Sin(Time.time * _speed) * _amplitude + _amplitudeOffset);
    }

    private void CanMove()
    {
        _canMove = true;
    }
}
