using System;
using System.Collections.Generic;
using System.Text;

namespace neo_to_redis
{
    public class BinaryPropertyAttribute : Attribute
    {
        /// <summary>
        /// Order
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="order">Order</param>
        public BinaryPropertyAttribute(int order)
        {
            Order = order;
        }
    }
}
