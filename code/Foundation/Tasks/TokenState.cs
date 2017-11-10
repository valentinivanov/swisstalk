namespace Swisstalk.Foundation.Tasks
{
    public enum TokenState
    {
        Undefined,
        Active,
        Cancelled,
        Done
    }

    public static class TokenStateExtension
    {
        private static TokenState[] TaskStateToTokenStateMap = new TokenState[]
        {
            TokenState.Undefined, //Undefined = 0,
            TokenState.Active, //Started,
            TokenState.Active, //Running,
            TokenState.Done,//Done,
            TokenState.Done,//Stopped,
            TokenState.Done//Disposed
        };

        public static bool IsActive(this TokenState state)
        {
            return (state == TokenState.Active);
        }

        public static bool IsComplete(this TokenState state)
        {
            return state.IsDone() || state.IsCancelled();
        }

        public static bool IsDone(this TokenState state)
        {
            return (state == TokenState.Done);
        }

        public static bool IsCancelled(this TokenState state)
        {
            return (state == TokenState.Cancelled);
        }

        public static TokenState ToTokenState(this TaskState taskState)
        {
            return TaskStateToTokenStateMap[(int)taskState];
        }

        public static TokenState GetMaxStateValue()
        {
            return TokenState.Cancelled;
        }
    }
}
