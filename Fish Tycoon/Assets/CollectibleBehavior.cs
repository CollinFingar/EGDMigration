using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehavior : MonoBehaviour {


    public enum CollectType {Refugee, Fish};

    CollectType ct = CollectType.Fish;

    [Tooltip ("Amount of units per second.")]
    public float accumulationRate = 0.0f;

    public int total = 0;
    float amount = 0.0f;

    bool collecting = false;

	// Use this for initialization
	void Start () {
        switch (this.gameObject.tag) {
            case("Fish"):
                ct = CollectType.Fish;
                break;
            case("Refugee"):
                ct = CollectType.Refugee;
                break;
            default:
                ct = CollectType.Fish;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (collecting) {
            collect ();
        }
	}

    void collect() {
        amount += accumulationRate * Time.deltaTime;

        while (total != 0 && amount > 1.0f) {
            amount = amount - 1.0f;
            total = total - 1;

            // Add stuff
        }
    }

    public void setup(int t = 10, float aR = 0.5f) {
        accumulationRate = aR;
        total = t;
    }

    void OnTriggerEnter2D(Collider2D other) {
        collecting = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        collecting = false;
        amount = 0.0f;
    }

}
