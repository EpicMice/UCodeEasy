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

        public GameStateObject DungeonGameState = new GameStateObject((gamecore, self)=> {
            
            gamecore.GuiElementDefs.OfflineButtonText.nodeTypeInstance.inputFieldObject.SetEventListener(gamecore.GuiElementDefs, new EventModel
            {
                HandleMouseDown = (g) =>
                {
                    gamecore.GuiElementDefs.MainMenuLoginPanel.nodeTypeInstance.SetActive(false);
                },
                HandleMouseUp = null,
                HandleOnFocus = null,
                HandleOnBlur = null,
                HandleKeyDown = null,
            });
            
        });
    }
}