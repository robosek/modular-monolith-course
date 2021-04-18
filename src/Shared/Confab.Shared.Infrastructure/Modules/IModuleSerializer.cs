using System;
namespace Confab.Shared.Infrastructure.Modules
{
    internal interface IModuleSerializer
    {
        byte[] Serialize<T>(T value);
        T Deserialize<T>(byte[] obj);
        object Deserialize(byte[] obj, Type type);
    }
}
