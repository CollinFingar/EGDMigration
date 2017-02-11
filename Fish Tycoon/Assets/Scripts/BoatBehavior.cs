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

	void Awake() {
		level = LevelHandler.Instance;

		pathLine = this.gameObject.GetComponent<LineRenderer> ();
		pathLine.startWidth = pathWidth;
		pathLine.endWidth = pathWidth;

		pathLine.startColor = pathColor;
		pathLine.endColor = pathColor;

	}

	// Use this for initialization
	void Start () {
		path.Add (this.gameObject.transform.position);
		updatePath ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			addPathNode (Camera.main.ScreenToWorldPoint (Input.mousePosition));
			updatePath ();
		}

        if (Input.GetKeyDown (KeyCode.Backspace)) {
            Stop ();
        }
		checkPath ();
		path [0] = this.gameObject.transform.position;
		Move ();

	}

	void Move() {
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

	void addPathNode(Vector3 point) {
//		if (path.Count - 1 < maxPathLength) {
//			path.Add (new Vector3 (point.x, point.y, -1.0f));
//		}
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
        }
	}

	void updatePath() {
		Vector3[] points = new Vector3[path.Count];
		path.CopyTo (points);
		pathLine.numPositions = path.Count;
		pathLine.SetPositions (points);
	}

    void Stop () {
        path.Clear ();
        path.Add (this.gameObject.transform.position);
    }
}
