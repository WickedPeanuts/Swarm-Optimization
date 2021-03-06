﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_Swarm_Optimization.Common
{
    public class Parameters
    {
        //Test
        public static readonly int PARTICLE_AMOUNT = 30;
        public static readonly int DIMENSION_AMOUNT = 30;
        public static readonly int ITERATION_AMOUNT = 10000;
        public static readonly int SAMPLE_COUNT = 30;

        //ABC
        public static readonly int EMPLOYED_BEE_AMOUNT = 40;
        public static readonly int SCOUT_BEE_AMOUNT = 20;
        public static readonly int ONLOOKER_BEE_AMOUNT = 100 - (EMPLOYED_BEE_AMOUNT + SCOUT_BEE_AMOUNT);
        public static readonly int INITIAL_FOOD_SOURCES = 3;
        public static readonly int ATTEMPT_TO_CHANGE_SOURCE = 3;

        //public static readonly int FOCAL_ADJACENT_PARTICLES = 2;
    }
}
