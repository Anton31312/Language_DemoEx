﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fedyaev_Language_01.EF
{
    public partial class Client
    {
        public List<Tag> Tags
        {
            get
            {
                return Tag.ToList();
            }
        }
    }
}
