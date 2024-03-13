using System;
using System.Collections.Generic;
using System.Text;

namespace Basic_Data_Structure
{
    //A binary search tree is an oredered tree where the nodes on the left side of the root are smaller than the root
    //And the nodes on the right side are bigger than the root, so we are going to be dong a lot of comparisons when inserting
    //new nodes

    //I am inheriting from this interface because i want to ensure that the type that will be used with this class must be comparable, like
    //type int and string

    // @author : Babatunde Ibukun
    class BinarySearchTree<T> where T : IComparable<T>
    {
        //I am Implementing Icomaparable interface, because i want the instances of this class to be comparable and also the type passed 
        //must also implement the Icomparable interface
        //This class represent each node within the tree
        internal class Node<T> : IComparable<Node<T>> where T : IComparable<T>
        {
            internal T Value { get; set; }
            internal Node<T> LeftChild { get; set; }
            internal Node<T> RightChild { get; set; }
            internal  Node<T> Parent { get; set; }

            public Node(T value)
            {
                if (value == null)
                {
                    throw new ArgumentNullException("You can not insert a null value");
                }
                Value = value;
                LeftChild = null;
                RightChild = null;
                Parent = null;
            }

            //This ensures our class can be represented as string values
            public override string ToString()
            {
                return this.Value.ToString();
            }

            public override int GetHashCode()
            {
                return this.Value.GetHashCode();
            }

            //This method is used to when we want to check the equality between two objects
            public override bool Equals(object obj)
            {
                Node<T> other = (Node<T>)obj;
                return this.CompareTo(other) == 0;
            }

            //This method compares two objects and returns an int value
            public int CompareTo(Node<T> other)
            {
                return this.Value.CompareTo(other.Value);
            }
        }

        public Node<T> Root { get; set; }

        public BinarySearchTree()
        {
            this.Root = null;
        }

        //This is the public Add method that users of this class have access to
        public void Insert(T value)
        {
            this.Root = Insert(value, null, this.Root);
        }

        private Node<T> Insert(T value, Node<T> parentNode, Node<T> currentNode)
        {
            //What is happenening is, i am checking if the tree is empty, by looking within the currentNode parameter, if it is null, then 
            //the currentNode becomes the root and i insert it into the tree
            if (currentNode == null)
            {
                currentNode = new Node<T>(value);
                currentNode.Parent = parentNode;
            }
            //what is happening here is incase the tree is not empty, then i compare the value i am trying to insert with the value of the
            //current node recursively, so that the value will be placed in the right place.
            else
            {
                int compare = value.CompareTo(currentNode.Value);
                if (compare < 0)
                {
                    currentNode.LeftChild = Insert(value, currentNode, currentNode.LeftChild);
                }
                else if(compare > 0)
                {
                    currentNode.RightChild = Insert(value, currentNode, currentNode.RightChild);
                }                
            }
            return currentNode;
        }

        //Basically the idea is that, i am trying to check if a node that has the specified value exist within the BST
        //If the specified value is equal to the root's value(compare variable must be Zero for this to happen), i break the loop
        //if the specified value is lesser than the root's value, then i search at the left side of the root and i keep on searching
        //if the specified value is greater than the root's value, then i search at the right side of the root
        public Node<T> Find(T value)
        {
            Node<T> node = this.Root;
            
            if (node == null)
            {
                throw new InvalidOperationException("You can not search for a value within an empty Tree");
            }
            while (node != null)
            {
                int compare = value.CompareTo(node.Value);
                if (compare < 0)
                {
                    node = node.LeftChild;
                }
                else if (compare > 0)
                {
                    node = node.RightChild;
                }
                else
                {
                    break;
                }
            }
            return node;
        }

        //This is the public method that will be available to users of this class
        public void Remove(T value)
        {
            //I first try to check if a node within the tree has the value i want to remove
            Node<T> nodeToDelete = Find(value);

            if (nodeToDelete == null)
            {
                throw new InvalidOperationException("The Node you want to remove does not exist");
            }

            //if a node with that value exists then i go ahead to remove it
            if (nodeToDelete != null)
            {
                Remove(nodeToDelete);
            }
        }

        //Private method that does the actual removing
        private void Remove(Node<T> node)
        {
            //If the node i want to remove has two children, then i replace the node with its right child leftmost node, if the right child has no left child
            //then i assign the node to its right child
            if (node.LeftChild != null && node.RightChild != null)
            {
                Node<T> replacement = node.RightChild;
                while (replacement.LeftChild != null)
                {
                    replacement = replacement.LeftChild;
                }
                // With the below replacement activity, we are going to have, the node and its value changed to the leftmost child on its right child, if that is available
                //or the right child if it is not available.
                //I want us to keep track, that now we have the replacement node at two places, we have it at the replaced node position and its original position, now we have to take care of removing it from its original position
                node.Value = replacement.Value;
                node = replacement;
            }

            // Now i try to check if the replacement node has a left child, if it does not then i check if it has a right child, if it does not 
            //also have a right child then I move on
            Node<T> theChild = node.LeftChild != null ? node.LeftChild : node.RightChild;
            
            //if it is has a child, then i change the parent of the child to the replaced node, why do i do this, remeber that the node we use
            //as a replacer is present in two position on the tree, its original position which we are tryin to get rid of and at the position 
            //where it is used as a replacer
            if (theChild != null)
            {
                theChild.Parent = node.Parent;

                //This check takes care of a situation where the replaced node is the root and has only a single child, with the replacement we
                //did earlier, the replacer node at its original positon has null parent, it has nothing pointing to it so this makes it easy for 
                //us to get rid of it, so the replacer node at the replaced node will become the the root of the tree.
                if (node.Parent == null)
                {
                    this.Root = theChild;
                }
                else
                {
                    // With this i am replacing the replacer node at its original position with its child, so the child moves up the tree, to
                    //a new position and as the child of the replacer node, at the replaced node position,, we know it is still the same value 
                    //at the replaced node
                    if (node.Parent.LeftChild == node)
                    {
                        node.Parent.LeftChild = theChild;
                    }
                    else
                    {
                        node.Parent.RightChild = theChild;
                    }
                }
            }
            else
            {
                // This takes care of the situation where the node i am removing has no child and it is actually the root of the tree, 
                //then i set the root to null and the tree becomes empty
                if (node.Parent == null)
                {
                    this.Root = null;
                }

                //This takes care of the situation where the node i am removing has no child and it is not the root of the tree, then
                //i check what position does the node take, the left or right side of its parent? when i get the position, i set it to null
                else
                {
                    if (node.Parent.LeftChild == node)
                    {
                        node.Parent.LeftChild = null;
                    }
                    else
                    {
                        node.Parent.RightChild = null;
                    }
                }
            }
        }

        //Allows me to traverse the tree, and this is an inorder traversion of the tree
        private void PrintTreeDFS(Node<T> node)
        {
            if (node != null)
            {
                PrintTreeDFS(node.LeftChild);
                Console.Write(node.Value + " ");
                PrintTreeDFS(node.RightChild);
            }
        }

        public void PrintTreeDFS()
        {
            PrintTreeDFS(this.Root);
            Console.WriteLine();
        }


        public bool Contains(T value)
        {
            bool found = this.Find(value) != null;
            return found;
        }

        static void Main(string[] args)
        {
            BinarySearchTree<int> bst = new BinarySearchTree<int>();
            bst.Insert(20);
            bst.Insert(14);
            bst.Insert(30);
            bst.Insert(10);
            bst.Insert(18);
            bst.Insert(25);
            bst.Insert(43);
            bst.Insert(19);
            bst.Remove(20);
            //Console.WriteLine(bst.Root);// 25
            Console.WriteLine(bst.Find(25));// 25
            bst.PrintTreeDFS(); //10 14 18 19 20 25 30 43
        }
    }
}
