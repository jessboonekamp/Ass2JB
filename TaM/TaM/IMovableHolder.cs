namespace TaM {
    interface IMoveableHolder {
        int TheseusRow { get; }
        int TheseusColumn { get; }
        int MinotaurRow { get; }
        int MinotaurColumn { get; }
        int MoveCount { get; }
        void MoveTheseus(Directions direction);
        bool IsTheseusDead();
        bool HasTheseusEscaped();
    }
}