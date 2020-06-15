using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interface
{
    public interface IPublisherData
    {
        Publisher CreatePublisher(Publisher publiser);
        Publisher UpdatePublisher(Publisher publiser);
        Publisher DeletePublisher(int publisherId);
        Publisher GetPubliserById(int publisherId);
        IEnumerable<Publisher> GetPublisers();
        int Commit();
    }
}
