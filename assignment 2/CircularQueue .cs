using System;
using System.Threading;

namespace Assignment2
{

    public class CircularQueue
    {
        private int head;
        private int tail;
        private int[] queue;
        private int length;
        static Object QueueLock = new Object(); //  Queue thread safe

      public CircularQueue(int Length)
        {

            Initialize(Length);

        }

        
        public CircularQueue()
        {

        }

        
        private void Initialize(int Length)
        {
            head = tail = -1;
            this.length = Length;
            queue = new int[Length];
        }

      
        public int Enqueue(int value)
        {
            lock (QueueLock)
            {
                if ((head == 0 && tail == length - 1) || (tail + 1 == head))
                {
                    
                    return -1; // Queue is full
                }
                else
                {


                    if (tail == length - 1)
                        tail = 0;
                    else
                        tail++;

                    queue[tail] = value;
                  
                    if (head == -1)
                        head = 0;

                    return tail;
                }

            }
        }

       
        public int Dequeue()
        {
            lock (QueueLock)
            {
                int value;

                if (head == -1)
                {
                    value = -1;
                }
                else
                {
                    value = queue[head];
                    queue[head] = 0;

                    if (head == tail)
                        head = tail = -1;
                    else
                        if (head == length - 1)
                            head = 0;
                        else
                            head++;
                }

                return value;
            }
        }

        
        public string ViewQueueElements()
        {
            lock (QueueLock)
            {
                int i;
                string result = " Queue Emelemnts: ";
                if (head == -1)
                {
                    result = " No Elements Found !";
                }
                else
                {
                    if (tail < head)
                    {
                        //for(i = head; i <= size - 1; i++)
                        for (i = 0; i <= length - 1; i++)
                            result += queue[i] + " ";
                    }
                    else
                        //for(i = head; i <= tail; i++)
                        for (i = 0; i <= tail; i++)
                            result += queue[i] + " ";


                }
                return result + "\n";
            }
        }


        public void StartConsoleTest()
        {
            Initialize(3);
            Enqueue(1);
            Console.WriteLine();
            Console.Write(ViewQueueElements());
            Enqueue(2);
            Console.WriteLine();
            Console.Write(ViewQueueElements());
            Enqueue(3);
            Console.WriteLine();
            Console.Write(ViewQueueElements());
            Dequeue();
            Console.WriteLine();
            Console.Write(ViewQueueElements());
            Dequeue();
            Console.WriteLine();
            Console.Write(ViewQueueElements());
            Dequeue();
            Console.WriteLine();
            Console.Write(ViewQueueElements());
            Console.WriteLine();
          
          



        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            CircularQueue circularQueue = new CircularQueue();

            Thread[] threads = new Thread[1]; // you can use any number of threads

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ThreadStart(circularQueue.StartConsoleTest));
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }
          
            Console.WriteLine();
            Console.Write(" Press any key to continue...");
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}