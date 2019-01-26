using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace add
{
    class Program
    {
        static void Main(string[] args)
        {
            AVL AVLTREE = new AVL();
            AVLTREE.InsertNode(5);
            AVLTREE.InsertNode(7);
            AVLTREE.InsertNode(19);
            AVLTREE.InsertNode(12);
            AVLTREE.InsertNode(10);
            AVLTREE.InsertNode(15);
            AVLTREE.InsertNode(10);

            AVLTREE.DeleteNode(5);
            AVLTREE.DisplayTree();

            Console.ReadLine();
        }
    }
    public class Node
    {//intializing properties for node
        public int data;
        public Node left;
        public Node right;

        public Node(int data)
        {
            this.data = data;
        }
    }
    public class AVL
    {
        public Node root;
        public AVL()
        {
        }
        //wrap function of search method
        public void Search(int data)
        {
            if (Search(data, root).data == data)
            {
                Console.WriteLine("{0} Target was found", data);
            }
            else
            {
                Console.WriteLine("Your given data is sufficient");
            }
        }
        private Node Search(int Givendata, Node Currentnode)
        {//if there is no node
            if (Currentnode == null)
            {
                return null;
            }
            //when the target if found
            if (Givendata == Currentnode.data)
            {
                return Currentnode;
            }
            if (Givendata < Currentnode.data)
            {
                return Search(Givendata, Currentnode.left);
            }
            else
            {
                return Search(Givendata, Currentnode.right);
            }

        }
        //wrap function for delete method whick takes a perticular target from user
        public void DeleteNode(int target)
        {
            root = DeleteNode(root, target);
        }
        private Node DeleteNode(Node CurrentNode, int GivenData)
        {
            Node ParentNode;
            //if there is no node 
            if (CurrentNode == null)
            { return null; }
            else
            {
                //Searching your target in left sub tree 
                if (GivenData < CurrentNode.data)
                {
                    CurrentNode.left = DeleteNode(CurrentNode.left, GivenData);
                    //checking the condition according to which we have to perform rotations 
                    if (LR_Difference(CurrentNode) == -2)
                    {
                        if (LR_Difference(CurrentNode.right) <= 0)
                        {
                            CurrentNode = RR_Rotation(CurrentNode);
                        }
                        else
                        {
                            CurrentNode = RL_Rotation(CurrentNode);
                        }
                    }
                }
                //Searching your target in right sub tree 
                else if (GivenData > CurrentNode.data)
                {
                    CurrentNode.right = DeleteNode(CurrentNode.right, GivenData);
                    //checking the condition according to which we have to perform rotations
                    if (LR_Difference(CurrentNode) == 2)
                    {
                        if (LR_Difference(CurrentNode.left) >= 0)
                        {
                            CurrentNode = LL_Rotation(CurrentNode);
                        }
                        else
                        {
                            CurrentNode = LR_Rotation(CurrentNode);
                        }
                    }
                }
                //when te given target is achieved 
                else
                {
                    if (CurrentNode.right != null)
                    {
                        //DeleteNode the node by the help of succesor
                        ParentNode = CurrentNode.right;
                        while (ParentNode.left != null)
                        {
                            ParentNode = ParentNode.left;
                        }
                        CurrentNode.data = ParentNode.data;
                        CurrentNode.right = DeleteNode(CurrentNode.right, ParentNode.data);
                        //rebalancing the tree after deletion
                        if (LR_Difference(CurrentNode) == 2)
                        {
                            if (LR_Difference(CurrentNode.left) >= 0)
                            {
                                CurrentNode = LL_Rotation(CurrentNode);
                            }
                            else { CurrentNode = LR_Rotation(CurrentNode); }
                        }
                    }
                    else
                    {   //if CurrentNode.left != null
                        return CurrentNode.left;
                    }
                }
            }
            return CurrentNode;
        }
        //wrap method for inserting nodewhich takes data from user
        public void InsertNode(int data)
        {
            Node newItem = new Node(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = InsertNode(root, newItem);//caling of insertnode method bypassing the above data and root node
            }
        }
        private Node InsertNode(Node CurrentNode, Node n)
        {
            if (CurrentNode == null)
            {
                CurrentNode = n;
                return CurrentNode;
            }
            else if (n.data < CurrentNode.data)
            {
                CurrentNode.left = InsertNode(CurrentNode.left, n);
                CurrentNode = TreeBalance(CurrentNode);
            }
            else if (n.data > CurrentNode.data)
            {
                CurrentNode.right = InsertNode(CurrentNode.right, n);
                CurrentNode = TreeBalance(CurrentNode);
            }
            return CurrentNode;
        }
        //tree balancing method 
        private Node TreeBalance(Node CurrentNode)
        {//calling the balance factor function in order to calculate the balance factor
            int DiffFactor = LR_Difference(CurrentNode);
            if (DiffFactor > 1)//main condition for LL and LR rotation
            {
                if (LR_Difference(CurrentNode.left) > 0)
                {
                    CurrentNode = LL_Rotation(CurrentNode);
                }
                else
                {
                    CurrentNode = LR_Rotation(CurrentNode);
                }
            }
            else if (DiffFactor < -1)//main condition for RR and RL rotation
            {
                if (LR_Difference(CurrentNode.right) > 0)
                {
                    CurrentNode = RL_Rotation(CurrentNode);
                }
                else
                {
                    CurrentNode = RR_Rotation(CurrentNode);
                }
            }
            return CurrentNode;
        }
        //tree display function
        public void DisplayTree()

        {//asking the user about there choice related to traversing and printing of the tree
            Console.WriteLine("PRESS \n0 for inorder traversing\n1 for Preorder traversing\n2 for Postoder Traversing");
            int opt = Convert.ToInt16(Console.ReadLine());

            switch (opt)
            {
                case 0:
                    if (root == null)
                    {
                        Console.WriteLine("Tree is empty");
                        return;
                    }
                    InOrderDisplayTree(root);
                    Console.WriteLine();

                    break;
                case 1:
                    if (root == null)
                    {
                        Console.WriteLine("Tree is empty");
                        return;
                    }
                    PreOrderDisplayTree(root);
                    Console.WriteLine();
                    break;
                case 2:
                    if (root == null)
                    {
                        Console.WriteLine("Tree is empty");
                        return;
                    }
                    PostOrderDisplayTree(root);
                    Console.WriteLine();
                    break;
            }
        }
        //inorder display tree
        private void InOrderDisplayTree(Node CurrentNode)
        {
            if (CurrentNode != null)
            {
                InOrderDisplayTree(CurrentNode.left);
                Console.Write("[{0}] ", CurrentNode.data);
                InOrderDisplayTree(CurrentNode.right);
            }
        }
        //preorderdisplay tree
        private void PreOrderDisplayTree(Node CurrentNode)
        {
            if (CurrentNode != null)
            {
                Console.Write("[{0}] ", CurrentNode.data);
                InOrderDisplayTree(CurrentNode.left);
                InOrderDisplayTree(CurrentNode.right);
            }

        }
        //postorder display tree
        private void PostOrderDisplayTree(Node CurrentNode)
        {
            if (CurrentNode != null)
            {

                InOrderDisplayTree(CurrentNode.left);
                InOrderDisplayTree(CurrentNode.right);
                Console.Write("[{0}] ", CurrentNode.data);
            }

        }
        //height calculating function which takes a perticular node as a parameter and returns the height of that node
        public int getHeight(Node Currentnode)
        {
            if (Currentnode == null)
                return -1;

            int left = getHeight(Currentnode.left);
            int right = getHeight(Currentnode.right);

            if (left > right)
                return left + 1;
            else
                return right + 1;
        }
        //balance factor calculation
        private int LR_Difference(Node CurrentNode)
        {
            int l = getHeight(CurrentNode.left);
            int r = getHeight(CurrentNode.right);
            int DiffFactor = l - r;
            return DiffFactor;
        }
        //Rightright rotation by using temporary node
        private Node RR_Rotation(Node ParentNode)
        {
            Node TemporaryNode = ParentNode.right;
            ParentNode.right = TemporaryNode.left;
            TemporaryNode.left = ParentNode;
            return TemporaryNode;
        }
        //Leftleft rotation by using temporary node
        private Node LL_Rotation(Node ParentNode)
        {
            Node TemporaryNode = ParentNode.left;
            ParentNode.left = TemporaryNode.right;
            TemporaryNode.right = ParentNode;
            return TemporaryNode;
        }
        //Leftright rotation by using temporary node
        private Node LR_Rotation(Node ParentNode)
        {
            Node TemporaryNode = ParentNode.left;
            ParentNode.left = RR_Rotation(TemporaryNode);
            return LL_Rotation(ParentNode);
        }
        //Rightleft rotation by using temporary node
        private Node RL_Rotation(Node ParentNode)
        {
            Node TemporaryNode = ParentNode.right;
            ParentNode.right = LL_Rotation(TemporaryNode);
            return RR_Rotation(ParentNode);
        }
    }

}


