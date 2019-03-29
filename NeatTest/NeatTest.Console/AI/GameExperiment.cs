using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpNeat.Domains;
using SharpNeat.Phenomes;
using SharpNeat.Core;

namespace NeatTest.ConsoleApp.AI
{
    public class GameExperiment : SimpleNeatExperiment
    {
        public override IPhenomeEvaluator<IBlackBox> PhenomeEvaluator
        {
            get { return new GameEvaluator(); }
        }

        public override int InputCount
        {
            get { return 9; }
        }

        public override int OutputCount
        {
            get { return 9; }
        }

        public override bool EvaluateParents
        {
            get { return true; }
        }
    }
}
