using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LittleDandy : MonoBehaviour
{
    public Slider powerSlider;
    public GameObject petalFallEffect;
    public float health = 10;
    public float tolerance = 0.8f;
    public float powerCostOfOneBlow;
    public float pushForce;
    public float torqueForce;
    private Camera cam;
    private Rigidbody2D rg2D;
    private ParticleSystem petalEffect;
    ParticleSystem.EmissionModule emissionModule;
    ParticleSystem.MainModule mainModule;
    GameController gameController;
    private Vector2 screenCenterPos;

    bool rotateForVictory = false;
    bool isCollided = false;
    public float fadeInterval = 1f;
    private Vector2 destinationPos;
    // Use this for initialization
    void Start()
    {

        powerSlider.minValue = 0;
        powerSlider.maxValue = health;
        powerSlider.value = powerSlider.maxValue;

        cam = Camera.main;
        petalEffect = GetComponentInChildren<ParticleSystem>();
        emissionModule = petalEffect.emission;
        mainModule = petalEffect.main;
        rg2D = GetComponent<Rigidbody2D>();

        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (rotateForVictory)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPos, Time.deltaTime);
            transform.RotateAround(transform.position, Vector3.forward, 100 * Time.deltaTime);
            if(transform.localScale.x <= 0)
            {
                transform.localScale = new Vector2(0, 0);
                return;
            }
            transform.localScale = new Vector2(transform.localScale.x - Time.deltaTime, transform.localScale.y - Time.deltaTime);
        }

        if (gameController.GetIsStageEnd()) { return; }
        if (gameController.IsGuidePanelDisplaying()) { return; }

            if (Input.GetMouseButtonUp(0))
        {
            if (powerSlider.value <= 0) { return; }

            rg2D.simulated = true;
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;
            Vector2 point = cam.ScreenToWorldPoint(new Vector2(x, y));
            Vector2 dir = new Vector2(transform.position.x - point.x, transform.position.y - point.y);
            rg2D.AddForce(dir * pushForce);
            rg2D.AddTorque(Random.Range(-torqueForce, torqueForce));

        }

        if (mainModule.simulationSpeed == 1)
        {
            fadeInterval -= Time.deltaTime;
            if (fadeInterval <= 0)
            {
                mainModule.simulationSpeed = 0.001f;
                fadeInterval = 1f;
            }
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!GetIsAlive()) { return; }
        if (col.gameObject.CompareTag("Harmless Bound")) { return; }
        if (gameController.GetIsStageEnd()) { return; }

        if (col.relativeVelocity.magnitude > tolerance)
        {
            GameObject petalFallFx;
            ParticleSystem.EmissionModule fallEmissionModule;

            float impactForce = col.relativeVelocity.magnitude;
            powerSlider.value -= impactForce;

            float impactRate = col.relativeVelocity.magnitude / powerSlider.maxValue;
            float flyAwayAmount = impactRate * emissionModule.rate.constant;
            if (powerSlider.value <= 0)
            {
                emissionModule.rate = 0;
                mainModule.simulationSpeed = 3;

                petalFallFx = Instantiate(petalFallEffect, transform.position, Quaternion.identity) as GameObject;
                fallEmissionModule = petalFallFx.GetComponent<ParticleSystem>().emission;
                fallEmissionModule.rate = flyAwayAmount;
                petalFallFx.transform.SetParent(transform, true);

                FindObjectOfType<GameController>().LoseBehavior();
                return;
            }


            float newRate = emissionModule.rate.constant - flyAwayAmount;

            emissionModule.rate = newRate;
            mainModule.simulationSpeed = 1;


            petalFallFx = Instantiate(petalFallEffect, transform.position, Quaternion.identity) as GameObject;
            fallEmissionModule = petalFallFx.GetComponent<ParticleSystem>().emission;
            fallEmissionModule.rate = flyAwayAmount;
            petalFallFx.transform.SetParent(transform, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<DandyFellow>())
        {
            collider.GetComponent<DandyFellow>().SetTouchByDandy();
            if(gameController.TouchedAllRequiredFellows()){
                gameController.SetUpDestinationForStage();
            }
        }
    }
    public bool GetIsAlive()
    {
        bool isAlive = (powerSlider.value >= 0);
        return isAlive;
    }

    public float GetRemainingHealth()
    {
        return powerSlider.value / health;
    }

    public void VictoryRotate(Vector2 desPos)
    {
        destinationPos = desPos;
        rg2D.simulated = false;
        rg2D.isKinematic = true;
        rotateForVictory = true;
    }
}
