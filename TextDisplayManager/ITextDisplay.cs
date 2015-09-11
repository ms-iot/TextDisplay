using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation;

namespace TextDisplay
{
    public interface ITextDisplay
    {
        uint Height
        {
            get;
        }

        uint Width
        {
            get;
        }

        IAsyncAction InitializeAsync();

        IAsyncAction DisposeAsync();

        IAsyncAction WriteMessageAsync(string message, uint timeout);
    }
}
