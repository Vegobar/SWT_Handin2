﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    public interface IDisplay
    {
        string ReceivedString { get; }

        void display(string text, int display_num);
    }
}