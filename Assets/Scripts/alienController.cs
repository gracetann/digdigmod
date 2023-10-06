using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class alienController : enemyController
{
    private bool paceStateOver = false;
    // Start is called before the first frame update
    void Start()
    {
        randomNumber = Random.Range(5, 10);
        animator = this.GetComponent<Animator>();
        startPosition = this.transform.position;
        translation = new Vector3(Time.deltaTime, 0, 0);
        tilemap = tilemapObject.GetComponent<Tilemap>();
        gridLayout = tilemap.GetComponentInParent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!paceStateOver)
        {
            StartCoroutine("paceAroundState");
        }
        else
        {
            StartCoroutine("ghostChaseState");
        }
    }

    protected override IEnumerator paceAroundState()
    {
        float duration = Random.Range(4, 6);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, 5f, LayerMask.GetMask("Ground"));
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, 5f, LayerMask.GetMask("Ground"));
        if (rightHit.point.x - transform.position.x <= 0.5)
        {
            translation = new Vector3(-velocity * Time.deltaTime, 0);
        }
        else if (Mathf.Abs(leftHit.point.x - transform.position.x) <= 0.5)
        {
            translation = new Vector3(velocity * Time.deltaTime, 0);
        }
        transform.Translate(translation);
        yield return new WaitForSeconds(duration);
        paceStateOver = true;
    }
}
