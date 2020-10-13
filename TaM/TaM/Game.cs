using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Numerics;


namespace TaM {
    public class Game : ILevelHolder, IMoveableHolder {

        public int LevelCount { get; set; }
        public string CurrentLevelName { get; set; }
        public int LevelWidth { get; set; }  //Current Level
        public int LevelHeight { get; set; } //Current Level
        public int TheseusRow { get; set; }
        public int TheseusColumn { get; set; }
        public int MinotaurRow { get; set; }
        public int MinotaurColumn { get; set; }
        public int MoveCount { get; set; }
        public Vector2 ExitLocation;

        public Level currentLevel;

        public List<Level> levels = new List<Level>();
        public bool HasMinotaurWon = false;
        public bool HasTheseusWon = false;
        public bool gameFinished = false;


        public Game() {
            LevelCount = 0;
            LevelHeight = 0;
            LevelWidth = 0;
            CurrentLevelName = "No levels loaded";
            MoveCount = 0;
            gameLoop();
        }
        public void gameLoop() {
            
            while (!gameFinished) {

                //If no level is loaded get the first level if it exists
                if (CurrentLevelName.Equals("No levels loaded") && levels.Count > 0) {
                    Level currentLevel = levels[0];
                    CurrentLevelName = currentLevel.getName();
                    LevelWidth = currentLevel.LevelWidth;
                    LevelHeight = currentLevel.LevelHeight;
                }
                gameFinished = true;
            }

        }

        public List<String> LevelNames() {
            //Iterate levels create list of names of levels
            List<String> levelNameList = new List<String>();

            foreach (Level l in levels) {
                levelNameList.Add(l.getName());
            }

            return levelNameList;
        }

        public void AddLevel(string name, int width, int height, string data) {
            //Create a level
            
            Level level = new Level(name, width, height, data, this);
            levels.Add(level);

            //Increment LevelCount
            LevelCount += 1;
            SetLevel(name);
            
        }

        public void SetLevel(string name) {
            foreach(Level l in levels) {
                if (l.LevelName.Equals(name)){
                    currentLevel = l;
                    CurrentLevelName = l.LevelName;
                    LevelHeight = l.LevelHeight;
                    LevelWidth = l.LevelWidth;
                }
            }
            HasTheseusEscaped();
            CheckForMinotaurWon();

        }

        public Square WhatIsAt(int y, int x)
        {
            return currentLevel.levelLayout[x, y];
        }

        public void MoveTheseus(Directions direction) {
            switch (direction) {
                case Directions.UP: //UP
                if (getSquare(TheseusColumn, TheseusRow).Top) {
                    break;
                } else {
                    Moveable theseusMove = new Moveable(TheseusColumn, TheseusRow, this);
                    int[] move = theseusMove.GoUp();
                    currentLevel.levelLayout[TheseusColumn, TheseusRow].theseusLeftSquare();
                    currentLevel.levelLayout[move[1], move[0]].theseusEnteredSquare();
                    TheseusRow = move[0];
                    TheseusColumn = move[1];
                    MoveCount += 1;
                    break;
                }
                case Directions.DOWN: //DOWN
                if (getSquare(TheseusColumn, TheseusRow).Bottom) {
                    break;
                } else {
                    Moveable theseusMove = new Moveable(TheseusColumn, TheseusRow, this);
                    int[] move = theseusMove.GoDown();
                    currentLevel.levelLayout[TheseusColumn, TheseusRow].theseusLeftSquare();
                    currentLevel.levelLayout[move[1], move[0]].theseusEnteredSquare();
                    TheseusRow = move[0];
                    TheseusColumn = move[1];
                    MoveCount += 1;
                    break;
                }
                case Directions.LEFT: //LEFT
                if (getSquare(TheseusColumn, TheseusRow).Left) {
                    break;
                } else {
                    Moveable theseusMove = new Moveable(TheseusColumn, TheseusRow, this);
                    int[] move = theseusMove.GoLeft();
                    currentLevel.levelLayout[TheseusColumn, TheseusRow].theseusLeftSquare();
                    currentLevel.levelLayout[move[1], move[0]].theseusEnteredSquare();
                    TheseusRow = move[0];
                    TheseusColumn = move[1];
                    MoveCount += 1;
                    break;
                }
                case Directions.RIGHT: //RIGHT
                if (getSquare(TheseusColumn, TheseusRow).Right) {
                    break;
                } else {
                    Moveable theseusMove = new Moveable(TheseusColumn, TheseusRow, this);
                    int[] move = theseusMove.GoRight();
                    currentLevel.levelLayout[TheseusColumn, TheseusRow].theseusLeftSquare();
                    currentLevel.levelLayout[move[1], move[0]].theseusEnteredSquare();
                    TheseusRow = move[0];
                    TheseusColumn = move[1];
                    MoveCount += 1;
                    break;
                }
                case Directions.PAUSE: //PAUSE
                MoveCount += 1;
                break;
                
            }
            HasTheseusEscaped();
            CheckForMinotaurWon();


        }
        public void MoveMinotaur() {
            Vector2 T = new Vector2(TheseusColumn, TheseusRow);
            Vector2 M = new Vector2(MinotaurColumn, MinotaurRow);
            Vector2 delta = T - M;
            int[] move = null;
            if (delta.X > 0) {//Minotaur goes RIGHT
                if (!(getSquare(MinotaurColumn, MinotaurRow).Right)) {
                    Moveable minotaurMove = new Moveable(MinotaurColumn, MinotaurRow, this);
                    move = minotaurMove.GoRight();
                    currentLevel.levelLayout[MinotaurColumn, MinotaurRow].minotaurLeftSquare();
                    currentLevel.levelLayout[move[1], move[0]].minotaurEnteredSquare();
                    MinotaurRow = move[0];
                    MinotaurColumn = move[1];
                }
            }if (delta.X < 0 && move == null) { //Minotaur goes LEFT
                if (!getSquare(MinotaurColumn, MinotaurRow).Left) {
                    Moveable minotaurMove = new Moveable(MinotaurColumn, MinotaurRow, this);
                    move = minotaurMove.GoLeft();
                    currentLevel.levelLayout[MinotaurColumn, MinotaurRow].minotaurLeftSquare();
                    currentLevel.levelLayout[move[1], move[0]].minotaurEnteredSquare();
                    MinotaurRow = move[0];
                    MinotaurColumn = move[1];
                }
            }if(delta.Y < 0 && move == null) { //Minotaur Moves Up
                    if (!getSquare(MinotaurColumn, MinotaurRow).Top) {
                        Moveable minotaurMove = new Moveable(MinotaurColumn, MinotaurRow, this);
                        move = minotaurMove.GoUp();
                        currentLevel.levelLayout[MinotaurColumn, MinotaurRow].minotaurLeftSquare();
                        currentLevel.levelLayout[move[1], move[0]].minotaurEnteredSquare();
                        MinotaurRow = move[0];
                        MinotaurColumn = move[1];
                    }
            }if (delta.Y > 0 && move == null) {
                    if (!getSquare(MinotaurColumn, MinotaurRow).Bottom) {
                        Moveable minotaurMove = new Moveable(MinotaurColumn, MinotaurRow, this);
                        move = minotaurMove.GoDown();
                        currentLevel.levelLayout[MinotaurColumn, MinotaurRow].minotaurLeftSquare();
                        currentLevel.levelLayout[move[1], move[0]].minotaurEnteredSquare();
                        MinotaurRow = move[0];
                        MinotaurColumn = move[1];
                    }
                }
                
            
            CheckForMinotaurWon();
        }

        public bool IsTheseusDead() {
            if (HasMinotaurWon) {
                return true;
            } else return false;
        }

        public bool HasTheseusEscaped() {
            Vector2 TheseusLocation = new Vector2(TheseusColumn, TheseusRow);
            if (TheseusLocation == ExitLocation) {
                HasTheseusWon = true;
                return true;
            } else return false;
        }

        public void CheckForMinotaurWon() {
            Vector2 TheseusLocation = new Vector2(TheseusColumn, TheseusRow);
            Vector2 MinotaurLocation = new Vector2(MinotaurColumn, MinotaurRow);
            if (TheseusLocation == MinotaurLocation) {
                HasMinotaurWon = true;
            }
        }

        public static void Main(string[] args) {

        }

        public Square getSquare(int x, int y) {
            return currentLevel.levelLayout[x, y];
        }

    }
}
