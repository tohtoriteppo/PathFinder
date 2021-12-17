using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PenttiMaze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PathFinder pathFinder;
        public MainWindow()
        {
            InitializeComponent();

            FindGoal();

        }


        void FindGoal()
        {
            // Read the file
            string filePath = Directory.GetCurrentDirectory() + "/";
            string fileName = "maze.txt";
            string text = File.ReadAllText(filePath + fileName);

            // Create the UI for the maze
            int width = text.IndexOf("\n") - 1;
            Grid myGrid = new Grid();
            myGrid.Width = width * 20;
            myGrid.Height = (text.Length / width) * 20;
            for (int i = 0; i < width; i++)
            {
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            myGrid.RowDefinitions.Add(new RowDefinition());

            // Create the necessary node containers
            List<List<Node>> nodes = new List<List<Node>>();
            nodes.Add(new List<Node>());
            List<Node> exits = new List<Node>();
            Node start = new Node();

            // Parse the file
            int column = 0;
            int row = 0;
            foreach (char c in text)
            {
                // end of line - next row in the maze
                if (c == '\n')
                { 
                    row++;
                    nodes.Add(new List<Node>());
                    myGrid.RowDefinitions.Add(new RowDefinition());
                    column = 0;
                    continue;
                }

                Node node = new Node();
                node.x = column;
                node.y = row;

                TextBlock textBlock = new TextBlock();
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;

                Grid.SetRow(textBlock, row);
                Grid.SetColumn(textBlock, column);

                // Parse the type of the cell
                switch (c)
                {
                    case '#':
                        textBlock.Text = "#";
                        node.type = Cell.Wall;
                        break;
                    case 'E':
                        textBlock.Text = "E";
                        node.type = Cell.Exit;
                        exits.Add(node);
                        break;
                    case '^':
                        textBlock.Text = "^";
                        node.type = Cell.Empty;
                        start = node;
                        break;
                    default:
                        textBlock.Text = " ";
                        node.type = Cell.Empty;
                        break;
                }

                nodes[row].Add(node);
                myGrid.Children.Add(textBlock);
                column++;
            }
            // Add a node at the end if the loop stops before the last char
            // Should be fine assuming that the borders are walls anyway
            if(column < width)
            {
                Node n = new Node();
                n.x = column;
                n.y = row;
                n.type = Cell.Wall;
                nodes[row].Add(n);
            }
           

            // Set the grid to the window
            Application.Current.MainWindow.Content = myGrid;

            pathFinder = new PathFinder(nodes);

            //Find the shortest path among all the exits
            int lowestCost = int.MaxValue;
            List<Node> bestPath = new List<Node>();
            foreach(Node exit in exits)
            {
                List<Node> path = pathFinder.FindPath(start, exit);
                if (path != null && path[0].cost < lowestCost)
                {
                    lowestCost = path[0].cost;
                    bestPath = path;
                }
            }
            
            //Visualize the shortest path in the UI with letter o
            foreach (Node node in bestPath)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Text = "o";
                Grid.SetRow(textBlock, node.y);
                Grid.SetColumn(textBlock, node.x);
                myGrid.Children.Add(textBlock);
            }

        }

    }
}
