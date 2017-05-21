using System.Collections.Generic;

namespace Task_3.Interfaces
{
    public interface IStorage<T>
    {
        IList<T> GetInfoList();
    }
}