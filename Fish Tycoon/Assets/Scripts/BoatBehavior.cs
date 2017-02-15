using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatBehavior : MonoBehaviour {

	LevelHandler level;
	LineRenderer pathLine;
	List<Vector3> path = new List<Vector3>();

	public float pathWidth = 1.0f;
	public Color pathColor = Color.white;

	public float maxPathLength = 100;

	public float travelSpeed = 0.1f;

    public int capacity = 0;
    public int maxCapaciity = 30;

    public float maxFuel = 300.0f;
    public float fuelConsumptionRate = 15.0f;
    public float fuel = 0.0f;

    public int fish = 0;
    public int refugees = 0;

    public int fishPrice = 5;

    bool docked = false;

	void Awake() {
		level = LevelHandler.Instance;

		pathLine = this.gameObject.GetComponent<LineRenderer> ();
		pathLine.startWidth = pathWidth;
		pathLine.endWidth = pathWidth;

		pathLine.startColor = pathColor;
		pathLine.endColor = pathColor;

        fuel = maxFuel;
	}

	// Use this for initialization
	void Start () {
		path.Add (this.gameObject.transform.position);
		updatePath ();
	}
	
	// Update is called once per frame
	void Update () {
        
		checkPath ();
		path [0] = this.gameObject.transform.position;
		Move ();

        //Debug.Log ("Capacity: " + capacity + "/" + maxCapaciity + " | Fish: " + fish + " | Refugees: " + refugees + " | Fuel: " + fuel + "/" + maxFuel);
	}

	void Move() {
        if (!docked) {
            int overcapacity = 1.0f;

            overcapacity += 0.1 * Mathf.Max (0, (capacity - maxCapaciity));
            fuel = fuel - fuelConsumptionRate * Time.deltaTime;
        }

        if (fuel <= 0) {
            fuel = 0;
            runOutOfFuel ();
            return;
        }

        Vector3 start = this.gameObject.transform.position;
		float timetomove = Time.deltaTime * level.getTimeDilation ();
		while (path.Count > 1 && Vector3.Distance(path[0], path[1]) <= travelSpeed*timetomove ) {
			if (timetomove * travelSpeed > Vector3.Distance (path [0], path [1])) {
				this.gameObject.transform.position = path [1];
				timetomove = timetomove - (Vector3.Distance (path [0], path [1]) / travelSpeed);
				path.RemoveAt (1);
			} else {
				break;
			}
		}
		if (timetomove != 0 && path.Count > 1) {
			Vector3 direction = (path[1] - this.gameObject.transform.position).normalized;
			this.gameObject.transform.position = this.gameObject.transform.position + direction * timetomove * travelSpeed;
		}
		path [0] = this.gameObject.transform.position;
		updatePath ();
	}

	void checkPath() {
		if (path.Count > 1 && Vector3.Distance (path [1], this.transform.position) < 0.001f ) {
            path.RemoveAt (1);
			updatePath ();
		}
	}

	public bool addPathNode(Vector3 point) {

        if (Vector3.Distance (path [path.Count - 1], new Vector3 (point.x, point.y, -1.0f)) < 0.1f) {
            return false;
        }

        float distance = 0.0f;
        for (int i = 0; i < path.Count; i++) {
            if (i + 1 < path.Count) {
                if (i == 0) {
                    distance += Vector3.Distance (this.gameObject.transform.position, path [i + 1]);
                }
                else {
                    distance += Vector3.Distance (path [i], path [i + 1]);
                }
            }
            else {
                distance += Vector3.Distance (path [i], point);
            }
        }

        if (distance < maxPathLength) {
            path.Add (new Vector3 (point.x, point.y, -1.0f));
            return true;
        }
        else {
            return false;
        }
	}

	void updatePath() {
		Vector3[] points = new Vector3[path.Count];
		path.CopyTo (points);
		pathLine.numPositions = path.Count;
		pathLine.SetPositions (points);
	}

    public void Stop () {
        path.Clear ();
        path.Add (this.gameObject.transform.position);
    }

    public void addStuff(CollectibleBehavior.CollectType t) {
        switch (t) {
            case (CollectibleBehavior.CollectType.Fish):
                fish++;
                break;
            case (CollectibleBehavior.CollectType.Refugee):
                refugees++;
                break;
        }
        capacity++;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Dock") {

            GameHandler gh = GameObject.FindObjectOfType<GameHandler> ();
            UIHandler uih = GameObject.FindObjectOfType<UIHandler> ();

            gh.AddFunds (fishPrice*fish);
            uih.AddRefugeeSaved (refugees);

            gh.SubtractFunds (20);

            fish = 0;
            refugees = 0;

            capacity = 0;

            fuel = maxFuel;

            docked = true;

        }
    }

    void OnTriggerExit2D(Collider2D other) {
        docked = false;
    }

    void runOutOfFuel() {
        fish = 0;
        refugees = 0;

        this.transform.position = GameObject.FindGameObjectWithTag ("Dock").transform.position;
    }

}
