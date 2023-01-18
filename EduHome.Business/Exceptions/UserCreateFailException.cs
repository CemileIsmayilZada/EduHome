﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Exceptions
{
    public sealed class UserCreateFailException : Exception
    {
        public UserCreateFailException(string message):base(message) 
        {

        }
    }
}
