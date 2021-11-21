using System;
using System.Collections.Generic;
using System.Text;

namespace SerialToKeyboard
{
    /// <summary>
    /// Fixed-size lock-free queue. Front of the queue can be modified.
    /// 
    /// Throws exceptions on errors.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    public class FixedQueue <T>
    {
        private T[] items_ = null;
        int pushIndex_ = 0;
        int popIndex_ = 0;

        /// <summary>
        /// Fixed queue of given maximum size.
        /// </summary>
        /// <param name="size">Maximum size of the queue.</param>
        public FixedQueue(int size)
        {
            items_ = new T[size];
        }

        /// <summary>
        /// True iff the queue is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return pushIndex_ == popIndex_; }
        }

        /// <summary>
        /// Push item at the back of the queue.
        /// </summary>
        /// <param name="t"></param>
        public void PushBack(T t)
        {
            int nextIndex = (pushIndex_ + 1) % items_.Length;
            if (nextIndex == popIndex_)
            {
                throw new Exception("FixedQueue: tried to insert item into full queue.");
            }
            items_[pushIndex_] = t;
            pushIndex_ = nextIndex;
        }

        /// <summary>
        /// Pop item off the Front of the queue.
        /// </summary>
        /// <returns>Front element of the queue.</returns>
        public T PopFront()
        {
            if (pushIndex_ == popIndex_)
            {
                throw new Exception("FixedQueue: tried to pop item off from empty queue.");
            }
            T r = items_[popIndex_];
            popIndex_ = (popIndex_ + 1) % items_.Length;
            return r;
        }

        /// <summary>
        /// Front item of the queue, read/write.
        /// </summary>
        public T Front
        {
            get
            {
                if (pushIndex_ == popIndex_)
                {
                    throw new Exception("FixedQueue: tried to access front item from empty queue.");
                }
                return items_[popIndex_];
            }
            set
            {
                if (pushIndex_ == popIndex_)
                {
                    throw new Exception("FixedQueue: tried to update empty queue.");
                }
                items_[popIndex_] = value;
            }
        }
#if (false)
        /// <summary>
        /// Count the items available to be popped.
        /// </summary>
        /// <returns>Number of the items in the queue.</returns>
        public int Count()
        {
            int r = 0;
            int pop_index = popIndex_;
            while (pushIndex_ != pop_index)
            {
                pop_index = (pop_index + 1) % items_.Length;
            }
            return r;
        }
#endif
    }
}
