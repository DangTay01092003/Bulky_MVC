﻿using Bulky.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Domain.IRepository
{
    public interface ICategoryRepository : IRepository<Category>

    {
        void Update(Category obj);
    }
}
