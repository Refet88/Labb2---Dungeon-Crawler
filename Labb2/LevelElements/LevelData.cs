using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Labb2
{
    internal class LevelData
    {
        private List<LevelElement> _elements = new List<LevelElement>();
        public List<LevelElement> Elements
        {
            get { return _elements; }
        }
        public Player player { get; private set; }
        public LevelData()
        {
            player = new Player(new Positions(0, 0));
        }

        private List<Positions> seenWalls = new List<Positions>();

        public void Load(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string? line;
                int y = 5;

                while ((line = reader.ReadLine()) != null)
                {
                    for (int x = 0; x < line.Length; x++)
                    {
                        char c = line[x];
                        switch (c)
                        {
                            case '#':
                                _elements.Add(new Wall(new Positions(x, y)));
                                break;
                            case 'r':
                                _elements.Add(new Rat(new Positions(x, y), this));
                                break;
                            case 's':
                                _elements.Add(new Snake(new Positions(x, y), this));
                                break;
                            case '@':
                                player = new Player(new Positions(x, y));
                                _elements.Add(player);
                                break;
                        }
                    }
                    y++;
                }
            }
        }
        public void UpdateVisibility(VisionRange visionRange)
        {
            foreach (var element in _elements)
            {
                if (element is Wall wall)
                {
                    if (visionRange.IsInVisionRange(player.Position, wall.Position))
                    {
                        wall.IsVisible = true;
                        if (!seenWalls.Contains(wall.Position))
                        {
                            seenWalls.Add(wall.Position);
                        }
                    }
                    else if (seenWalls.Contains(wall.Position))
                    {
                        wall.IsVisible = true;
                    }
                    else
                    {
                        wall.IsVisible = false;
                    }
                }
                else if (element is Enemy enemy)
                {
                    bool wasVisible = enemy.IsVisible;
                    enemy.IsVisible = visionRange.IsInVisionRange(player.Position, enemy.Position);
                }
            }
        }
    }
}