using NeatTest.ConsoleApp.AI;
using SharpNeat.EvolutionAlgorithms;
using SharpNeat.Genomes.Neat;
using System;
using System.Collections.Generic;
using System.Xml;
using TitTacToeGame;

namespace NeatTest.ConsoleApp
{
    class Program
    {
        const string FILE = "train.xml";

        static void Main(string[] args)
        {
            bool train = true;

            GameExperiment experiment = new GameExperiment();

            // Load config XML.
            XmlDocument xmlConfig = new XmlDocument();
            xmlConfig.Load("expconfig.xml");
            experiment.Initialize("TicTacToe", xmlConfig.DocumentElement);

            if (!train)
            {
                #region Play
                NeatGenome genome = null;

                // Try to load the genome from the XML document.
                try
                {
                    using (XmlReader xr = XmlReader.Create(FILE))
                        genome = NeatGenomeXmlIO.ReadCompleteGenomeList(xr, false, (NeatGenomeFactory)experiment.CreateGenomeFactory())[0];
                }
                catch (Exception e1)
                {
                    return;
                }

                // Get a genome decoder that can convert genomes to phenomes.
                var genomeDecoder = experiment.CreateGenomeDecoder();

                // Decode the genome into a phenome (neural network).
                var phenome = genomeDecoder.Decode(genome);

                IPlayer p1 = new NeatBrain(phenome);
                IPlayer p2 = new RandomBrain();

                while (true)
                {

                    Game Game = new Game(p1, p2);
                    Game.PlayUntilFinished();

                    Draw(Game.Board);

                    Console.WriteLine(Game.GetGameState().ToString());
                    Console.ReadKey();
                    Console.WriteLine();
                }
                #endregion Play
            }
            else
            {
                #region Instructions
                Console.WriteLine("This is the training example. If you want to APPLY it, modify the boolean 'train' to false.");
                Console.WriteLine("Take into acocunt that training generates a file 'expconfig.xml' that you can load to play.");
                #endregion


                #region Train
                NeatEvolutionAlgorithm<NeatGenome> _ea;

                // Create evolution algorithm and attach update event.
                _ea = experiment.CreateEvolutionAlgorithm();
                _ea.UpdateEvent += new EventHandler(ea_UpdateEvent);

                // Start algorithm (it will run on a background thread).
                _ea.StartContinue();

                Console.ReadLine();
                #endregion Train
            }
        }

        private static void ea_UpdateEvent(object sender, EventArgs e)
        {
            NeatEvolutionAlgorithm<NeatGenome> _ea = (NeatEvolutionAlgorithm<NeatGenome>)sender;
            Console.WriteLine(string.Format("gen={0:N0} bestFitness={1:N6}", _ea.CurrentGeneration, _ea.Statistics._maxFitness));

            // Save the best genome to file
            var doc = NeatGenomeXmlIO.SaveComplete(new List<NeatGenome>() { _ea.CurrentChampGenome }, false);
            doc.Save(FILE);
        }

        #region Draw
        private static void Draw(Board board)
        {
            WriteLine(board.Squares, 0);
            WriteLine(board.Squares, 1);
            WriteLine(board.Squares, 2);
        }

        private static void WriteLine(Square[,] squares, int line)
        {
            WriteCell(squares[0, line]);
            WriteCell(squares[1, line]);
            WriteCell(squares[2, line]);
            Console.WriteLine();
        }

        private static void WriteCell(Square square)
        {
            Console.Write(TranslateValue(square.State));
        }

        private static string TranslateValue(Square.StateEnum state)
        {
            switch (state)
            {
                case Square.StateEnum.Player1:
                    return "X";
                case Square.StateEnum.Player2:
                    return "O";
                default:
                    return " ";
            }
        }
        #endregion Draw
    }
}
