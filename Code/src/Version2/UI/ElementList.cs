using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace gamecore
{

    public partial class GameCore
    {

        public GuiElementDefinitions GuiElementDefs = new GuiElementDefinitions((g, self) => {

            self.MainMenuLoginPanel.nodeTypeInstance.SetActive(true);
        });
    }

    public partial class GuiElementDefinitions
    {

        public static Action<GuiElementDefinitions> InitActionList = (e) => { };

        public static Action<GuiElementDefinitions> AfterInit = (g) => { };

        public static Indexer GuiIndexerInstance = new Indexer("GUI_ELEMENT_DEFS");
        public Indexer GuiIndexer;

        public GameObject ROOT_GUI_OBJECT;

        public EventModel FocusedObjectEventInstance;

        public Dictionary<string, EventModel> ElementEventMap;
               
        public GuiElementDefinitions(Action<GameCore, GuiElementDefinitions> initObject)
        {

            this.ElementEventMap = new Dictionary<string, EventModel>();

            this.GuiIndexer = GuiIndexerInstance;
            GuiIndexerInstance = null;

            this.ROOT_GUI_OBJECT = new GameObject();

            InitActionList(this);

            AfterInit(this);
            
            new GameCoreObject<GuiElementDefinitions>(this, initObject);
        }

        public void HandleEvent(string eventname, string indexname = null, int mousebutton = 0, KeyCode keycode = KeyCode.None)
        {
            if(mousebutton != 0)
            {
                for(int i = 0; i <  7; i++)
                {
                    if (((mousebutton >> i) & 1) == 1)
                    {
                        switch (eventname)
                        {
                            case EventModel.EVENT_MOUSE:
                                    //Debug.Log("MOUSE");
                                    this.SendEvent(eventname : eventname, indexname : indexname, mousebutton : mousebutton);
                                break;
                            case EventModel.EVENT_MOUSE_DOWN:
                                    //Debug.Log("MOUSE DOWN");
                                this.SendEvent(eventname: eventname, indexname: indexname, mousebutton: mousebutton);
                                break;
                            case EventModel.EVENT_MOUSE_UP:
                                    //Debug.Log("MOUSE UP");
                                this.SendEvent(eventname: eventname, indexname: indexname, mousebutton: mousebutton);
                                break;
                        }
                    }
                }
            }
            else
            {
                SendEvent(eventname : eventname, keycode : keycode);
            }
        }

        private void SendEvent(string eventname = null, string indexname = null, int mousebutton = 0, KeyCode keycode = KeyCode.None)
        {
            if(indexname == null)
            {
                if(this.FocusedObjectEventInstance == null)
                {
                    //Debug.Log("nothing is selected");
                    return;
                }
                switch (eventname)
                {
                    case EventModel.EVENT_KEY:
                        this.FocusedObjectEventInstance.HandleKey(this);
                        break;

                    case EventModel.EVENT_KEY_DOWN:
                        Debug.Log("key is down now ");
                        this.FocusedObjectEventInstance.HandleKeyDown(this);
                        break;

                    case EventModel.EVENT_KEY_UP:
                        this.FocusedObjectEventInstance.HandleKeyUp(this);
                        break;
                }
            }
            else
            {
                if (this.ElementEventMap.ContainsKey(indexname))
                {
                    EventModel eventInstance = null;
                    switch (eventname)
                    {
                        case EventModel.EVENT_MOUSE:
                            this.HandleFocusShift(eventInstance = this.ElementEventMap[indexname]);
                            //FocusObjectEventInstance will become the eventinstance of indexname's value.
                            eventInstance.HandleMouse(this);
                            break;

                        case EventModel.EVENT_MOUSE_DOWN:
                            this.HandleFocusShift(eventInstance = this.ElementEventMap[indexname]);
                            //FocusObjectEventInstance will become the eventinstance of indexname's value.
                            this.FocusedObjectEventInstance.HandleMouseDown(this);
                            break;

                        case EventModel.EVENT_MOUSE_UP:
                            if (this.FocusedObjectEventInstance != null)
                            {
                                this.FocusedObjectEventInstance.HandleMouseUp(this);
                            }
                            break;
                    }
                }
                else
                {

                    //Debug.Log("no index name");
                }

            }
        }

        public void HandleFocusShift(EventModel nextInstance)
        {
            //perform onblur on last element.
            //perform onfocus on next element.
            if (this.FocusedObjectEventInstance == nextInstance)
            {
                return;
            }
            if (this.FocusedObjectEventInstance == null)
            {
                this.FocusedObjectEventInstance = nextInstance;
            }
            else
            {
                
                this.FocusedObjectEventInstance.HandleOnBlur(this);
                this.FocusedObjectEventInstance = nextInstance;
            }

            this.FocusedObjectEventInstance.HandleOnFocus(this);

        }

        public void AddEventListenerInstance(string name, EventModel em)
        {
            if (this.ElementEventMap.ContainsKey(name))
            {
                Debug.Log(name + ":overridden");
            }
            else
            {
                Debug.Log(name + ":added");
                this.ElementEventMap.Add(name, em);
            }
        }

    }

    public class EventModel
    {

        public const string EVENT_KEY = "EVENT_KEY";
        public const string EVENT_KEY_DOWN = "EVENT_KEY_DOWN";
        public const string EVENT_KEY_UP = "EVENT_KEY_UP";

        public const string EVENT_MOUSE = "EVENT_MOUSE";
        public const string EVENT_MOUSE_DOWN = "EVENT_MOUSE_DOWN";
        public const string EVENT_MOUSE_UP = "EVENT_MOUSE_UP";

        public const string EVENT_BLUR = "EVENT_BLUR";
        public const string EVENT_FOCUS = "EVENT_FOCUS";

        public Action<GuiElementDefinitions> HandleKeyDown = (g) => { };
        public Action<GuiElementDefinitions> HandleKeyUp = (g) => { };
        public Action<GuiElementDefinitions> HandleKey = (g) => { };

        public Action<GuiElementDefinitions> HandleMouseDown = (g) => { };
        public Action<GuiElementDefinitions> HandleMouseUp = (g) => { };
        public Action<GuiElementDefinitions> HandleMouse = (g) => { };

        public Action<GuiElementDefinitions> HandleOnFocus = (g) => { };
        public Action<GuiElementDefinitions> HandleOnBlur = (g) => { };

    }

    public class ElementNode
    {
        public object pNodeTypeInstance;

        public string name = GuiElementDefinitions.GuiIndexerInstance.GetIndex();
        public EventModel eventListeners = null;
        
        public void SetEventListener(GuiElementDefinitions g, EventModel eventmodel)
        {
            g.AddEventListenerInstance(this.name, eventmodel);
            this.SetEventListeners(eventmodel);
        }

        private void SetEventListeners(EventModel eventmodel)
        {
            if (this.eventListeners == null)
            {
                Debug.Log("Event Listeners for , " + this.name + ", Could not be set");
                this.eventListeners = eventmodel;
            }            
            foreach (var a in typeof(EventModel).GetFields(~System.Reflection.BindingFlags.Static))
            {
                Action<GuiElementDefinitions> hold = (Action<GuiElementDefinitions>)a.GetValue(eventmodel);
                if (hold == null)
                {
                    Debug.Log("nullerino");
                }
                else
                {
                    Debug.Log(this.name+":"+a.Name+"::::");
                    a.SetValue(this.eventListeners, hold);
                }
            }
            
        }

    }

    public class ElementNode<NodeType> : ElementNode where NodeType : new()
    {

        public NodeType nodeTypeInstance { get { return (NodeType) pNodeTypeInstance; } set { pNodeTypeInstance = value; } }

        public ElementNode(Action<GuiElementDefinitions, ElementNode, NodeType> Init, object nodeTypeValue = null)
        {            
            //Debug.Log("Node Instantiation");

            if (nodeTypeValue == null)
            {

                nodeTypeValue = new NodeType();
            }

            this.nodeTypeInstance = (NodeType)nodeTypeValue;
                       
            GuiElementDefinitions.InitActionList += (g) =>
            {
                Init(g, this, this.nodeTypeInstance);
                this.AdjustPosition();
            };
        }

        public Action AdjustPosition = ()=> { };
       
    }

    public partial class GuiElementDefinitions
    {

        public static int LabelHeight = 50;

        public ElementNode<GameObject> GuiCanvas = new ElementNode<GameObject>((guiClassInstance, self, node) => {

            node.name = "GuiCanvas";

            Canvas canvas = node.AddComponent<Canvas>();
            CanvasScaler scaler = node.AddComponent<CanvasScaler>();

            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);

            canvas.worldCamera = Camera.main;
            canvas.renderMode = RenderMode.ScreenSpaceCamera;

            node.transform.SetParent(guiClassInstance.ROOT_GUI_OBJECT.transform);
            node.transform.localRotation = default;
        });
    }

    public abstract class GuiElement
    {

        public Vector3 ElementSize;
        public Vector3 ElementScale;
        public Vector3 ElementPosition;
        public Quaternion elementRotation;

        public Vector3 elemenentCentering;

        public ElementNode<GameObject> sourceObject;

        public List<ElementNode<GameObject>> containedObjects;

        public ElementNode<GameObject> AddContainedObject(ElementNode<GameObject> g)
        {
            this.containedObjects.Add(g);
            return g;
        }
        
        public GuiElement(ElementNode<GameObject> self)
        {
            this.containedObjects = new List<ElementNode<GameObject>>();
            this.sourceObject = this.AddContainedObject(self);
        }

        public virtual void Adjust()
        {

        }

        public void SetActive(bool b)
        {            
            foreach(var c in containedObjects)
            {
                Debug.Log(c.name+":::");
                c.nodeTypeInstance.SetActive(b);
            }
        }

        public void UpdateTransform(Action<GuiElement> update)
        {
            update(this);
            Adjust();
        }

        public GameObject SetParent(ElementNode<GameObject> parent)
        {
            return this.sourceObject.nodeTypeInstance.SetParent(this.sourceObject, parent.nodeTypeInstance.transform);
        }

        public GameObject SetParent(ElementNode parent)
        {
            return this.SetParent(((GuiElement) (parent.pNodeTypeInstance)).sourceObject);
        }

        public Quaternion SetRotation(Quaternion rotation)
        {
            return this.elementRotation = rotation;
        }

        public Vector3 SetPosition(Vector3 position)
        {
            return this.ElementPosition = position;
        }

        public Vector2 SetSize(Vector2 size)
        {
            return this.aSetSize(this.ElementSize = size);
        }
        public Vector3 SetSize(Vector3 size)
        {
            return this.aSetSize(this.ElementSize = size);
        }

        public Vector2 SetScale(Vector3 scale)
        {
            return this.ElementScale = scale;
        }


        internal virtual Vector2 aSetSize(Vector2 size)
        {
            this.ElementSize = size;

            return size;
        }

        internal virtual Vector3 aSetSize(Vector3 size)
        {
            this.ElementSize = size;

            return size;
        }
    }

    public class GuiText : GuiElement
    {
        public ElementNode<GameObject> textNode { get; set; }

        public Text textComponent;

        public GuiText() : base(new ElementNode<GameObject>(nodeTypeValue: null, Init: (guiClassInstance, self, node) => {
            GameObject selfObject = node.SetGameObject<GameObject>(self, node.transform);
            selfObject.AddComponent<CanvasScaler>();
            selfObject.AddComponent<GraphicRaycaster>();
        }))
        {

            this.textNode = this.AddContainedObject(new ElementNode<GameObject>(nodeTypeValue: GuiPrefabs.BasicText, Init: (guiClassInstance, self, node) => {
                GameObject selfObject = node.SetParent<GameObject>(this.textNode, this.sourceObject.nodeTypeInstance.transform);
                this.textComponent = selfObject.GetComponent<Text>();
                this.textComponent.fontSize = 1;
                this.textComponent.text = "Hello World";
            }));
            //this.textNode.AdjustPosition = this.Adjust;
        }

        public override void Adjust()
        {
            if (elemenentCentering.x == 1)
            {
                this.ElementPosition.x += (this.ElementSize.x);
            }
            if (elemenentCentering.y == 1)
            {
                //this.elementPosition.y;//+= (this.elementSize.y/2);
            }

            RectTransform r = this.textNode.nodeTypeInstance.GetComponent<RectTransform>();
            r.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.ElementSize.x);
            r.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.ElementSize.y);

            this.textNode.nodeTypeInstance.transform.SetLocationWithBounds(ElementPosition, ElementSize, elemenentCentering);
        }
    }

    public class GuiPanel : GuiElement
    {
        public ElementNode<GameObject> panelBackground { get; set; }

        public GameObject panelContainerGameObject;
        public GameObject panelBackgroundGameObject;
        
        public GuiPanel() : base(new ElementNode<GameObject>(nodeTypeValue: null, Init: (guiClassInstance, self, node) => {
            GameObject selfObject = node.SetGameObject<GameObject>(self, node.transform);
        }))
        {

            this.panelContainerGameObject = this.sourceObject.nodeTypeInstance;

            this.panelBackground = new ElementNode<GameObject>(nodeTypeValue: GameObject.CreatePrimitive(PrimitiveType.Cube), Init: (guiClassInstance, self, node) => {
                GameObject selfObject = node.SetParent<GameObject>(this.panelBackground, this.sourceObject.nodeTypeInstance.transform);

                MeshRenderer mr = selfObject.GetComponent<MeshRenderer>();
                Material mat = mr.material;
            });

            this.panelBackgroundGameObject = panelBackground.nodeTypeInstance;

            // this.panelBackground.AdjustPosition = this.Adjust;
        }


        public override void Adjust()
        {

            if (elemenentCentering.x == 1)
            {
                this.ElementPosition.x += (this.ElementSize.x) - (this.ElementScale.x / 2);
            }
            if (elemenentCentering.y == 1)
            {
                this.ElementPosition.y -= ((this.ElementSize.y / 2) - (this.ElementScale.y)) / 2;
            }

            this.sourceObject.nodeTypeInstance.transform.SetLocationWithBounds(ElementPosition, ElementSize, elemenentCentering);

            this.panelBackground.nodeTypeInstance.transform.SetLocation(Vector3.zero, ElementScale, new Vector3(1, 1, 1));
        }

        internal override Vector3 aSetSize(Vector3 size)
        {
            this.ElementSize = size;

            return size;
        }

        internal override Vector2 aSetSize(Vector2 size)
        {
            this.ElementSize = size;

            return size;
        }

    }

    public class InputBox : GuiElement
    {
        public ElementNode<GameObject> inputFieldObject;
        public InputField inputfieldComponent;


        public InputBox() : base(new ElementNode<GameObject>(nodeTypeValue: GuiPrefabs.InputField, Init: (guiClassInstance, self, node) => {
            GameObject selfObject = node.SetGameObject<GameObject>(self, node.transform);
        }))
        {
                this.inputFieldObject = new ElementNode<GameObject>(nodeTypeValue: (this.inputfieldComponent = this.sourceObject.nodeTypeInstance.GetComponentInChildren<InputField>()).gameObject, Init: (guiClassInstance, self, node) =>
                {
                    GameObject selfObject = node.SetParent<GameObject>(inputFieldObject, this.sourceObject.nodeTypeInstance.transform);
                    this.inputFieldObject.nodeTypeInstance.AddComponent<BoxCollider>();
                });

                this.inputFieldObject.AdjustPosition = this.Adjust;
        }


        internal override Vector2 aSetSize(Vector2 size)
        {

            RectTransform r = this.sourceObject.nodeTypeInstance.GetComponent<RectTransform>();
            r.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            r.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);

            r = inputFieldObject.nodeTypeInstance.GetComponent<RectTransform>();
            r.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            r.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);

            //Debug.Log((this.canvasObject.eventListeners == null)+":?");
            BoxCollider bc = this.inputFieldObject.nodeTypeInstance.AddComponent<BoxCollider>();
            bc.size = size;

            return size;
        }

        public override void Adjust()
        {
            if (elemenentCentering.x == 1)
            {
                this.ElementPosition.x += (this.ElementSize.x);
            }
            if (elemenentCentering.y == 1)
            {
                //this.elementPosition.y;//+= (this.elementSize.y/2);
            }

            RectTransform r = inputFieldObject.nodeTypeInstance.GetComponent<RectTransform>();
            r.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ElementSize.x);
            r.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ElementSize.y);

            //Debug.Log((this.canvasObject.eventListeners == null)+":?");
            BoxCollider bc = this.inputFieldObject.nodeTypeInstance.GetComponent<BoxCollider>();
            bc.size = ElementSize;

            this.inputFieldObject.nodeTypeInstance.transform.SetLocationWithBounds(ElementPosition, ElementSize, elemenentCentering);

        }

    }



    public static class uiUnityExtensions
    {

        public static RectTransform SetSize(this RectTransform self, Rect rect)
        {

            self.rect.Set(rect.x, rect.y, rect.width, rect.height);
            self.ForceUpdateRectTransforms();
            return self;
        }

        public static void SetSize(this Rect self, Vector2 size)
        {
            self.size = size;
        }


        public static GameObject SetGameObject<T>(this GameObject self, ElementNode elem, Transform parent) where T: new()
        {

            self.name = elem.name;
            self.transform.localPosition = default;
            self.transform.localRotation = default;
            self.transform.localScale = Vector3.one;

            return self;
        }

        public static GameObject SetParent<T>(this GameObject self, ElementNode<T> elem, Transform parent) where T: new()
        {

            self.name = elem.name;

            self.transform.localScale = Vector3.one;
            self.transform.SetParent(parent);
            self.transform.localPosition = default;
            self.transform.localRotation = default;
            self.transform.localScale = Vector3.one;

            return self;
        }
        
        public static void SetLocationWithBounds(this Transform self, Vector3 position, Vector3 bounds, Vector3 center)
        {

            self.localPosition =
               new Vector3
               {
                   x = -(bounds.x/2) * (center.x),
                   y = -(bounds.y/2) * (center.y),
               } + new Vector3
               {
                   x = position.x,
                   y = -position.y,
                   z = position.z
               };
        }
        

        public static void SetLocation(this Transform self, Vector3 position, Vector3 scale, Vector3 center)
        {

            self.localPosition =
                new Vector3
                {
                    x = scale.x / 2*(center.x),
                    y = -(scale.y / 2*(center.y)),
                } + new Vector3
                {
                    x = position.x,
                    y = -position.y,
                    z = position.z
                };
           
            self.localScale = new Vector3
            {
                x = scale.x,
                y = scale.y,
                z = scale.z,
            };
        }

        public static void SetLocation(this Transform self, Vector3 position)
        {

            self.localPosition = new Vector3
                {
                    x = position.x,
                    y = -position.y,
                    z = position.z
                };
        }
    }

}
