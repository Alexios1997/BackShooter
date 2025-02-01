using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace AlexHyperCasualGames
{
    public static class Constants
    {

        public enum GameState
        {
            LoadingLevel,
            StartUIAnim,
            Shopping,
            WaitingForInput,
            Playing,
            Celebrating
        }

        public enum PlayerState
        {
            NotSet,
            Idle,
            Run,
            Jump,
            Lose,
            Win
        }
        public enum Operations
        {
            Minus,
            Add,
            Multiply,
            Divide
        }

        

    }
}
