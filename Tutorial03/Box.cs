using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks.Dataflow;

namespace Tutorial01
{
    /// <summary>
    /// A box can hold 1 thing only
    /// </summary>
    /// <typeparam name="T">The type of the thing</typeparam>
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
        /// <summary>
        /// Validate, Extract, Transform and Lift (If Valid)
        /// </summary>
        public static Box<TB> Select<TA, TB>(this Box<TA> box, Func<TA, TB> map)
        {
            // Validate
            if (box.IsEmpty)
                return new Box<TB>();
            
            // Extract
            var extracted = box.Item;

            // Transform
            TB transformedItem = map(extracted);
            
            // Lift
            return new Box<TB>(transformedItem);
        }

        /// <summary>
        /// Validate, Extract, Transform and Lift (If Valid)
        /// Check/Validate then transform to T and lift into Box<T>
        /// </summary>
        public static Box<TB> Bind<TA, TB>(this Box<TA> box, Func<TA, Box<TB>> bind /*liftAndTransform*/)
        {
            // Validate
            if(box.IsEmpty)
                return new Box<TB>();
            
            //Extract 
            TA extract = box.Item;

            // Transform and lift
            Box<TB> transformedAndLifted = bind(extract);

            return transformedAndLifted;
        }

        /// <summary>
        /// Validate, Extract, Transform and automatic Lift (If Valid) 
        /// </summary>
        public static Box<TB> Map<TA, TB>(this Box<TA> box, Func<TA, TB> select /*Transform*/)
        {
            // Validate
            if(box.IsEmpty)
                return new Box<TB>();
            
            // Extract
            TA extract = box.Item;

            // Transform
            TB transformed = select(extract);

            // Lift
            return new Box<TB>(transformed);
        }
    }

}
