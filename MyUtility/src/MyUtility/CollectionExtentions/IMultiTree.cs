using System;
using System.Collections.Generic;
using System.Text;

namespace MyUtility.CollectionExtentions
{
    public interface IMultiTree<T>
    {
        List<T> Children { get; set; }
    }
}
