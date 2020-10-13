using System;
using System.Collections.Generic;
using System.Numerics;

namespace TaM {

    public class Level {

        public int LevelHeight { get; set; }
        public int LevelWidth { get; }
        public String LevelName = "No valid name found";
        public Square[,] levelLayout;
        List<bool> levelWalls = new List<bool>();

        public Level(string name, int width, int height, string data, Game game) {
            LevelName = name;
            LevelWidth = width;
            LevelHeight = height;
            this.createLevelLayout(data, game);
        }

        public void createLevelLayout(String data, Game game)
        {
            levelLayout = new Square[LevelWidth, LevelHeight];
            String dataPoints = data.Substring(0, 15);
            String squareData = data.Remove(0, 15);

            int MinotaurX =0;
            int MinotaurY =0;
            int TheseusX = 0;
            int TheseusY = 0;
            int ExitX = 0;
            int ExitY = 0;
            for (int j = 0; j < 3; j++) {
                if (j == 0) {
                    MinotaurY = Convert.ToInt32(dataPoints.Substring(j * 5, 2));
                    MinotaurX = Convert.ToInt32(dataPoints.Substring(j * 5 + 2, 2));
                    game.MinotaurColumn = MinotaurX;
                    game.MinotaurRow = MinotaurY;

                } else if (j == 1) {
                    TheseusY = Convert.ToInt32(dataPoints.Substring(j * 5, 2));
                    TheseusX = Convert.ToInt32(dataPoints.Substring(j * 5 + 2, 2));
                    game.TheseusColumn = TheseusX;
                    game.TheseusRow = TheseusY;
                } else {
                    ExitY = Convert.ToInt32(dataPoints.Substring(j * 5, 2));
                    ExitX = Convert.ToInt32(dataPoints.Substring(j * 5 + 2, 2));
                    game.ExitLocation = new Vector2(ExitX, ExitY);
                }
            }
            String squareDataTrimmed = squareData.Replace(" ", "");
            for (int k = 0; k < squareDataTrimmed.Length; k += 1) {
                    levelWalls.Add(Convert.ToBoolean(Convert.ToInt32(squareDataTrimmed.Substring(k, 1))));
   

            }

            int x; int y;
            for(int p = 0; p< LevelHeight; p++) {
                y = p;
                for(int q = 0; q< LevelWidth; q++) {
                    x = q;
                    this.createSquare(MinotaurX, MinotaurY, TheseusX, TheseusY, ExitX, ExitY, this.levelWalls, x, y);
                    
                }
                
            }

        }
        public void createSquare(int mX, int mY, int tX, int tY, int eX, int eY, List<bool> levelWalls, int x, int y) {

            bool top = levelWalls[0];
            bool right = levelWalls[1];
            bool bottom = levelWalls[2];
            bool left = levelWalls[3];
            levelWalls.RemoveRange(0, 4);
            bool hasMinotaur = false;
            bool hasTheseus = false;
            bool isExit = false;
            if (mX == x && mY == y) { hasMinotaur = true; };
            if (tX == x && tY == y) { hasTheseus = true; };
            if (eX == x && eY == y) { isExit = true; };
            Square tempSquare = new Square(top, left, bottom, right, hasMinotaur, hasTheseus, isExit);
            levelLayout[x, y] = tempSquare;
        }

        public String getName() {
            return LevelName;
        }

    }
}