using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    //structure with Keyvalue is described globally
    public struct KeyValue<k,v>
    {
        public k Key { get; set; }
        public v Value { get; set; }
    }
    public class MyMapNode<K,V>
    {
        //creating size as local variable
        private readonly int size;

        //creating items array of type LinkedList which containing datatype same as struct
        private readonly LinkedList<KeyValue<K, V>>[] items;

        //creating constructor MyMapNode and its assigned value
        public MyMapNode(int size)
        {
            this.size=size;
            this.items = new LinkedList<KeyValue<K, V>>[size];
        }

        //GetArrayPosition is the starting of hash table program which congtain inbuilt GetHashCode()
        protected int GetArrayPosition(K key)
        {
            //position will store the output of modulo division of key.GetHashCode() with size of array 
            int position = key.GetHashCode() % size;

            //returining the position using abs from math class whose value cannot be negative
            return Math.Abs(position);
        }

        public V Get(K key)
        {
            //position of keyvalue using hashing function
            int position = GetArrayPosition(key);

            //creating single linkedlist object
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);

            foreach(KeyValue<K,V> item in linkedList)
            {
                //returns value if item if it matches the key
                if(item.Key.Equals(key))
                {
                    return item.Value;
                }
            }
            return default(V);
        }

        //Adding key and value pair to the Linked List
        public void Add(K key,V value)
        {
            //geting position of array using hashing and obtaining linked list object
            int position = GetArrayPosition(key);
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);

            //creating object named item of keyvalue struct with key and value as input
            KeyValue<K, V> item = new KeyValue<K, V>() { Key = key, Value = value };

            //checking wheather any duplicate key
            if(linkedList.Count != 0)
            {
                foreach(KeyValue<K,V> item1 in linkedList)
                {
                    if(item1.Key.Equals(key))
                    {
                        Remove(key);
                        break;
                    }
                }
            }
            linkedList.AddLast(item);
        }

        public void Remove(K key)
        {
            int position = GetArrayPosition(key);
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);

            //declaring itemFound variable to be boolean false
            bool itemFound = false;
            KeyValue<K, V> foundItem = default(KeyValue<K, V>);

            //checking input key is already present
            foreach(KeyValue<K,V> item in linkedList)
            {
                if(item.Key.Equals(key))
                {
                    itemFound = true;
                    foundItem = item;
                }
            }
            if(itemFound)
            {
                linkedList.Remove(foundItem);
            }
        }

        //taking input as integer position in range of array items index
        protected LinkedList<KeyValue<K,V>> GetLinkedList(int position)
        {
            //checks there is any existing value in LinkedList
            LinkedList<KeyValue<K, V>> linkedList = items[position];

            if(linkedList == null)
            {
                //if there no value in LinkedList then create linkedList object in respective position
                linkedList = new LinkedList<KeyValue<K, V>>();
                items[position] = linkedList;
            }
            return linkedList;
        }

        //checking if a key exist in the array Linked List
        public bool Exists(K key)
        {
            int position = GetArrayPosition(key);
            LinkedList<KeyValue<K, V>> linkedList = GetLinkedList(position);
            foreach(KeyValue<K,V> item in linkedList)
            {
                if(item.Key.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }

        //displaying the LinkedList object variable inside of item array
        public void Display()
        {
            foreach(var linkedList in items)
            {
                if(linkedList != null)
                {
                    foreach(var element in linkedList)
                    {
                        string res = element.ToString();
                        if (res != null)
                            Console.WriteLine(element.Key + " " + element.Value);
                    }
                }
            }
        }
    }
}
