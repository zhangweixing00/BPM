using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuleExpression
{
    public class StackSimple<T>
    {
        //栈默认的初始容量大小
        private const int DEFAULT_SIZE = 100;

        private T[] array;

        private int index;

        /// <summary>
        /// 初始化 Stack 类的新实例，该实例为空并且具有默认初始容量。
        /// </summary>
        public StackSimple()
            : this(DEFAULT_SIZE) { }

        /// <summary>
        /// 初始化 Stack 类的新实例，该实例为空并且具有指定的初始容量。
        /// </summary>
        /// <param name="size"></param>
        public StackSimple(int size)
        {
            index = -1;
            this.array = new T[size];
        }

        private int _count;
        /// <summary>
        /// 获取 Stack 中包含的元素数。
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// 清空栈
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = default(T);
                this.index = -1;
                this._count = 0;
            }
        }

        public Boolean IsEmpty()
        {
            return this._count == 0;
        }

        /// <summary>
        /// 返回位于 Stack 顶部的对象但不将其移除。
        /// </summary>
        /// <returns>位于 Stack 顶部的 Object。</returns>
        public T Peep()
        {
            return this.index < 0 ? default(T) : this.array[this.index];
        }

        /// <summary>
        /// 移除并返回位于 Stack 顶部的对象。
        /// </summary>
        /// <returns>从 Stack 的顶部移除的 Object。 </returns>
        public T Pop()
        {
            T obj = this.index < 0 ? default(T) : this.array[this.index];
            if (null != obj)
            {
                this.index--;
                this._count--;
                //this.array[this.index] = default(T);
            }
            return obj;
        }

        /// <summary>
        /// 将对象插入 Stack 的顶部。
        /// </summary>
        public void Push(T obj)
        {
            if (null != obj)
            {
                if (this.index >= this.array.Length - 1)
                {
                    T[] temp = new T[this.array.Length * 2];
                    int count = 0;
                    foreach (T item in array)
                    {
                        temp[count++] = item;
                    }
                    this.array = temp;
                }
                array[++index] = obj;
                this._count++;
            }
        }
    }
}
