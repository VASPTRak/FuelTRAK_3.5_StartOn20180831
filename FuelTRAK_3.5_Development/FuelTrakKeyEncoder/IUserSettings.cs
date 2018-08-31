using System;
namespace FuelTrakKeyEncoder
{
    public interface IUserSettings
    {
        string ComPort { get; }
        string FuelTrakUrl { get; }
    }
}
