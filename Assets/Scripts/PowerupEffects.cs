using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupEffects : MonoBehaviour
{
    public Light light1;
    float natural;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        {
            StartCoroutine("countdown");
        }
    }
    IEnumerator countdown()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(gameObject);
    }
    IEnumerator speedBoost()
    {
        yield return new WaitForSeconds(5.0f);
        playerController.velocity -= 5.0f;
    }
    IEnumerator freezing()
    {
        yield return new WaitForSeconds(5.0f);
        enemyController.frozen = false;
    }
    //IEnumerator lighting()
    //{
    //    yield return new WaitForSeconds(5.0f);
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (string.Equals(gameObject.tag, "jetpack"))
        {
            if (gameObject.activeSelf)
            {
                Debug.Log("jetpack");
                AudioManagerDigdug.Instance.PlayPowerupSound();
                Destroy(gameObject);
                playerController.velocity += 1.0f;
                StartCoroutine("speedBoost");
            }
        }
        else if (string.Equals(gameObject.tag, "no2"))
        {
            if (gameObject.activeSelf)
            {
                enemyController.frozen = true;
                AudioManagerDigdug.Instance.PlayPowerupSound();
                Destroy(gameObject);
                StartCoroutine("freezing");
            }
        }
        else if (string.Equals(gameObject.tag, "food"))
        {
            if (gameObject.activeSelf)
            {
                scoreCalc.score += 100;
                Debug.Log("food");
                AudioManagerDigdug.Instance.PlayPowerupSound();
                Destroy(gameObject);
            }
        }
        else if (string.Equals(gameObject.tag, "battery"))
        {
            if (gameObject.activeSelf)
            {
                AudioManagerDigdug.Instance.PlayPowerupSound();
                //natural = light1.range;
                //light1.range = natural * 5;
                //Debug.Log(light1.range);
                lightController.instance.lightingEffect();
                Destroy(gameObject);
                //StartCoroutine("lighting");
                //light1.range = natural;
            }
        }
    }
}
