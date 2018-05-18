using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class QueueExtensions
{
    public static T NullSafePeek<T>(this Queue<T> queue)
    {
        if (queue.Any())
        {
            return queue.Peek();
        }
        else
        {
            return default(T);
        }
    }
}
