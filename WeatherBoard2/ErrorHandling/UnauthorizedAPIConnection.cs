using System;


namespace WeatherBoard2.ErrorHandling
{
    public class UnauthorizedAPIConnection : Exception
    {
        public UnauthorizedAPIConnection()
        {
        }

        public UnauthorizedAPIConnection(string message) : base(message)
        {
        }

        public UnauthorizedAPIConnection(string message, Exception inner) : base(message, inner)
        {
        }
    }
}