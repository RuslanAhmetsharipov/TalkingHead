using UnityEngine;
using System.Collections;

public class AlphaChanging : MonoBehaviour
{
    public GameObject cortex;
    public GameObject cortex1;
    public GameObject striatum;
    public GameObject internalGP;
    public GameObject externalGP;
    public GameObject thalamus;
    public GameObject nigraReticulata;
    public GameObject subthalamicNucleus;
    public GameObject particle;
    public GameObject head;

    public float timeForCycle = 7f;
    private Color inhibitory = Color.blue;
    private Color excitatory = Color.red;
    private bool ready = false;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(ChoosePath("undirectPath"));
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void dopamine()
    {
        
        StartCoroutine(wait());
    }
    IEnumerator wait()
    {
        while (!ready)
            yield return new WaitForSeconds(0.1f);
        Debug.Log("21");
        StopAllCoroutines();
        StartCoroutine(ChoosePath("directPath"));
        particle.SetActive(true);
        yield return null;
    }
   IEnumerator ChoosePath(string pathName)
    {
        
        switch (pathName)
        {
            case "directPath":
                {
                    ready = false;
                    cortex.GetComponent<MaterialChanging>().changeMaterialtoDefaultA();
                    cortex1.GetComponent<MaterialChanging>().changeMaterialtoDefaultA();
                    ApplyOpaque(excitatory, 1f, excitatory, striatum,true);
                    ApplyOpaque(inhibitory, 0.65f, excitatory, nigraReticulata,true);
                    ApplyOpaque(inhibitory, 0.2f, inhibitory, thalamus,true);
                    ApplyOpaque(inhibitory,0.5f, excitatory, internalGP,true);
                    ApplyOpaque(inhibitory, 0.7f, inhibitory, subthalamicNucleus,true);
                    ApplyOpaque(inhibitory, 0.2f, inhibitory, externalGP,true);
                    yield return new WaitForSeconds(timeForCycle);
                    cortex.GetComponent<MaterialChanging>().lowAlpha();
                    cortex1.GetComponent<MaterialChanging>().lowAlpha();
                    head.GetComponent<ChangeEmotionalState>().changeEmotionalStateFear = true;
                    yield return new WaitForSeconds(timeForCycle);
                    StartCoroutine(scaleTransform(striatum, 1f / 1.05f));
                    StartCoroutine(scaleTransform(nigraReticulata, 1f / 1.05f));
                    StartCoroutine(scaleTransform(thalamus, 1f / 0.95f));
                    StartCoroutine(scaleTransform(internalGP, 1f / 0.95f));
                    StartCoroutine(scaleTransform(subthalamicNucleus, 1f / 0.95f));
                    StartCoroutine(scaleTransform(externalGP, 1f / 0.95f));
                    ApplyOpaque(excitatory, 1f, excitatory, striatum, false);
                    ApplyOpaque(inhibitory, 0.35f, excitatory, nigraReticulata, false);
                    ApplyOpaque(inhibitory, 0.8f, inhibitory, thalamus, false);
                    ApplyOpaque(inhibitory, 0.8f, excitatory, internalGP, false);
                    ApplyOpaque(inhibitory, 0.5f, inhibitory, subthalamicNucleus, false);
                    ApplyOpaque(inhibitory, 1f, inhibitory, externalGP, false);
                    yield return new WaitForSeconds(timeForCycle);
                    head.GetComponent<ChangeEmotionalState>().changeEmotionalStateNormal = true;
                    yield return new WaitForSeconds(1f);
                    ready = true;
                    break;
                }
            case "undirectPath":
                {

                    ready = false;
                    ApplyOpaque(excitatory, 1f, excitatory, striatum,true);
                    ApplyOpaque(inhibitory, 0.35f, excitatory, nigraReticulata,true);
                    ApplyOpaque(inhibitory, 0.8f, inhibitory, thalamus,true);
                    ApplyOpaque(inhibitory, 0.8f, excitatory, internalGP,true);
                    ApplyOpaque(inhibitory, 0.5f, inhibitory, subthalamicNucleus,true);
                    ApplyOpaque(inhibitory, 1f, inhibitory, externalGP,true);
                    yield return new WaitForSeconds(timeForCycle);
                    cortex.GetComponent<MaterialChanging>().lowAlpha();
                    cortex1.GetComponent<MaterialChanging>().lowAlpha();
                    head.GetComponent<ChangeEmotionalState>().changeEmotionalStateFear = true;
                    yield return new WaitForSeconds(timeForCycle);
                    cortex.GetComponent<MaterialChanging>().changeMaterialtoDefaultA();
                    cortex1.GetComponent<MaterialChanging>().changeMaterialtoDefaultA();
                    StartCoroutine(scaleTransform(striatum, 1f/1.05f));
                    StartCoroutine(scaleTransform(nigraReticulata, 1f / 0.95f));
                    StartCoroutine(scaleTransform(thalamus, 1f / 0.95f));
                    StartCoroutine(scaleTransform(internalGP, 1f / 1.05f));
                    StartCoroutine(scaleTransform(subthalamicNucleus, 1f / 0.95f));
                    StartCoroutine(scaleTransform(externalGP, 1f / 0.95f));
                    ApplyOpaque(excitatory, 1f, excitatory, striatum, false);
                    ApplyOpaque(inhibitory, 0.35f, excitatory, nigraReticulata, false);
                    ApplyOpaque(inhibitory, 0.8f, inhibitory, thalamus, false);
                    ApplyOpaque(inhibitory, 0.8f, excitatory, internalGP, false);
                    ApplyOpaque(inhibitory, 0.5f, inhibitory, subthalamicNucleus, false);
                    ApplyOpaque(inhibitory, 1f, inhibitory, externalGP, false);
                    yield return new WaitForSeconds(timeForCycle);
                    head.GetComponent<ChangeEmotionalState>().changeEmotionalStateNormal = true;
                    ready = true;
                    yield return new WaitForSeconds(1f);
                    break;
                }
        }
        StartCoroutine(ChoosePath("undirectPath"));
    }
    void ApplyOpaque(Color col, float intensity, Color col2, GameObject go,bool path)
    {
        go.GetComponent<Renderer>().material.SetFloat("_Mode", 0f);
        Color ourNewColor;
        if (path)
        {
            if (col == col2)
            {
                ourNewColor = col * intensity;
                if (col.r > col.b)
                    StartCoroutine(scaleTransform(go, 1.05f));
                else
                    StartCoroutine(scaleTransform(go, 0.95f));
            }
            else
            {
                if (col.r > col2.r)
                    ourNewColor = new Color(col.r * intensity, 0f, col2.b * (1f - intensity));
                else
                    if (col.r == col2.r)
                    {
                        ourNewColor = new Color(col.r, col2.g * intensity + col.g * (1f - intensity), col2.b * intensity + col.b * (1f - intensity));
                    }
                    else
                    {
                        ourNewColor = new Color(col2.r * intensity, 0f, col.b * (1f - intensity));
                    }
                if (ourNewColor.r > ourNewColor.b)
                    StartCoroutine(scaleTransform(go, 1.05f));
                else
                    StartCoroutine(scaleTransform(go, 0.95f));
            }
        }
        else
        {
            ourNewColor = go.GetComponent<MaterialChanging>().defaultColor;
        }
        StartCoroutine(colorLerp(go.GetComponent<Renderer>().material.GetColor("_Color"), ourNewColor, go));
        go.GetComponent<Renderer>().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        go.GetComponent<Renderer>().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        go.GetComponent<Renderer>().material.SetInt("_ZWrite", 1);
        go.GetComponent<Renderer>().material.DisableKeyword("_ALPHATEST_ON");
        go.GetComponent<Renderer>().material.DisableKeyword("_ALPHABLEND_ON");
        go.GetComponent<Renderer>().material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        go.GetComponent<Renderer>().material.renderQueue = -1;
    }

    IEnumerator scaleTransform(GameObject go, float t)
    {
        Vector3 defaultScale = go.transform.localScale;
        float step = defaultScale.x / 5000f;
        if(t>1f)
        while(go.transform.localScale.x<defaultScale.x*t)
        {
            yield return new WaitForSeconds(0.01f);
            go.transform.localScale += new Vector3(step, step, step);
        }
        else
            while (go.transform.localScale.x > defaultScale.x * t)
            {
                yield return new WaitForSeconds(0.01f);
                go.transform.localScale -= new Vector3(step, step, step);
            }
    }
    IEnumerator colorLerp(Color col1, Color col2, GameObject go)
    {
        float step = 0f;
        while (!go.GetComponent<Renderer>().material.GetColor("_Color").Equals(col2))
        {
            yield return new WaitForSeconds(0.01f);
            go.GetComponent<Renderer>().material.SetColor("_Color",Color.Lerp(col1, col2, step+=0.005f));
        }
    }

}
