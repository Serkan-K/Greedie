using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Beceri : MonoBehaviour
{
    [SerializeField] GameObject beceri;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        beceri.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        beceri.SetActive(false);
    }
}
