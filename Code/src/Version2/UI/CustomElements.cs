using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace gamecore
{

    //Login Panel
    public partial class GuiElementDefinitions
    {

        public ElementNode<GuiPanel> MainMenuLoginPanel = new ElementNode<GuiPanel>(nodeTypeValue: new GuiPanel(), Init: (guiClassInstance, self, node) => {

            node.SetParent(guiClassInstance.GuiCanvas);

            //selfObject.transform.SetLocation(new Vector3(-1920 / 2, -1080 / 2, 0.001f));

            node.UpdateTransform((panel) => {
                panel.ElementPosition = new Vector3(-1920 / 2, -1080 / 2, 0.001f);
                panel.ElementSize = new Vector3(1920, 1080, 0.01f);
                panel.ElementScale = new Vector3(340, 410, 0.01f);
                panel.elemenentCentering = new Vector3(1, 1, 0);
            });

            GuiElementDefinitions.AfterInit += (g) => {
                node.SetActive(false);
            };

        });

        public ElementNode<GuiPanel> LoginPanel = new ElementNode<GuiPanel>(nodeTypeValue: new GuiPanel(), Init: (guiClassInstance, selfb,node) => {

            node.SetParent(guiClassInstance.MainMenuLoginPanel);

            MeshRenderer mr = node.panelBackgroundGameObject.GetComponent<MeshRenderer>();
            mr.material.color = new Color(0.3f, 0.3f, 0.3f, 1f);

            node.UpdateTransform((panel) => {
                panel.ElementPosition = new Vector3(0, 0, -0.01f);
                panel.ElementSize = new Vector3(340, 0, 0.01f);
                panel.ElementScale = new Vector3(300, LabelHeight, 0.01f);
                panel.elemenentCentering = new Vector3(1, 1, 0);
            });
        });


        public ElementNode<InputBox> LoginPanelText = new ElementNode<InputBox>(nodeTypeValue: new InputBox(), Init: (guiClassInstance, self, node) => {

            node.SetParent(guiClassInstance.LoginPanel);

            node.inputfieldComponent.textComponent.fontSize = 24;
            node.inputfieldComponent.text = "Username";

            node.inputfieldComponent.interactable = false;
            node.inputfieldComponent.GetComponent<Image>().enabled = false;

            node.UpdateTransform((panel) => {
                panel.ElementPosition = new Vector3(0, 0, -0.01f);
                panel.ElementSize = new Vector3(300, LabelHeight, 0.01f);
                //panel.elementScale = new Vector3(300, LabelHeight, 0.01f);
                panel.elemenentCentering = new Vector3(1, 1, 0);
            });
        });

        public ElementNode<GuiPanel> LoginFieldPanel = new ElementNode<GuiPanel>(nodeTypeValue: new GuiPanel(), Init: (guiClassInstance, selfb, node) => {

            node.SetParent(guiClassInstance.MainMenuLoginPanel);

            MeshRenderer mr = node.panelBackgroundGameObject.GetComponent<MeshRenderer>();
            mr.material.color = new Color(0.3f, 0.3f, 0.3f, 1f);

            node.UpdateTransform((panel) => {
                panel.ElementPosition = new Vector3(0, (LabelHeight) + 2, -0.01f);
                panel.ElementSize = new Vector3(340, 0, 0.01f);
                panel.ElementScale = new Vector3(300, LabelHeight, 0.01f);
                panel.elemenentCentering = new Vector3(1, 1, 0);
            });
        });

        public ElementNode<InputBox> LoginField = new ElementNode<InputBox>(nodeTypeValue: new InputBox(), Init: (guiClassInstance, self, node) => {

            node.SetParent(guiClassInstance.LoginFieldPanel);

            node.inputfieldComponent.textComponent.fontSize = 24;
            node.inputfieldComponent.text = "Username here";

            node.UpdateTransform((panel) => {
                panel.ElementPosition = new Vector3(0, 0, -0.01f);
                panel.ElementSize = new Vector3(300, LabelHeight, 0.01f);
                //panel.elementScale = new Vector3(300, LabelHeight, 0.01f);
                panel.elemenentCentering = new Vector3(1, 1, 0);
            });

            Debug.Log(node.inputFieldObject.name + "::");

            node.inputFieldObject.SetEventListener(guiClassInstance, new EventModel
            {
                HandleMouseDown = (g) =>
                {
                    Debug.Log($"Clicked {self.name} !");

                },
                HandleOnFocus = (g) =>
                {
                    Debug.Log($"Focused {self.name}");
                    Material material = g.LoginFieldPanel.nodeTypeInstance.panelBackgroundGameObject.GetComponent<MeshRenderer>().material;
                    material.color = new Color(0.9f, 0.4f, 0.4f);
                },
                HandleOnBlur = (g) =>
                {
                    Debug.Log("here");
                    Debug.Log($"Focused {self.name}");
                    Material material = g.LoginFieldPanel.nodeTypeInstance.panelBackgroundGameObject.GetComponent<MeshRenderer>().material;
                    material.color = new Color(0.3f, 0.3f, 0.3f);
                },

                HandleKeyDown = (g) =>
                {
                    Debug.Log("key down");
                }
            });
        });

        //Password Panel[

        public ElementNode<GuiPanel> PasswordPanel = new ElementNode<GuiPanel>(nodeTypeValue: new GuiPanel(), Init: (guiClassInstance, selfb, node) => {

            node.SetParent(guiClassInstance.MainMenuLoginPanel);

            MeshRenderer mr = node.panelBackgroundGameObject.GetComponent<MeshRenderer>();
            mr.material.color = new Color(0.3f, 0.3f, 0.3f, 1f);

            node.UpdateTransform((panel) => {
                panel.ElementPosition = new Vector3(0, (LabelHeight * 2) + 10, -0.01f);
                panel.ElementSize = new Vector3(340, 0, 0.01f);
                panel.ElementScale = new Vector3(300, LabelHeight, 0.01f);
                panel.elemenentCentering = new Vector3(1, 1, 0);
            });
        });


        public ElementNode<InputBox> PasswordPanelText = new ElementNode<InputBox>(nodeTypeValue: new InputBox(), Init: (guiClassInstance, selfb, node) => {

            node.SetParent(guiClassInstance.PasswordPanel);

            node.inputfieldComponent.textComponent.fontSize = 24;
            node.inputfieldComponent.text = "Password";

            node.inputfieldComponent.interactable = false;
            node.inputfieldComponent.GetComponent<Image>().enabled = false;

            node.UpdateTransform((panel) => {
                panel.ElementPosition = new Vector3(0, 0, -0.01f);
                panel.ElementSize = new Vector3(300, LabelHeight, 0.01f);
                //panel.elementScale = new Vector3(300, LabelHeight, 0.01f);
                panel.elemenentCentering = new Vector3(1, 1, 0);
            });

        });

        public ElementNode<GuiPanel> TestPanelPasswordFieldPanel = new ElementNode<GuiPanel>(nodeTypeValue: new GuiPanel(), Init: (guiClassInstance, selfb, node) => {

            node.SetParent(guiClassInstance.MainMenuLoginPanel);

            MeshRenderer mr = node.panelBackgroundGameObject.GetComponent<MeshRenderer>();
            mr.material.color = new Color(0.3f, 0.3f, 0.3f, 1f);

            node.UpdateTransform((panel) => {
                panel.ElementPosition = new Vector3(0, (LabelHeight * 3) + 12, -0.01f);
                panel.ElementSize = new Vector3(340, 0, 0.01f);
                panel.ElementScale = new Vector3(300, LabelHeight, 0.01f);
                panel.elemenentCentering = new Vector3(1, 1, 0);
            });
        });

        public ElementNode<InputBox> TestPanelPasswordField = new ElementNode<InputBox>(nodeTypeValue: new InputBox(), Init: (guiClassInstance, self, node) => {

            node.SetParent(guiClassInstance.TestPanelPasswordFieldPanel);

            node.inputfieldComponent.textComponent.fontSize = 24;
            node.inputfieldComponent.text = "Password here";

            node.UpdateTransform((panel) => {
                panel.ElementPosition = new Vector3(0, 0, -0.01f);
                panel.ElementSize = new Vector3(300, LabelHeight, 0.01f);
                //panel.elementScale = new Vector3(300, LabelHeight, 0.01f);
                panel.elemenentCentering = new Vector3(1, 1, 0);
            });

            Debug.Log(node.inputFieldObject.name + "::");

            node.inputFieldObject.SetEventListener(guiClassInstance, new EventModel
            {
                HandleMouseDown = (g) =>
                {
                    Debug.Log($"Clicked {self.name} !");

                },
                HandleOnFocus = (g) =>
                {
                    Debug.Log($"Focused {self.name}");
                    Material material = g.TestPanelPasswordFieldPanel.nodeTypeInstance.panelBackgroundGameObject.GetComponent<MeshRenderer>().material;
                    material.color = new Color(0.9f, 0.4f, 0.4f);
                },
                HandleOnBlur = (g) =>
                {
                    Debug.Log("here");
                    Debug.Log($"Focused {self.name}");
                    Material material = g.TestPanelPasswordFieldPanel.nodeTypeInstance.panelBackgroundGameObject.GetComponent<MeshRenderer>().material;
                    material.color = new Color(0.3f, 0.3f, 0.3f);
                },

                HandleKeyDown = (g) =>
                {
                    Debug.Log("key down");
                }
            });
        });

        //LoginButton[

        public ElementNode<GuiPanel> SubmitButton = new ElementNode<GuiPanel>(nodeTypeValue: new GuiPanel(), Init: (guiClassInstance, self, node) => {

            node.SetParent(guiClassInstance.MainMenuLoginPanel);

            MeshRenderer mr = node.panelBackgroundGameObject.GetComponent<MeshRenderer>();
            mr.material.color = new Color(0.3f, 0.3f, 0.3f, 1f);

            node.UpdateTransform((panel) => {
                panel.ElementPosition = new Vector3(0, (LabelHeight * 4) + 20, -0.01f);
                panel.ElementSize = new Vector3(340, 0, 0.01f);
                panel.ElementScale = new Vector3(300, LabelHeight, 0.01f);
                panel.elemenentCentering = new Vector3(1, 1, 0);
            });
        });

        public ElementNode<InputBox> SubmitButtonText = new ElementNode<InputBox>(nodeTypeValue: new InputBox(), Init: (guiClassInstance, self, node) => {

            node.SetParent(guiClassInstance.SubmitButton);

            node.inputfieldComponent.textComponent.fontSize = 24;
            node.inputfieldComponent.text = "Login";

            node.inputfieldComponent.interactable = false;
            node.inputfieldComponent.GetComponent<Image>().enabled = false;

            node.UpdateTransform((panel) => {
                panel.ElementPosition = new Vector3(0, 0, -0.01f);
                panel.ElementSize = new Vector3(300, LabelHeight, 0.01f);
                //panel.elementScale = new Vector3(300, LabelHeight, 0.01f);
                panel.elemenentCentering = new Vector3(1, 1, 0);
            });

            node.inputFieldObject.SetEventListener(guiClassInstance, new EventModel
            {
                HandleMouseDown = (g) =>
                {
                    Debug.Log($"Clicked {self.name} !");

                },
                HandleMouseUp = (g) => {

                },
                HandleOnFocus = (g) =>
                {
                    Debug.Log($"Focused {self.name}");
                    Material material = g.SubmitButton.nodeTypeInstance.panelBackgroundGameObject.GetComponent<MeshRenderer>().material;
                    material.color = new Color(0.7f, 0.7f, 0.7f);
                },
                HandleOnBlur = (g) =>
                {
                    Debug.Log("here");
                    Debug.Log($"Focused {self.name}");
                    Material material = g.SubmitButton.nodeTypeInstance.panelBackgroundGameObject.GetComponent<MeshRenderer>().material;
                    material.color = new Color(0.3f, 0.3f, 0.3f);
                },

                HandleKeyDown = (g) =>
                {
                    Debug.Log("key down");
                }
            });
        });

        //OfflineModeButton[

        public ElementNode<GuiPanel> OfflineButton = new ElementNode<GuiPanel>(nodeTypeValue: new GuiPanel(), Init: (guiClassInstance, self, node) => {

            node.SetParent(guiClassInstance.MainMenuLoginPanel);

            MeshRenderer mr = node.panelBackgroundGameObject.GetComponent<MeshRenderer>();
            mr.material.color = new Color(0.3f, 0.3f, 0.3f, 1f);

            node.UpdateTransform((panel) => {
                panel.ElementPosition = new Vector3(0, (LabelHeight * 5) + 20 + 2 + 40, -0.01f);
                panel.ElementSize = new Vector3(340, 0, 0.01f);
                panel.ElementScale = new Vector3(180, LabelHeight, 0.01f);
                panel.elemenentCentering = new Vector3(1, 1, 0);
            });
        });

        public ElementNode<InputBox> OfflineButtonText = new ElementNode<InputBox>(nodeTypeValue: new InputBox(), Init: (guiClassInstance, self, node) => {

            node.SetParent(guiClassInstance.OfflineButton);

            node.inputfieldComponent.textComponent.fontSize = 24;
            node.inputfieldComponent.text = "Offline Mode";

            node.inputfieldComponent.interactable = false;
            node.inputfieldComponent.GetComponent<Image>().enabled = false;

            node.UpdateTransform((panel) => {
                panel.ElementPosition = new Vector3(0, 0, -0.01f);
                panel.ElementSize = new Vector3(180, LabelHeight, 0.01f);
                //panel.elementScale = new Vector3(300, LabelHeight, 0.01f);
                panel.elemenentCentering = new Vector3(1, 1, 0);
            });
            
            node.inputFieldObject.SetEventListener(guiClassInstance, new EventModel
            {
                HandleMouseDown = (g) =>
                {
                    Debug.Log($"Clicked {self.name} !");

                },
                HandleMouseUp = (g) => {

                },
                HandleOnFocus = (g) =>
                {
                    Debug.Log($"Focused {self.name}");
                    Material material = g.OfflineButton.nodeTypeInstance.panelBackgroundGameObject.GetComponent<MeshRenderer>().material;
                    material.color = new Color(0.7f, 0.7f, 0.7f);
                },
                HandleOnBlur = (g) =>
                {
                    Debug.Log("here");
                    Debug.Log($"Focused {self.name}");
                    Material material = g.OfflineButton.nodeTypeInstance.panelBackgroundGameObject.GetComponent<MeshRenderer>().material;
                    material.color = new Color(0.3f, 0.3f, 0.3f);
                },

                HandleKeyDown = (g) =>
                {
                    Debug.Log("key down");
                }
            });
            
        });


    }


    //Main Menu Panel
    public partial class GuiElementDefinitions
    {




    }

}
