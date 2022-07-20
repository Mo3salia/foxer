using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wood : MonoBehaviour
{
    [SerializeField] private GameObject wood_platfrom;
    void Update()
    {
        if(wood_platfrom.transform.position.x <= -172.7)
        {
            wood_platfrom.SetActive(false);
            StartCoroutine(DO());
        }
    }
    IEnumerator DO()
    {
        yield return new WaitForSeconds(0.2f);
        wood_platfrom.transform.position = new Vector3(-155.79f, 7.11f, 0);
        wood_platfrom.SetActive(true);

    }
}
