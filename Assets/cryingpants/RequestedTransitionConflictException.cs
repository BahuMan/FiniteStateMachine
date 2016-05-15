using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cryingpants
{
    /**
     * This Exception is thrown when 2 OnStateUpdate() methods each return a different state transition
     */
    public class RequestedTransitionConflictException<StateEnum>: Exception
    {

        public StateEnum current, requested1, requested2;

        public RequestedTransitionConflictException(string message, StateEnum current, StateEnum requested1, StateEnum requested2): base(message)
        {
            this.current = current;
            this.requested1 = requested1;
            this.requested2 = requested2;
        }
    }
}
