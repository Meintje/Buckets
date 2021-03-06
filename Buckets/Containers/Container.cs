﻿using Buckets.CustomEventArgs;
using Buckets.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buckets
{
    public abstract class Container
    {
        protected int _content;
        protected int addedAmount;

        public int Capacity { get; protected set; }
        public int Content 
        {
            get { return _content; } 
            set
            {
                ValidateAndSetContent(value, 0);
            }
        }

        public event EventHandler<FullEventArgs> FullEventHandler;
        public event EventHandler<OverflowedEventArgs> OverflowedEventHandler;
        public event EventHandler<OverflowingEventArgs> OverflowingEventHandler;

        public Container()
        {
            _content = 0;
        }

        public void Fill(int amount)
        {
            ValidateAndSetContent(amount, _content);
        }

        // TODO: Think of a more descriptive name for baseContent
        protected void ValidateAndSetContent(int amount, int baseContent)
        {
            if (amount < 0) { throw new NegativeAmountException("A container cannot be filled with a negative amount."); }

            int amountThatWillBeSpilled = (baseContent + amount) - Capacity;

            if (amountThatWillBeSpilled > 0)
            {
                int amountThatCanBeAdded = Capacity - baseContent;

                var overflowingEventArguments = Overflowing(new OverflowingEventArgs(amountThatWillBeSpilled, amountThatCanBeAdded));

                switch (overflowingEventArguments.Response)
                {
                    case OverflowingEventResponse.Cancel:
                        addedAmount = 0;
                        break;
                    case OverflowingEventResponse.IgnoreOverflow:
                        addedAmount = amount;
                        _content = Capacity;
                        Overflowed(new OverflowedEventArgs(amountThatWillBeSpilled));
                        break;
                    case OverflowingEventResponse.FillToBrim:
                        addedAmount = amountThatCanBeAdded;
                        _content = Capacity;
                        break;
                    case OverflowingEventResponse.FillPartially:
                        addedAmount = overflowingEventArguments.AmountToBeAdded;
                        _content = baseContent + overflowingEventArguments.AmountToBeAdded;
                        break;
                }
            }
            else
            {
                _content = baseContent + amount;
                addedAmount = amount;
            }

            if (_content == Capacity)
            {
                Full(new FullEventArgs());
            }
        }

        public void Empty()
        {
            _content = 0;
        }

        public void Empty(int amount)
        {
            if (amount < 0) { throw new NegativeAmountException("A container cannot be emptied with a negative amount."); }

            if ((Content - amount) < 0)
            {
                Content = 0;
            }
            else
            {
                Content -= amount;
            }
        }

        protected void Full(FullEventArgs e)
        {
            FullEventHandler?.Invoke(this, e);
        }

        protected void Overflowed(OverflowedEventArgs e)
        {
            OverflowedEventHandler?.Invoke(this, e);
        }

        protected OverflowingEventArgs Overflowing(OverflowingEventArgs e)
        {
            OverflowingEventHandler?.Invoke(this, e);

            return e;
        }
    }
}
