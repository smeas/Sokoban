using SFML.Graphics;
using SFML.System;

namespace Sokoban {
	public class Player : MovableTileSprite {
		public Player() : base(Resources.playerTexture) { }
	}
}