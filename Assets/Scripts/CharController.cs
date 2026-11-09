using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CharController : MonoBehaviour
{
    bool _canMove;
    int _distanceToMove = 1;
    float _health;
    readonly WaitForSeconds _timeToWait = new(3);
    readonly Color playerColor = new(0.3411765f, 0.4901961f, 0.9607843f);
    readonly Color enemyColor = new(0.9607843f, 0.3411765f, 0.3411765f);
    SpriteRenderer[] _spriteRenderers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _canMove = true;
    }

    void Start()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        // SwapCharType();
    }

    // Update is called once per frame
    void Update()
    {
        if (_canMove)
        {
            transform.position = new(transform.position.x,transform.position.y + _distanceToMove,transform.position.z);
            StartCoroutine(WaitToMove());
        }
    }

    IEnumerator WaitToMove()
    {
        _canMove = false;
        yield return _timeToWait;
        // SwapCharType();
        _canMove = true;
    }

    public void SwapCharType()
    {
        Color colourToSwitchTo;
        String tagToSwitchTo;
        int rotationToSwitchTo;
        if (CompareTag("Player"))
        {
            colourToSwitchTo = enemyColor;
            tagToSwitchTo = "Enemy";
            rotationToSwitchTo = 180;
        }
        else
        {
            colourToSwitchTo = playerColor;
            tagToSwitchTo = "Player";
            rotationToSwitchTo = 0;
        }
        foreach(SpriteRenderer renderer in _spriteRenderers)
        {
            renderer.color = colourToSwitchTo;
        }
        transform.rotation = new(transform.rotation.x,transform.rotation.y,rotationToSwitchTo,transform.rotation.w);
        gameObject.tag = tagToSwitchTo;
        _distanceToMove = -_distanceToMove;
    }
}
