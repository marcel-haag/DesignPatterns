﻿using System;
using System.Collections;

namespace DesignPatterns.Behavioural_Patterns.Object_Based
{
    /// Iterator Design Pattern.
    class MainApp
    {
        /// Entry point into console application.
        static void Main()
        {
            ConcreteAggregate a = new ConcreteAggregate();
            a[0] = "Item A";
            a[1] = "Item B";
            a[2] = "Item C";
            a[3] = "Item D";

            // Create Iterator and provide aggregate
            Iterator i = a.CreateIterator();
            Console.WriteLine("Iterating over collection:");
            object item = i.First();
            while (item != null)
            {
                Console.WriteLine(item);
                item = i.Next();
            }
            // Wait for user
            Console.ReadKey();
        }
    }

    /// The 'Aggregate' abstract class
    abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
    }

    /// The 'ConcreteAggregate' class
    class ConcreteAggregate : Aggregate
    {
        private ArrayList _items = new ArrayList();
        public override Iterator CreateIterator()
        {
            return new ConcreteIterator(this);
        }
        // Gets item count
        public int Count
        {
            get { return _items.Count; }
        }
        // Indexer
        public object this[int index]
        {
            get { return _items[index]; }
            set { _items.Insert(index, value); }
        }
    }

    /// The 'Iterator' abstract class
    abstract class Iterator
    {
        public abstract object First();
        public abstract object Next();
        public abstract bool IsDone();
        public abstract object CurrentItem();
    }

    /// The 'ConcreteIterator' class
    class ConcreteIterator : Iterator
    {
        private ConcreteAggregate _aggregate;
        private int _current = 0;

        // Constructor
        public ConcreteIterator(ConcreteAggregate aggregate)
        {
            this._aggregate = aggregate;
        }

        // Gets first iteration item
        public override object First()
        {
            return _aggregate[0];
        }

        // Gets next iteration item
        public override object Next()
        {
            object ret = null;
            if (_current < _aggregate.Count - 1)
            {
                ret = _aggregate[++_current];
            }
            return ret;
        }

        // Gets current iteration item
        public override object CurrentItem()
        {
            return _aggregate[_current];
        }

        // Gets whether iterations are complete
        public override bool IsDone()
        {
            return _current >= _aggregate.Count;
        }
    }
}
