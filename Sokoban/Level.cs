using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Sokoban {
	public class Level : Drawable {
		public const int TileSize = 32;

		public int[,] TileData { get; }
		public List<Crate> Crates { get; }

		public Player Player { get; } = new Player();

		private readonly List<Sprite> tileSprites;


		public Level(int[,] tileData) {
			TileData = tileData;
			tileSprites = new List<Sprite>(tileData.Length);
			Crates = new List<Crate>();
			Player.Level = this;

			Reset();
		}

		public int Width => TileData.GetLength(1);

		public int Height => TileData.GetLength(0);

		public Vector2i Size => new Vector2i(Width, Height);

		public Vector2i PixelSize => Size * Tiles.TileSize;

		public void Draw(RenderTarget target, RenderStates states) {
			foreach (Sprite sprite in tileSprites)
				target.Draw(sprite, states);

			foreach (Crate crate in Crates)
				target.Draw(crate, states);
		}

		public void Reset() {
			// Rebuild the level from the tile data.
			tileSprites.Clear();
			Crates.Clear();

			for (int y = 0; y < Height; y++)
			for (int x = 0; x < Width; x++) {
				int tileId = TileData[y, x];
				if (tileId == 0) continue; // Nothing

				var bgSprite = new Sprite {
					Position = new Vector2f(x, y) * TileSize,
					Texture = tileId switch {
						Tiles.Ground => Resources.groundTexture,
						Tiles.Wall => Resources.bricksTexture,
						Tiles.Target => Resources.targetTexture,
						Tiles.Crate => Resources.groundTexture,
						Tiles.Player => Resources.groundTexture,
						_ => null
					}
				};
				tileSprites.Add(bgSprite);

				if (tileId == Tiles.Crate) {
					var crate = new Crate(this, new Vector2i(x, y));
					Crates.Add(crate);
				}
				else if (tileId == Tiles.Player) {
					Player.Position = new Vector2i(x, y);
				}
			}
		}

		public bool CheckSolved() {
			foreach (Crate crate in Crates) {
				if (TileData[crate.Position.Y, crate.Position.X] != Tiles.Target)
					return false;
			}

			return true;
		}
	}
}