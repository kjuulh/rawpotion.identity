using System;
using Microsoft.AspNetCore.Http;

namespace Rawpotion.Identity.Common.Objects
{
    public class RawpotionEndpoint
    {
        public string Name { get; }
        public PathString Path { get; }
        public Type Handler { get; }

        public RawpotionEndpoint(string name, in PathString path, Type handlerType)
        {
            Name = name;
            Path = path;
            Handler = handlerType;
        }
    }
}