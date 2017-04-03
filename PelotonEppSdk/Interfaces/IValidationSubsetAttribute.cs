using System;

namespace PelotonEppSdk.Interfaces
{
    public interface IValidationSubsetAttribute
    {
        Enum[] ValidationSubsetEnum { get; set; }
    }
}