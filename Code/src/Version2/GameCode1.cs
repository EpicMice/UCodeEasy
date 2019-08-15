using gamecore;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace gamecore
{

    public partial class GameCore
    {

        public GameStateObject GameState = null;

    }

    public partial class GameCore
    {
        public static Action<GameCore> InitGameCore = (gamecore) => {

        };

        public GameCore()
        {
            GameCore.InitGameCore(this);

            this.Start();
        }

        public void Start()
        {
            Debug.Log("here"+GameState);
            if (this.GameState != null)
            {
                this.GameState.RunGameState();
            }
            else
            {
                Debug.Log("No Game State to run");
            }
        }
    }

    public class GameCoreObject
    {

    }

    public class GameCoreObject<T> : GameCoreObject
    {
        public GameCoreObject(T t, Action<GameCore, T> Init)
        {

            GameCore.InitGameCore += (g) =>
            {
                Init(g, t);
            };
        }
    }
    
    public class GameStateObject
    {
        public const string MAIN_MENU_STATE_1 = nameof(GameStateObject.MAIN_MENU_STATE_1);

        public string currentgamestate = MAIN_MENU_STATE_1;



        public GameStateObject(Action<GameCore, GameStateObject> initObject)
        {
            initObject += (gamecore, self) => {
                gamecore.GameState = this;
            };

            new GameCoreObject<GameStateObject>(this, initObject);

        }

        public void RunGameState()
        {

        }

        public void MoveState(string stateFrom, string stateTo)
        {
            //Perform functions on things that need to know that the state has changed.
        }
    }

    
}