﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tex.Net.Engine
{
    public class CommandInvocationException : Exception
    {
        public CommandInvocationException(string message) : base(message)
        {
        }

        public CommandInvocationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CommandInvocationException()
        {
        }
    }
}
