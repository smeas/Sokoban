using SFML.System;

namespace Sokoban {
	public class Crate : MovableTileSprite {
		public Crate(Level level, Vector2i position) : base(Resources.crateTexture) {
			Level = level;
			Position = position;
		}
	}
}