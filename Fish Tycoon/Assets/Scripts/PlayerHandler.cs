using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour {

    GameObject interest = null;

    bool inUI = false;

    public Texture2D cursor = null;

	// Use this for initialization
	void Start () {
        
	}
	
//	// Update is called once per frame
//	void Update () {
//        Debug.Log (interest);
//        if (interest != null) {
//            string t = interest.tag;
//            switch (t) {
//                case ("Boat"):
//                    HandleBoat ();
//                    break;
//                default:
//                    Debug.Log ("nothing");
//                    break;
//            }
//        }
//        else if (!inUI) {
//            pickState ();
//        }
//            
//	}
//
//    void pickState() {
//        if (Input.GetMouseButtonDown (0)) {
//            Collider2D c = Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint (Input.mousePosition));
//
//            if (c != null) {
//                interest = c.gameObject;
//            }
//        }
//        else if (Input.GetMouseButtonDown (1)) {
//            Collider2D c = Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint (Input.mousePosition));
//
//            if (c != null) {
//                if (c.gameObject.tag == "Boat") {
//                    c.GetComponent<BoatBehavior> ().Stop ();
//                }
//                else {
//                    interest = c.gameObject;
//                }
//            }
//        }
//    }
//
//    void HandleBoat() {
//        if (Input.GetMouseButtonDown (0)) {
//            Collider2D c = Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint (Input.mousePosition));
//
//            if (c != null) {
//                // if a fish spot, add path to that
//                // if port, to that,
//
//                if (c.gameObject.tag == "Boat") {
//                    if (c.gameObject == interest) {
//                        Debug.Log ("You played yourself");
//                        //interest.GetComponent<BoatBehavior> ().Stop ();
//                    }
//                    else {
//                        interest = c.gameObject;
//                    }
//                }
//            }
//            else if (isValid (Camera.main.ScreenToWorldPoint (Input.mousePosition)) && interest.GetComponent<BoatBehavior> ().addPathNode (Camera.main.ScreenToWorldPoint (Input.mousePosition))) {
//                Debug.Log ("Path added!");
//            }
//            else {
//                Debug.Log ("path not added");
//                interest = null;
//                return;
//            }
//        }
//
//        if (Input.GetMouseButtonDown (1) && Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint (Input.mousePosition)) != null && Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint (Input.mousePosition)).gameObject == interest) {
//            interest.GetComponent<BoatBehavior> ().Stop ();
//            Debug.Log ("right clicked boat");
//            interest = null;
//        }
//    }

    void Update() {
        Collider2D c = Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint (Input.mousePosition));
        GameObject over =  (c == null) ? null:c.gameObject;

        if ((over != null && over.tag == "Boat") || (interest != null && interest.tag == "Boat")) {
            Cursor.SetCursor (cursor, Vector2.zero, CursorMode.Auto);
        }
        else {
            Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
        }

        if (Input.GetMouseButtonDown (0) ){
            pickAction (0, over);
        }
        else if (Input.GetMouseButtonDown(1)) {
            pickAction(1, over);
        }
    }

    void pickAction(int button, GameObject over) {
        
        if (over != interest) {
            if ((interest == null || interest.gameObject.tag != "Boat") || (interest != null && interest.gameObject.tag == "Boat" && button == 1)) {
                interest = over;
                Debug.Log ("new interest set");
            }
        }


        if (interest != null) {
            switch (interest.gameObject.tag) {
                case("Boat"):
                    if (button == 0 && over == interest) {
                        Debug.Log ("boat picked");
                    }
                    else if (button == 0 && interest.GetComponent<BoatBehavior> ().addPathNode (Camera.main.ScreenToWorldPoint (Input.mousePosition))) {
                        //Debug.Log ("Path node added");
                    }
                    else if (button == 1 && over == interest) {
                        interest.GetComponent<BoatBehavior> ().Stop ();
                        Debug.Log ("Boat Stopped " + interest.name);
                        interest = null;
                    }
                    else if (button == 1 && over != interest) {
                        Debug.Log ("Deselect " + interest.name + " over: " + over.name + ", over != interest: " + (over != interest));
                        interest = null;
                    }
                    else {
                        Debug.Log ("not valid boat cmmand");
                        interest = null;
                    }
                    break;
                default:
                    interest = null;
                    Debug.Log ("i Dont know what this is.");
                    break;
            }
        }
        else {
            Debug.Log ("Empty Click");
        }
    }

    bool isValid(Vector3 p) {
        return true;
    }
}
