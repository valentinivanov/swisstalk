using Swisstalk.Foundation.Metadata;

namespace Swisstalk.Foundation.Tasks.Token
{
    public interface ICompleteableExecutionToken : IExecutionToken, ICompleteable<TokenState>
    {
    }

    public class CompleteableExecutionToken : ICompleteableExecutionToken
    {
        private TokenState _state;

        public CompleteableExecutionToken()
        {
            _state = TokenState.Active;
        }

        public void CompleteWith(TokenState state)
        {
            _state = state;
        }

        public TokenState State
        {
            get
            {
                return _state;
            }
        }

        public void Cancel()
        {
            _state = TokenState.Cancelled;
        }
    }
}
