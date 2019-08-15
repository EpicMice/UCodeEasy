using System;
using System.Collections.Generic;
using UnityEngine;

namespace gamecore
{

    public partial class GameCore
    {
                
        Action<int>[] KeyUpEventPass = { (index) => { }, (index) => { } };
        Action<int>[] KeyDownEventPass = { (index) => { }, (index) => { } };

        int KeyEventIndex = 0;

        public void SetupKeyPass(KeyCode hold)
        {
            Action<int> Key = (index) => { };
            Action<int> KeyUp = (index) => { };
            Action<int> KeyDown = (index) => { };


            KeyUpEventPass[0] += KeyUp = (index) => {
                if (Input.GetKeyDown(hold))
                {
                    GuiElementDefs.HandleEvent(eventname: EventModel.EVENT_KEY_DOWN, keycode: hold);
                    KeyDownEventPass[index] += KeyDown;
                }
                else
                {
                    KeyUpEventPass[index] += KeyUp;
                }
            };

            KeyDown = (index) => {
                if (Input.GetKeyUp(hold))
                {
                    GuiElementDefs.HandleEvent(eventname: EventModel.EVENT_KEY_UP, keycode: hold);
                    KeyUpEventPass[index] += KeyUp;
                }
                else
                {
                    KeyDownEventPass[index] += KeyDown;
                    GuiElementDefs.HandleEvent(eventname: EventModel.EVENT_KEY, keycode: hold);
                }
            };


        }

        public void Init()
        {

            foreach (KeyCode k in System.Enum.GetValues(typeof(KeyCode)))
            {
                KeyCode hold = k;
                SetupKeyPass(hold);
            }
        }

        Dictionary<string, GameObject> mousehits = new Dictionary<string, GameObject>();


        int[] keyvalues = new int[Enum.GetValues(typeof(KeyCode)).Length];


        public void Update()
        {
            //Debug.Log("Run");

            mousehits.Clear();

            mousehits = InputMethods.MousePickedObjects(Camera.main);

            int mouseButton = 0;
            int mouseButtonDown = 0;
            int mouseButtonUp = 0;

            if (mousehits.Count > 0)
            {
                    //Debug.Log(go.layer + ":" + go.name);


                for (int i = 0; i < 7; i++) {

                    if (Input.GetMouseButtonDown(i))
                    {
                        mouseButtonDown +=  1 << (i);
                    }else if (Input.GetMouseButtonUp(i))
                    {
                        mouseButtonUp += 1 << (i);
                    }
                    else if(Input.GetMouseButton(i))
                    {
                        mouseButton += 1 << (i);
                    }
                }

                foreach (GameObject go in mousehits.Values)
                {
                    if (mouseButton != 0)
                    {
                        //Debug.Log(go.name + ":" + ElementDefs.ElementEventMap.ContainsKey(go.name));
                        GuiElementDefs.HandleEvent(eventname: EventModel.EVENT_MOUSE, indexname: go.name, mousebutton: mouseButton);
                    }

                    if(mouseButtonDown != 0)
                    {
                        //Debug.Log(go.name + ":" + ElementDefs.ElementEventMap.ContainsKey(go.name));
                        GuiElementDefs.HandleEvent(eventname: EventModel.EVENT_MOUSE_DOWN, indexname: go.name, mousebutton: mouseButtonDown);
                    }

                    if(mouseButtonUp != 0)
                    {
                        //Debug.Log(go.name + ":" + ElementDefs.ElementEventMap.ContainsKey(go.name));
                        GuiElementDefs.HandleEvent(eventname: EventModel.EVENT_MOUSE_UP, indexname: go.name, mousebutton: mouseButtonUp);
                    } 
                }
            }

            if ((mouseButton + mouseButtonDown + mouseButtonUp) == 0)
            {
                //Debug.Log("started");
                int currentKeyIndex = KeyEventIndex;

                if (KeyEventIndex == 0)
                {
                    KeyEventIndex = 1;
                }
                else
                {
                    KeyEventIndex = 0;
                }
                               
                KeyUpEventPass[currentKeyIndex](KeyEventIndex);
                KeyDownEventPass[currentKeyIndex](KeyEventIndex);

                KeyUpEventPass[currentKeyIndex] = (index) => { };
                KeyDownEventPass[currentKeyIndex] = (index) => { };

            }

        }

        public class InputMethods
        {

            public static Dictionary<string, GameObject> MousePickedObjects(Camera cam)
            {
                Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                GameObject go = null;
                foreach (RaycastHit hit in Physics.RaycastAll(ray.origin, ray.direction, int.MaxValue, ~0, QueryTriggerInteraction.Collide))
                {
                    go = hit.transform.gameObject;
                    dictionary.Add(go.GetInstanceID()+":"+go.layer, hit.transform.gameObject);
                }
                return dictionary;
            }

        }
    }

    
}