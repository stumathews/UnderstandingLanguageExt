using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace Tutorial01
{
    /// <summary>
    /// A box can hold 1 thing only
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Box<T>
    {
        public Box(T newItem)
        {
            Item = newItem;
            IsEmpty = false;
        }

        public Box() { }

        private T _item;

        public T Item
        {
            get => _item;
            set
            {
                _item = value;
                IsEmpty = false;
            }
        }
        
        public bool IsEmpty = true;

        
    }

    public static class BoxMethods
    {
        public static Box<TB> Select<TA, TB>(this Box<TA> box, Func<TA, TB> map)
        {
            // Validate/Check if map should be run
            if (box.IsEmpty)
            {
                // No, return the empty box
                return new Box<TB>();
            }
            
            // Extract
            TB transformedItem = map(box.Item);
            
            // Map
            return new Box<TB>(transformedItem);
        }
    }

}
