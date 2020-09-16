using SFML.Graphics;
using SFML.System;

namespace Sokoban {
	public class MovableTileSprite : TileSprite {
		public Level Level { get; set; }

		public MovableTileSprite() { }
		public MovableTileSprite(Texture texture) : base(texture) { }

		public bool Move(int dx, int dy) => Move(new Vector2i(dx, dy));

		public bool Move(Vector2i delta) {
			Vector2i pos = Position + delta;
			if (pos.X >= 0 && pos.Y >= 0 &&
				pos.X < Level.Width && pos.Y < Level.Height) {
				// Collide with walls.
				if (Level.TileData[pos.Y, pos.X] == Tiles.Wall)
					return false;

				// Push obstructing crates (if possible).
				foreach (Crate crate in Level.Crates) {
					if (crate.Position != pos) continue;

					if (crate.Move(delta)) {
						Position = pos;
						return true;
					}
					else {
						return false;
					}
				}

				Position = pos;
				return true;
			}

			return false;
		}
	}
}