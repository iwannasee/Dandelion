using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DandyFellow : MonoBehaviour {
    public Color haloHitColor;
    Transform petalEffect;
    Transform haloEffect;
    bool isTouchByDandy = false;
	// Use this for initialization
	void Start () {
        petalEffect = transform.GetChild(0);
        haloEffect = transform.GetChild(1);
        petalEffect.gameObject.SetActive(false);

    }

    public void PlayEffectWhenTouchDandy()
    {
        petalEffect.gameObject.SetActive(true);
        ParticleSystem halo = haloEffect.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule haloMainModule = halo.main;
        haloMainModule.startColor = haloHitColor;
    }

    public void SetTouchByDandy()
    {
        isTouchByDandy = true;
        PlayEffectWhenTouchDandy();
    }

    public bool GetIsTouched()
    {
        return isTouchByDandy;
    }
}
