using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderToo.Logic.Algorithms
{
    public class NodeNotSetException : Exception
    {
        public NodeNotSetException(PFNode node) : base($"Node {node} not set.") { }
        protected NodeNotSetException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
