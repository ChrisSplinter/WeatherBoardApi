﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBoard2.ErrorHandling
{
  public class LocationNotFoundException:Exception
    {
        public LocationNotFoundException()
        {
        }

        public LocationNotFoundException(string message) : base(message)
        {
        }

        public LocationNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

    }
}
