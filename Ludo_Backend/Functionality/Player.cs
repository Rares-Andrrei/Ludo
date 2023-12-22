﻿using System.Collections.Generic;

namespace Ludo_Backend.Functionaity
{
    public class Player
    {
        public enum PlayerColor
        {
            Blue,
            Yellow,
            Red,
            Green,
        }

        public Player()
        {
            Pawns = new List<Pawn>();
        }

        public string Name { get; set; }
        public byte StartPosition { get; set; }
        public byte EndPosition { get; set; }
        public List<Pawn> Pawns { get; set; }
        public PlayerColor Color { get; set; }
    }
}
