using System;
using System.Collections.Generic;
using System.Text;

namespace TaM {
    public class Square{
        public bool Top;
        public bool Left;
        public bool Bottom;
        public bool Right;
        public bool Minotaur;
        public bool Theseus;
        public bool Exit;

        public Square(bool top, bool left, bool bottom, bool right, bool hasMinotaur, bool hasTheseus, bool isExit)
        {
            Top = top;
            Left = left;
            Bottom = bottom;
            Right = right;
            Minotaur = hasMinotaur;
            Theseus = hasTheseus;
            Exit = isExit;
        }

        public void theseusLeftSquare(){
            Theseus = false;
        }
        public void theseusEnteredSquare(){
            Theseus = true;
        }
        public void minotaurLeftSquare() {
            Minotaur = false;
        }
        public void minotaurEnteredSquare() {
            Minotaur = true;
        }

    }
}
