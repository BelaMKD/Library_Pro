using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interface
{
    public interface ILendingData
    {
        Lending CreateLending(Lending lending);
        Lending UpdateLending(Lending lending);
        Lending DeleteLenging(int lendingId);
        Lending GetLendingById(int lendingId);
        IEnumerable<Lending> GetLendings();
        int Commit();
    }
}
