using System;

namespace Ludo.Service
{
    [AttributeUsage(AttributeTargets.Constructor, Inherited = false, AllowMultiple = false)]
    public sealed class InjectAttribute : Attribute
    {

    }
}
