using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Sokoban {
	/// <summary>
	/// Logic for loading Tiled map editor (.tmx) files.
	/// </summary>
	public static class Tilemap {
		public static int[,] Load(string path) {
			XDocument xml = XDocument.Load(path);

			// TODO: Null checks and validation.
			XElement layer = xml.Root.Element("layer");
			int width = int.Parse(layer.Attribute("width").Value);
			int height = int.Parse(layer.Attribute("height").Value);
			int[] data = ReadCsv(layer.Element("data").Value);

			if (data.Length != width * height)
				throw new InvalidDataException("Length of data does not match map size.");

			int[,] tiles = new int[height, width];
			for (int i = 0; i < width * height; i++) {
				tiles[i / width, i % width] = data[i];
			}

			return tiles;
		}

		private static int[] ReadCsv(string data) {
			return data.Split(',')
				.Select(int.Parse)
				.ToArray();
		}
	}
}