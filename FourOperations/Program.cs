using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourOperations
{
    public class Program
    {
        static void Main(string[] args)
        {
            var eTree = GetExpressionTree("((15 / (7 - (1 - 1))) * 3) - (2 + (1 + 1)))");
            string sb = "";
            eTree.traversePreOrder(ref sb,"","",eTree.root);
            Console.WriteLine(sb);
            Console.ReadKey();
        }

        public static BinaryExpressionTree GetExpressionTree(string input)
        {
            Stack<TreeNode> nodes = new Stack<TreeNode>();
            Stack<string> operaters = new Stack<string>();

            Dictionary<string, int> priority = new Dictionary<string, int>();
            priority.Add("(", 1);
            priority.Add("+", 2);
            priority.Add("-", 2);
            priority.Add("*", 3);
            priority.Add("/", 3);            

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    operaters.Push("(");
                }
                else if (input[i] == ')')
                {
                    while (operaters.Peek() != "(")
                    {
                        // merge the nodes inside "()" as whole nodes
                        MergeToOneNode(nodes, operaters);
                    }
                    operaters.Pop();
                }
                else if (char.IsDigit(input[i]))
                {
                    string num = "";
                    while (i < input.Length && char.IsDigit(input[i]))
                    {
                        num += input[i];
                        i++;
                    }
                    i--;
                    TreeNode Node = new TreeNode();
                    Node.Value = num;
                    // Because digit node is always a child node. simply push to nodes and wait for its parent.
                    nodes.Push(Node);
                }
                else if (input[i] == '+' || input[i] == '-'|| input[i] == '*' || input[i] == '/')
                {                   
                   
                    string current = "" + input[i];
                    
                    while (operaters.Count > 0 && priority[current]<=priority[operaters.Peek()])
                    {
                        // when priority of A is higher than B, means the calculation of A must be done before B. 
                        // we should do */ before +-, and all calculations inside before "("
                        // if priority(A)>=priority(B), That make A to be a child node of B, then we should merge A nodes and push to stack as new child nodes.
                        MergeToOneNode(nodes, operaters);
                    }
                    // push a new parent nodes in operator
                    operaters.Push(current);
                }              
            }

            while (operaters.Count > 0)
            {
                // merge the remaining nodes from bottom to top.
                MergeToOneNode(nodes, operaters);
            }            

            var eTree = new BinaryExpressionTree(nodes.Pop());
            return eTree;
        }

        public static void MergeToOneNode(Stack<TreeNode> nodes, Stack<string> opeartors)
        {
            //create new merge node, retrieve the last 2 nodes as it's chirdren. And push back to nodes stack as new left child for next parent.
            string op = opeartors.Pop();
            TreeNode merge = new TreeNode();
            merge.Value = op;
            merge.Right = nodes.Pop();
            merge.Left = nodes.Pop();
            nodes.Push(merge);
        }


        public class BinaryExpressionTree
        {
            public TreeNode root { get; set; }

            public BinaryExpressionTree(TreeNode treeNode)
            {
                root = treeNode;
            }

            public void traversePreOrder(ref String sb, String padding, String pointer, TreeNode node)
            {
                if (node != null)
                {
                    sb+=padding;
                    sb+=pointer;
                    sb+=node.Value;
                    sb+="\n";

                    String paddingBuilder = padding;
                    paddingBuilder+="│  ";

                     String paddingForBoth = paddingBuilder;
                    String pointerForRight = "└──";
                    String pointerForLeft = (node.Right != null) ? "├──" : "└──";

                    traversePreOrder(ref sb, paddingForBoth, pointerForLeft, node.Left);
                    traversePreOrder(ref sb, paddingForBoth, pointerForRight, node.Right);
                }
            }
        }

        public class TreeNode
        {
            public string Value { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }

            public int CalculateLeftAndRight()
            {                
                int result = 0;
                if (Left == null || Right == null) return Int32.Parse(Value);
                switch (this.Value)
                {
                    case "*":
                        result = Left.CalculateLeftAndRight() * Right.CalculateLeftAndRight();
                        break;
                    case "/":
                        result = Left.CalculateLeftAndRight() / Right.CalculateLeftAndRight();
                        break;
                    case "+":
                        result = Left.CalculateLeftAndRight() + Right.CalculateLeftAndRight();
                        break;
                    case "-":
                        result = Left.CalculateLeftAndRight() - Right.CalculateLeftAndRight();
                        break;
                    default:
                        result = 0;
                        break;
                }

                return result;

            }
        }
    }

    
}
