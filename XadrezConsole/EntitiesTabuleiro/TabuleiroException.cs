using System;
using System.Collections.Generic;
using System.Text;

namespace XadrezConsole.EntitiesTabuleiro
{
    class TabuleiroException : ApplicationException
    {
        public TabuleiroException(string mensage) : base(mensage) { }
    }
}
