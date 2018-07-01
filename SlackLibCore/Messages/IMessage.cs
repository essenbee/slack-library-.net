using System;

namespace SlackLibCore.Messages
{
    public interface IMessage
    {
        String Type { get; }
        TimeStamp ts { get; }
    }
}
