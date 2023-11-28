using System;
namespace FashionTrend.Domain.Enums;

public enum MessageStatus
{
    InProcessing,
    WithError,
    Retry,
    Successful
}

