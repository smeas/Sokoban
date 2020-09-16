using SFML.Graphics;
using SFML.System;

namespace Sokoban {
	public class TileSprite : Drawable {
		private Sprite sprite;
		private Vector2i position;

		public TileSprite() { }

		public TileSprite(Texture texture) {
			Sprite = new Sprite(texture);
		}

		public Sprite Sprite {
			get => sprite;
			set {
				sprite = value;
				if (sprite != null) {
					Position = Position;
				}
			}
		}

		public Vector2i Position {
			get => position;
			set {
				position = value;
				Sprite.Position = (Vector2f)(position * Tiles.TileSize);
			}
		}

		public void Draw(RenderTarget target, RenderStates states) {
			if (Sprite != null)
				target.Draw(sprite, states);
		}
	}
}