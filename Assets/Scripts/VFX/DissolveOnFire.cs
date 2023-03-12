using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveOnFire : MonoBehaviour
{
    // Start is called before the first frame update
    public Material material;
    public Material shade;
    private SpriteRenderer renderer;
    float fade = 1f;
    public bool dieTest;

    private void Start()
    {
        fade = 1f;
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (dieTest)
        {
            renderer.material = shade;
            DoDissolve();
        }

        else
        {
            renderer.material = material;
        }
    }

    private void DoDissolve()
    {
        fade -= Time.deltaTime;
        if (fade >= 0.3f)
        {
            fade = 0.4f;
            //dieTest = false;
        }

        shade.SetFloat("_DissolveAmount", fade);
    }

    public void Dissolve()
    {
        dieTest = true;
    }

}

