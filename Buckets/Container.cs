using System;
using System.Collections.Generic;
using System.Text;

namespace Buckets
{
    public abstract class Container
    {
        private int _content;

        public Container()
        {
            Capacity = 12;
            _content = 0;
        }

        public Container(int content) : this()
        {
            _content = content;
        }

        public Container(int capacity, int content)
        {
            Capacity = capacity;
            _content = content;
        }

        public int Content 
        {
            get { return _content; } 
            set
            { 
                _content = value;
                ContentChanged();
            }
        }
        public int Capacity { get; protected set; }

        public event EventHandler<ContentEventArgs> FullEventHandler;
        public event EventHandler<ContentEventArgs> OverflowedEventHandler;

        // TODO: Make it so an overflowing event can be cancelled, allowed to fill the bucket
        // to the brim (or just below the brim), or allow the bucket to overflow.
        public event EventHandler<ContentEventArgs> OverflowingEventHandler;

        public void Fill(int amount)
        {
            Content += amount;
        }

        public void Empty()
        {
            Content = 0;
        }

        public void Empty(int amount)
        {
            Content -= amount;

            if (Content < 0)
            {
                Content = 0;
            }
        }

        protected void ContentChanged()
        {
            if (Content == Capacity)
            {
                Full(new ContentEventArgs("Container is full.", 0));
            }
            else if (Content > Capacity)
            {
                if (Overflowing(new ContentEventArgs("Container is overflowing.", Content - Capacity)))
                {
                    return;
                }
                else
                {
                    Content = Capacity;
                    Overflowed(new ContentEventArgs("Container overflowed.", Content - Capacity));
                }
            }
        }

        protected void Full(ContentEventArgs e)
        {
            FullEventHandler?.Invoke(this, e);
        }

        protected void Overflowed(ContentEventArgs e)
        {
            OverflowedEventHandler?.Invoke(this, e);
        }

        protected bool Overflowing(ContentEventArgs e)
        {
            OverflowingEventHandler?.Invoke(this, e);

            return !(OverflowingEventHandler == null);
        }
    }

    public class ContentEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public int SpilledAmount { get; private set; }

        public ContentEventArgs(string message, int spilledAmount)
        {
            Message = message;
            SpilledAmount = spilledAmount;
        }
    }
}
