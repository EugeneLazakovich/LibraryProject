﻿using Lesson1_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson1_DAL.Interfaces
{
    public interface IBooksRepository
    {
        Task<(Book book, IEnumerable<BookRevision> bookRevisions)> GetFullInfo(Guid id);
    }
}