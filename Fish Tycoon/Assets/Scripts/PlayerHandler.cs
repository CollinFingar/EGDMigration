using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour {

    GameObject interest = null;

    bool inUI = false;

    public Texture2D cursor = null;

    GameObject map = null;

	// Use this for initialization
	void Start () {
        map = GameObject.FindGameObjectWithTag ("Map");
	}
	
    void Update() {
        Collider2D c = Physics2D.OverlapPoint (Camera.main.ScreenToWorldPoint (Input.mousePosition));
        GameObject over =  (c == null) ? null:c.gameObject;

        if (over != null) {
            //Debug.Log (over.name);
        }

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
                //Debug.Log ("new interest set");
            }
        }


        if (interest != null && isValid(Camera.main.ScreenToWorldPoint (Input.mousePosition))) {
            switch (interest.gameObject.tag) {
                case("Boat"):
                    if (button == 0 && over == interest) {
                        //Debug.Log ("boat picked");
                    }
                    else if (button == 0 && interest.GetComponent<BoatBehavior> ().addPathNode (Camera.main.ScreenToWorldPoint (Input.mousePosition))) {
                        ////Debug.Log ("Path node added");
                    }
                    else if (button == 1 && over == interest) {
                        interest.GetComponent<BoatBehavior> ().Stop ();
                        //Debug.Log ("Boat Stopped " + interest.name);
                        interest = null;
                    }
                    else if (button == 1 && over != interest) {
                        //Debug.Log ("Deselect " + interest.name + " over: " + over.name + ", over != interest: " + (over != interest));
                        interest = null;
                    }
                    else {
                        //Debug.Log ("not valid boat cmmand");
                        interest = null;
                    }
                    break;
                default:
                    interest = null;
                    //Debug.Log ("i Dont know what this is.");
                    break;
            }
        }
        else {
            //Debug.Log ("Empty Click");
        }
    }

    bool isValid(Vector3 p) {
        Texture2D mapText = map.GetComponent<SpriteRenderer> ().sprite.texture;
        Bounds bounds = map.GetComponent<SpriteRenderer> ().sprite.bounds;

        float xscale = map.transform.localScale.x;
        float yscale = map.transform.localScale.y;

        int x = Mathf.FloorToInt( ((p.x - xscale*(bounds.center.x - bounds.extents.x)) / (xscale*bounds.size.x))*mapText.width );
        int y = Mathf.FloorToInt( ((p.y - yscale*(bounds.center.y - bounds.extents.y)) / (yscale*bounds.size.y))*mapText.height );

        Color pix = mapText.GetPixel (x, y);

        if (pix.a == 0.0f) {
            return true;
        }
        else {
            return false;
        }
    }
}
