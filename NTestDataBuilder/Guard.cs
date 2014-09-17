﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTestDataBuilder
{
    public static class Guard
    {
        public static void Against(bool condition, string errorMessage)
        {
            if (condition)
            {
                throw new ArgumentException(errorMessage);
            }
        }
    }
}