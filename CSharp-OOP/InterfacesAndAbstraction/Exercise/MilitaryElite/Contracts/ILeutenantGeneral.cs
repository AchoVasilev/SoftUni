using System.Collections.Generic;

namespace MilitaryElite.Contracts
{
    public interface ILeutenantGeneral
    {
        public ICollection<IPrivate> Privates { get; }
    }
}
