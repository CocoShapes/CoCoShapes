using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public GameObject loginPanel;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("Logo").GetComponent<Animator>();
        StartCoroutine(Splash());
    }

    private IEnumerator Splash()
    {
        animator.Play("SplashAnimation");
        yield return new WaitForSeconds(4);
        loginPanel.SetActive(true);
    }
}
