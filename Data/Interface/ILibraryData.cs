using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interface
{
    public interface ILibraryData
    {
        Library CreateLibrary(Library library);
        Library UpdateLibrary(Library library);
        Library DeleteLibrary(int libraryId);
        Library GetLibraryById(int libraryId);
        IEnumerable<Library> GetLibraries();
        int Commit();
    }
}
